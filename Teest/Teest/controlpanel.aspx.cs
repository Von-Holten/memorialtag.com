using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Teest
{
    public partial class controlpanel : System.Web.UI.Page
    {
        //Det der sker når siden indlæses
        protected void Page_Load(object sender, EventArgs e)
        {
            //Opretter en datasource som kan bruges under hele Page_Load
            SqlDataSource SqlDataSource1 = new SqlDataSource();                                         //Laver en SqlDataSource
            SqlDataSource1.ID = "SqlDataSource1";                                                       //Giver den et ID
            this.Page.Controls.Add(SqlDataSource1);                                                     //Tilføjer den til siden
            SqlDataSource1.CancelSelectOnNullParameter = true;
            try
            {
                //Kigger om brugeren er logget ind
                if (HttpContext.Current.Session["Username"].ToString() != "")
                {   
                    //Forbinder til databasen for at kontrollere at brugernavnet eksisterer og kun findes en gang
                    SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
                    SqlDataSource1.SelectCommand = "SELECT COUNT(1) FROM LoginTabel WHERE Brugernavn= '" + HttpContext.Current.Session["Username"].ToString() + "' AND Adgangskode= '" + HttpContext.Current.Session["Password"].ToString() + "'"; //Vores SELECT statement
                    DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);           //Laver et test dataview til vores template
                    int count = Convert.ToInt32(test.Table.Rows[0][0]);
                    if (count == 1)
                    {
                        //Hiver brugernavnet fra vores sessions variabel og viser det som en overskrift
                        string User = HttpContext.Current.Session["Username"].ToString();
                        HttpContext.Current.Session["Username"] = User;
                        labUserOverskrift.Text = "Hej, " + User;

                        //Vores forbindelse til SQL databasen
                        SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
                        SqlDataSource1.SelectCommand = "SELECT [KundeTabel].[Fornavn], [KundeTabel].[Efternavn], [KundeTabel].[Telefon], [KundeTabel].[Mail], [LandNavn], [ByNavn], [KundeTabel].[Postnummer], [KundeTabel].[Vej], [KundeTabel].[KundeID], [TagTabel].[TagID], GravTabel.[GravID] FROM [KundeTabel] INNER JOIN  LandTabel ON KundeTabel.LandID = LandTabel.LandID INNER JOIN ByTabel ON KundeTabel.Postnummer = ByTabel.Postnummer INNER JOIN TagTabel ON KundeTabel.KundeID = TagTabel.KundeID INNER JOIN GravTabel ON TagTabel.TagID = GravTabel.TagID WHERE [KundeTabel].[Mail] = '" + User + "'";

                        //Error handling med en try cath
                        try
                        {
                            test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                            string TagID = test.Table.Rows[0]["TagID"].ToString();
                            string GravID = test.Table.Rows[0]["GravID"].ToString();
                            string KundeID = test.Table.Rows[0]["KundeID"].ToString();

                            if (test.Table.Rows[0]["Fornavn"].ToString() != null) { labFornavn.Text = test.Table.Rows[0]["Fornavn"].ToString(); }
                            if (test.Table.Rows[0]["Efternavn"].ToString() != null) { labEfternavn.Text = test.Table.Rows[0]["Efternavn"].ToString(); }
                            if (test.Table.Rows[0]["Telefon"].ToString() != null) { labTelefon.Text = test.Table.Rows[0]["Telefon"].ToString(); }
                            if (test.Table.Rows[0]["Mail"].ToString() != null) { labEmail.Text = test.Table.Rows[0]["Mail"].ToString(); }
                            if (test.Table.Rows[0]["LandNavn"].ToString() != null) { labLand.Text = test.Table.Rows[0]["LandNavn"].ToString(); }
                            if (test.Table.Rows[0]["ByNavn"].ToString() != null) { labBy.Text = test.Table.Rows[0]["ByNavn"].ToString(); }
                            if (test.Table.Rows[0]["Postnummer"].ToString() != null) { labPostnr.Text = test.Table.Rows[0]["Postnummer"].ToString(); }
                            if (test.Table.Rows[0]["Vej"].ToString() != null) { labVej.Text = test.Table.Rows[0]["Vej"].ToString(); }
                            if (test.Table.Rows[0]["KundeID"].ToString() != null) { kundeID.Text = "Kunde ID: " + test.Table.Rows[0]["KundeID"].ToString(); }

                            //Ny SQL Select kommando på grav tabellen istedet for kunde tabellen
                            SqlDataSource SqlDataSourceFrontPage = new SqlDataSource();                                         //Laver en SqlDataSource
                            SqlDataSourceFrontPage.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
                            SqlDataSourceFrontPage.ID = "SqlDataSourceFrontPage";                                               //Giver den et ID
                            this.Page.Controls.Add(SqlDataSourceFrontPage);                                                     //Tilføjer den til siden
                            SqlDataSourceFrontPage.SelectCommand = "SELECT [Fornavn], [Efternavn], [Fødselsdato], [Dødsdato], [GravTabel].[GravID] FROM[GravTabel] INNER JOIN TagTabel ON GravTabel.TagID = TagTabel.TagID WHERE KundeID = '" + KundeID + "'";
                            test = (DataView)SqlDataSourceFrontPage.Select(DataSourceSelectArguments.Empty);
                        }
                        catch
                        {
                            Response.Redirect("./Error.aspx");
                        }
                    }
                }
                else { Response.Redirect("./Error.aspx"); }
            }
            catch { Response.Redirect("./Error.aspx"); }   
        }

        //Vores Skift Password knap der tillader at der skrives i text boksene
        protected void Button1_Click(object sender, EventArgs e)
        {
            string User = Convert.ToString(Session["Username"]);
            //Hvis knappens tekst er = Godkend
            if (Button1.Text != "Godkend")
            {
                Password.Enabled = true;
                NytPassword.Enabled = true;
                GentagPassword.Enabled = true;
                Button1.Text = "Godkend";
            }
            else //Hvis knappens tekst er = Skift Password
            {   
                //Hvis kodeordet i tekstboksen mathcer det der er logget ind med
                if(HttpContext.Current.Session["Password"].ToString() == Password.Text)
                {   //Hvis kodeordene i de to bokse er ens
                    if (GentagPassword.Text == NytPassword.Text)
                    {
                        //Vi skifter sessionsvariablen til det nye password og disabler boksene
                        HttpContext.Current.Session["Password"] = NytPassword.Text;
                        Password.Enabled = false;
                        NytPassword.Enabled = false;
                        GentagPassword.Enabled = false;
                        Button1.Text = "Skift Password";

                        //Vores forbindelse til SQL databasen
                        SqlDataSource SqlDataSource2 = new SqlDataSource();
                        SqlDataSource2.ID = "SqlDataSource2";
                        this.Page.Controls.Add(SqlDataSource2);
                        SqlDataSource2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
                        SqlDataSource2.UpdateCommand = @"UPDATE [memorialtag].[dbo].[LoginTabel] SET [Adgangskode] = @Adgangskode WHERE [Brugernavn] = '" + User + "'";
                        SqlDataSource2.UpdateParameters.Add("Adgangskode", NytPassword.Text);
                        SqlDataSource2.Update();
                        labError.Text = "Kodeordet er skiftet!";
                        labError.ForeColor = System.Drawing.Color.Green;
                        Password.Text = "";
                        NytPassword.Text = "";
                        GentagPassword.Text = "";
                    }//Giver besked om fejl til brugeren så brugeren selv kan finde ud af at komme videre
                    else { labError.Visible = true; labError.Text = "Nyt Password og Gentag Password er ikke ens!"; labError.ForeColor = System.Drawing.Color.Red; }
                }
                else { labError.Visible = true; labError.Text = "Det angive kodeord passer ikke!"; labError.ForeColor = System.Drawing.Color.Red; }
            }


        }

        //Vores slet bruger knap som ligger i et modal popup vindue
        protected void btnDelete_Click(object sender, EventArgs e)
        {   //Hvis det angive brugerord mathcer det der er brugt til at logge ind
            if (txtDeletePass.Text == HttpContext.Current.Session["Password"].ToString())
            {
                SqlDataSource SqlDataSource1 = new SqlDataSource();                                         //Laver en SqlDataSource
                SqlDataSource1.ID = "SqlDataSource1";                                                       //Giver den et ID
                this.Page.Controls.Add(SqlDataSource1);                                                     //Tilføjer den til siden

                //Her sletter vi alle rækker der har med vores KundeID og gøre fra forskellige tabeller
                SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
                SqlDataSource1.SelectCommand = "SELECT KundeID FROM KundeTabel WHERE Mail = '" + HttpContext.Current.Session["Username"] + "'";
                DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                string KundeID = test.Table.Rows[0]["KundeID"].ToString();
                SqlDataSource1.SelectCommand = "SELECT [TagID] FROM TagTabel WHERE KundeID = '" + KundeID + "'";
                test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                string tagID = test.Table.Rows[0]["tagID"].ToString();

                SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
                SqlDataSource1.DeleteCommand = "DELETE FROM [LoginTabel] WHERE [KundeID] = '" + KundeID + "'";
                SqlDataSource1.Delete();
                SqlDataSource1.DeleteCommand = "DELETE FROM [GravTabel] WHERE [TagID] = '" + tagID + "'";
                SqlDataSource1.Delete();
                SqlDataSource1.DeleteCommand = "DELETE FROM [TagTabel] WHERE [KundeID] = '" + KundeID + "'";
                SqlDataSource1.Delete();
                SqlDataSource1.DeleteCommand = "DELETE FROM [KundeTabel] WHERE [KundeID] = '" + KundeID + "'";
                SqlDataSource1.Delete();

                //Nulstiller sessionsvariablerne
                HttpContext.Current.Session["Username"] = "";
                HttpContext.Current.Session["Password"] = "";

                //Videredirigerer til forsiden
                Response.Redirect("./default.aspx");
            }   //Viser vores fejlbesked til brugeren
            else { labPassError.Visible = true; }
        }
           
        //Sender brugeren videre til produkt siden
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("./products.aspx");
        }
    }
}