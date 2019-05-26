using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using teest.models;

namespace teest
{
    public partial class ProduktDetaljer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Forbindelse til SQL Databasen
                SqlDataSource SqlDataSource1 = new SqlDataSource();                                         //Laver en SqlDataSource
                SqlDataSource1.ID = "SqlDataSource1";                                                       //Giver den et ID
                this.Page.Controls.Add(SqlDataSource1);                                                     //Tilføjer den til siden
                SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
                SqlDataSource1.SelectCommand = "SELECT * FROM ProduktTabel WHERE ProduktID=@bid";           //Vores SELECT statement

                DataSet ds = new DataSet();
                SqlDataSource1.SelectParameters.Add("bid", Page.Request.QueryString["produktid"].ToString());
                DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                labProduktNavn.Text = test.Table.Rows[0]["ProduktNavn"].ToString();
                labBeskrivelse.Text = test.Table.Rows[0]["ProduktBeskrivelse"].ToString();
                labProduktetsPris.Text = test.Table.Rows[0]["ProduktPris"].ToString();
                labGravPris.Text = "100";

                int ProduktPris = Convert.ToInt32(labProduktetsPris.Text);
                int gravpris = Convert.ToInt32(labGravPris.Text);
                int PrisIAlt = ProduktPris + gravpris;
                labPrisIAlt.Text = PrisIAlt.ToString();
                imgProdukt.Src = "./Images/" + test.Table.Rows[0]["image"].ToString();
                imgIndkøbskurv.Src = "./Images/" + test.Table.Rows[0]["image"].ToString();
            }
        }
        
        //Retter prisen til når antal grave er ændret
        public void DropDownChanged(object sender, EventArgs e)
        {
            int ProduktPris = Convert.ToInt32(labProduktetsPris.Text);
            int antal = Convert.ToInt32(DropDown.SelectedValue);
            int gravpris = 100 * antal;
            int PrisIAlt = ProduktPris + gravpris;
            labPrisIAlt.Text = PrisIAlt.ToString();
            labGravPris.Text = gravpris.ToString();
        }

        //Tilføjer produktet til indkøbskurven
        public void btnTilføj_Click(object sender, EventArgs e)
        {
            int ProduktPris = Convert.ToInt32(labProduktetsPris.Text);
            int antal = Convert.ToInt32(DropDown.SelectedValue);
            int gravpris = 100 * antal;
            int PrisIAlt = ProduktPris + gravpris;
            labPrisIAlt.Text = PrisIAlt.ToString();
            labDetails.Text = labProduktNavn.Text;
            labTotal.Text = PrisIAlt.ToString();
            DropDownList1.SelectedValue = DropDown.SelectedValue;
            tableIndkøbskurv.Visible = true;
            btnBuy.Visible = true;
            DropDown.Enabled = false;
            btnTilføj.Enabled = false;
        }

        //Køber produktet
        protected void btnBuy_Click(object sender, EventArgs e)
        {
            //Håndterer data til og fra vores SQL Database
            SqlDataSource SqlDataSource2 = new SqlDataSource();
            SqlDataSource2.ID = "SqlDataSource2";
            this.Page.Controls.Add(SqlDataSource2);
            SqlDataSource2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;

            //Hiver brugerens KundeID
            SqlDataSource2.SelectCommand = "SELECT KundeID FROM KundeTabel WHERE Mail='" + HttpContext.Current.Session["Username"].ToString() + "'";           //Vores SELECT statement
            DataSet ds = new DataSet();
            DataView test = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

            //Her kalder vi vores klasse ShoppingCart som kalder vores klasse DataAccess = Inheritance/Nedarvning
            IndkøbsKurv k = new IndkøbsKurv()
            {
                ProduktID = Page.Request.QueryString["produktid"],
                KundeID = test.Table.Rows[0]["KundeID"].ToString(),
                Antal = DropDown.SelectedValue,
            };
            //Her kører vi en funktion der findes i vores ShoppingCart klasse - Denne funktion kører en Stores Procedure
            k.BuyProduct();

            //Indsætter det købte i TagTabellen hvor KundeID er = kundens KundeID
            //SqlDataSource2.InsertCommand = "INSERT INTO [TagTabel] ([ProduktID], [KundeID], [Antal]) VALUES (@ProduktID, @KundeID, @Antal)";
            //SqlDataSource2.InsertParameters.Add("ProduktID", Page.Request.QueryString["produktid"].ToString());
            //SqlDataSource2.InsertParameters.Add("KundeID", test.Table.Rows[0]["KundeID"].ToString());
            //SqlDataSource2.InsertParameters.Add("Antal", DropDownList1.SelectedValue);
            //SqlDataSource2.Insert();

            //Finder det TagID der lige er blevet oprettet
            //string kundeID = test.Table.Rows[0]["KundeID"].ToString();
            //SqlDataSource2.SelectCommand = "SELECT MAX(TagID) FROM TagTabel WHERE KundeID='" + kundeID + "'";           //Vores SELECT statement
            //DataView test2 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            //string tagID = test2.Table.Rows[0][0].ToString();

            //Finder antal af grave der skal oprettes på det valgte Tag
            //int antal = Convert.ToInt32(DropDown.SelectedValue);

            //Forbereder kommandoen til at indsætte X antal grave på det valgte Tag
            //SqlDataSource2.InsertCommand = "INSERT INTO [GravTabel] ([TagID]) VALUES (@TagID)";
            //SqlDataSource2.InsertParameters.Add("TagID", tagID);

            //Et loop der indsætter TagID i vores GravTabel X antal gange (Dette opretter en grav for det antal grave kunden har valgt)
            //for (int i = 0; i < antal; i++)
            //{
            //    SqlDataSource2.Insert();
            //}

            //SqlDataSource2.SelectCommand = "SELECT MAX(GravID) FROM TagTabel WHERE KundeID='" + kundeID + "'";           //Vores SELECT statement
            //test2 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            //string gravID = test2.Table.Rows[0][0].ToString();

            //SqlDataSource2.UpdateCommand = "UPDATE [TagTabel] SET [GravID] = @GravID WHERE TagID = @tagID";
            //SqlDataSource2.UpdateParameters.Add("GravID", gravID);
            //SqlDataSource2.UpdateParameters.Add("TagID", tagID);
            //SqlDataSource2.Update();
            
            //Sender en mail til kunden (Ordrebekræftelse)
            MailMessage mm = new MailMessage("chsdk89@gmail.com", "chsecurity@live.dk");
            //MailMessage mm = new MailMessage("chsdk89@gmail.com", txtForgotPass.Text.Trim())
            mm.Subject = "Ordrebekræftelse:";
            mm.Body = "Du har købt: ";
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = "memorialtag@gmail.com";
            NetworkCred.Password = "Pa$$w0rdtag";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            Response.Redirect("./default.aspx");
        }
    }
}