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
                //FormView1.DataSource = test;
                //FormView1.DataBind();
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
        
        public void DropDownChanged(object sender, EventArgs e)
        {
            int ProduktPris = Convert.ToInt32(labProduktetsPris.Text);
            int antal = Convert.ToInt32(DropDown.SelectedValue);
            int gravpris = 100 * antal;
            int PrisIAlt = ProduktPris + gravpris;
            labPrisIAlt.Text = PrisIAlt.ToString();
            labGravPris.Text = gravpris.ToString();
        }

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
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            SqlDataSource SqlDataSource2 = new SqlDataSource();
            SqlDataSource2.ID = "SqlDataSource2";
            this.Page.Controls.Add(SqlDataSource2);
            SqlDataSource2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;

            SqlDataSource2.SelectCommand = "SELECT KundeID FROM KundeTabel WHERE Mail='" + HttpContext.Current.Session["Username"].ToString() + "'";           //Vores SELECT statement
            DataSet ds = new DataSet();
            DataView test = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            
            SqlDataSource2.InsertCommand = "INSERT INTO [TagTabel] ([ProduktID], [KundeID], [Antal]) VALUES (@ProduktID, @KundeID, @Antal)";
            SqlDataSource2.InsertParameters.Add("ProduktID", Page.Request.QueryString["produktid"].ToString());
            SqlDataSource2.InsertParameters.Add("KundeID", test.Table.Rows[0]["KundeID"].ToString());
            SqlDataSource2.InsertParameters.Add("Antal", DropDownList1.SelectedValue);
            SqlDataSource2.Insert();

            string kundeID = test.Table.Rows[0]["KundeID"].ToString();
            SqlDataSource2.SelectCommand = "SELECT MAX(TagID) FROM TagTabel WHERE KundeID='" + kundeID + "'";           //Vores SELECT statement
            DataView test2 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

            SqlDataSource2.InsertCommand = "INSERT INTO [GravTabel] ([TagID]) VALUES(@TagID)";
            SqlDataSource2.InsertParameters.Add("TagID", test2.Table.Rows[0][0].ToString());
            SqlDataSource2.Insert();

            SqlDataSource2.SelectCommand = "SELECT MAX(GravID) FROM TagTabel WHERE KundeID='" + kundeID + "'";           //Vores SELECT statement
            test2 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            string gravID = test2.Table.Rows[0][0].ToString();




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