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
            SqlDataSource SqlDataSource1 = new SqlDataSource();                                         //Laver en SqlDataSource
            SqlDataSource1.ID = "SqlDataSource1";                                                       //Giver den et ID
            this.Page.Controls.Add(SqlDataSource1);                                                     //Tilføjer den til siden
            try
            {
                if (HttpContext.Current.Session["Username"].ToString() != "")
                {                  
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
                            labFornavn.Text = test.Table.Rows[0]["Fornavn"].ToString();
                            labEfternavn.Text = test.Table.Rows[0]["Efternavn"].ToString();
                            labTelefon.Text = test.Table.Rows[0]["Telefon"].ToString();
                            labEmail.Text = test.Table.Rows[0]["Mail"].ToString();
                            labLand.Text = test.Table.Rows[0]["LandNavn"].ToString();
                            labBy.Text = test.Table.Rows[0]["ByNavn"].ToString();
                            labPostnr.Text = test.Table.Rows[0]["Postnummer"].ToString();
                            labVej.Text = test.Table.Rows[0]["Vej"].ToString();
                            kundeID.Text = "Kunde ID: " + test.Table.Rows[0]["KundeID"].ToString();
                            string TagID = test.Table.Rows[0]["TagID"].ToString();
                            string GravID = test.Table.Rows[0]["GravID"].ToString();

                            //Ny SQL Select kommando på grav tabellen istedet for kunde tabellen
                            SqlDataSource1.SelectCommand = "SELECT [Fornavn], [Efternavn], [Fødselsdato], [Dødsdato], [GravID] FROM [GravTabel] WHERE [GravID] = '" + GravID + "'";
                            DataView test2 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                            if (test2.Table.Rows[0]["GravID"].ToString() == null)
                            {
                                Response.Redirect("./Error.aspx");
                            }
                        }
                        catch
                        {
                            Response.Redirect("./Error.aspx");
                        }
                    }
                }
            }
            catch { Response.Redirect("./Error.aspx"); }   
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string User = Convert.ToString(Session["Username"]);
            if (Button1.Text != "Godkend")
            {
                Password.Enabled = true;
                NytPassword.Enabled = true;
                GentagPassword.Enabled = true;
                Button1.Text = "Godkend";
            }
            else
            {
                if(HttpContext.Current.Session["Password"].ToString() == Password.Text)
                {
                    if (GentagPassword.Text == NytPassword.Text)
                    {
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
                    }
                    else { labError.Visible = true; labError.Text = "Nyt Password og Gentag Password er ikke ens!"; labError.ForeColor = System.Drawing.Color.Red; }
                }
                else { labError.Visible = true; labError.Text = "Det angive kodeord passer ikke!"; labError.ForeColor = System.Drawing.Color.Red; }
            }


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtDeletePass.Text == HttpContext.Current.Session["Password"].ToString())
            {
                Response.Redirect("./default.aspx");
            }
            else { labPassError.Visible = true; }
        }
    }
}