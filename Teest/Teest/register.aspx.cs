using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Text;

namespace Teest
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Det der sker når der trykkes på knappen
        protected void btnPayment_Click(object sender, EventArgs e)
        {
            //Error Handling med Try Catch
            try
            {
                //Vores forbindelse til SQL Databasen
                SqlDataSource SqlDataSource1 = new SqlDataSource();
                SqlDataSource1.ID = "SqlDataSource1";
                this.Page.Controls.Add(SqlDataSource1);
                SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
                SqlDataSource1.SelectCommand = "Select Mail FROM KundeTabel";
                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                //Her ser vi om emailen allerede er i brug
                if (dv[0]["Mail"].ToString().Trim().Equals(txtEmail.Text)) { labError.Visible = true; labError.Text = "Den angive email eksisterer allerede."; }
                else
                {
                    SqlDataSource1.SelectCommand = "SELECT COUNT(1) FROM LoginTabel WHERE Brugernavn='" + txtEmail.Text + "'"; //Vores SELECT statement
                    DataView validation = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);       //Laver et test dataview til vores template
                    int count = Convert.ToInt32(validation.Table.Rows[0][0]);

                    if (count == 0)
                    {
                        //Her kører vi vores Insert kommando og indsætter brugerens data og dermed opretter en ny bruger
                        SqlDataSource1.InsertCommand = "INSERT INTO [KundeTabel] (Fornavn, Efternavn, Telefon, Mail, LandID, Postnummer, Vej) VALUES (@Fornavn, @Efternavn, @Telefon, @Mail, @LandID, @Postnummer, @Vej)";
                        SqlDataSource1.InsertParameters.Add("Fornavn", txtFornavn.Text);
                        SqlDataSource1.InsertParameters.Add("Efternavn", txtEfternavn.Text);
                        SqlDataSource1.InsertParameters.Add("Telefon", txtTelefon.Text);
                        SqlDataSource1.InsertParameters.Add("Mail", txtEmail.Text);
                        SqlDataSource1.InsertParameters.Add("LandID", "DK");
                        SqlDataSource1.InsertParameters.Add("Postnummer", txtPostnr.Text);
                        SqlDataSource1.InsertParameters.Add("Vej", txtVej.Text);
                        SqlDataSource1.Insert();


                        //Her finder vi kundens KundeID
                        SqlDataSource1.SelectCommand = "SELECT KundeID FROM KundeTabel WHERE Mail = '" + txtEmail.Text + "'";
                        DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);       //Laver et test dataview til vores template
                        String KundeID = test.Table.Rows[0]["KundeID"].ToString();                             //Tester om der er lagt data i vores dataview

                        //Her opretter vi et Login til kunden
                        SqlDataSource1.InsertCommand = "INSERT INTO [LoginTabel] (KundeID, Brugernavn, Adgangskode) VALUES (@KundeID, @Brugernavn, @Adgangskode)";
                        SqlDataSource1.InsertParameters.Add("KundeID", KundeID);
                        SqlDataSource1.InsertParameters.Add("Brugernavn", txtEmail.Text);
                        SqlDataSource1.InsertParameters.Add("Adgangskode", txtPassword.Text);
                        SqlDataSource1.Insert();

                        //Her sætter vi vores sessions variabel til brugernavnet
                        Session["username"] = txtEmail.Text;

                        //Opretter en XML fil hvis den ikke allerede findes
                        if (!File.Exists(Server.MapPath("./XML/register.xml")))
                        {
                            XmlTextWriter xwriter = new XmlTextWriter(Server.MapPath("./XML/register.xml"), Encoding.UTF8);
                            xwriter.Formatting = Formatting.Indented;
                            xwriter.WriteStartElement("Salg");
                            xwriter.WriteStartElement("Kunde");
                            xwriter.WriteStartElement("Navn");
                            xwriter.WriteString(txtFornavn.Text + " " + txtEfternavn.Text);
                            xwriter.WriteEndElement();
                            xwriter.WriteStartElement("Mail");
                            xwriter.WriteString(txtEmail.Text);
                            xwriter.WriteEndElement();
                            xwriter.WriteStartElement("Telefon");
                            xwriter.WriteString(txtTelefon.Text);
                            xwriter.WriteEndElement();
                            xwriter.WriteStartElement("Vej");
                            xwriter.WriteString(txtVej.Text);
                            xwriter.WriteEndElement();
                            xwriter.WriteEndElement();
                            xwriter.WriteEndElement();
                            xwriter.Close();
                        }
                        else
                        {   //Skriver til en eksisterende XML fil (append)
                            string path = Server.MapPath("./XML/register.xml");
                            XDocument doc = XDocument.Load(path);
                            XElement salg = doc.Element("Salg");
                            salg.Add(new XElement("Kunde",
                                       new XElement("Navn", txtFornavn.Text + " " + txtEfternavn.Text),
                                       new XElement("Mail", txtEmail.Text),
                                       new XElement("Telefon", txtTelefon.Text),
                                       new XElement("Vej", txtVej.Text),
                                       new XElement("Tidspunkt", DateTime.Now.ToString("dddd, dd MMMM yyyy"))
                                       ));
                            doc.Save(Server.MapPath("./XML/register.xml"));
                        }
                        Response.Redirect("./products.aspx");
                    }

                    else { labError.Visible = true; labError.Text = "Det angive login eksisterer allerede."; }
                }
            }
            catch (Exception Ex)
            {
                labError.Visible = true;
                labError.Text = Ex.ToString();
            }
        }
    }
}