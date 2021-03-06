﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;

namespace Teest
{
    public partial class editor : System.Web.UI.Page
    {
        //Det der sker når siden indlæses
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["id"];
            //Hvis der ikke er tale om postback (Skal kun køres når siden egentlig indlæses, ikke hver gang der trykkes på en knap og siden refreshes)
            if (!IsPostBack)
            {
                //Vores forbindelse til SQL databasen
                SqlDataSource SqlDataSource1 = new SqlDataSource();
                SqlDataSource1.ID = "SqlDataSource1";
                this.Page.Controls.Add(SqlDataSource1);
                SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
                SqlDataSource1.CancelSelectOnNullParameter = true;
                SqlDataSource1.SelectCommand = "SELECT [Fornavn], [Efternavn], [Fødselsdato], [Dødsdato], [Fødeby], [SidsteBopæl], [Stilling], [NærmestePårørende], [FacebookLink], [MyHeritageLink], [Biografi], [Uddannelse], [Karriere], [Bedrifter] FROM [memorialtag].[dbo].[GravTabel] WHERE (GravID = '" + ID + "')";

                //Error handling med try catch - Her sættes data fra SQL ind i de forskellige tekstbokses
                try
                {
                    DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                    if (test.Table.Rows[0]["Fornavn"].ToString() != null) { txtFornavn.Text = test.Table.Rows[0]["Fornavn"].ToString(); }
                    if (test.Table.Rows[0]["Efternavn"].ToString() != null) { txtEfternavn.Text = test.Table.Rows[0]["Efternavn"].ToString(); }
                    if (test.Table.Rows[0]["Fødselsdato"].ToString() != null) { DateTime føsdag = DateTime.Parse(test.Table.Rows[0]["Fødselsdato"].ToString()); txtFødselsdato.Text = føsdag.ToShortDateString(); }
                    if (test.Table.Rows[0]["Dødsdato"].ToString() != null) { DateTime dødsdag = DateTime.Parse(test.Table.Rows[0]["Dødsdato"].ToString()); txtDødsdato.Text = dødsdag.ToShortDateString(); }
                    if (test.Table.Rows[0]["Fødeby"].ToString() != null) { txtFødeby.Text = test.Table.Rows[0]["Fødeby"].ToString(); }
                    if (test.Table.Rows[0]["SidsteBopæl"].ToString() != null) { txtSidsteBopæl.Text = test.Table.Rows[0]["SidsteBopæl"].ToString(); }
                    if (test.Table.Rows[0]["Stilling"].ToString() != null) { txtStilling.Text = test.Table.Rows[0]["Stilling"].ToString(); }
                    if (test.Table.Rows[0]["NærmestePårørende"].ToString() != null) { txtNærmestePårørende.Text = test.Table.Rows[0]["NærmestePårørende"].ToString(); }
                    if (test.Table.Rows[0]["FacebookLink"].ToString() != null) { txtFacebookLink.Text = test.Table.Rows[0]["FacebookLink"].ToString(); }
                    if (test.Table.Rows[0]["MyHeritageLink"].ToString() != null) { txtMyHeritageLink.Text = test.Table.Rows[0]["MyHeritageLink"].ToString(); }
                    if (test.Table.Rows[0]["Biografi"].ToString() != null) { txtaBiografi.Text = test.Table.Rows[0]["Biografi"].ToString(); }
                    if (test.Table.Rows[0]["Uddannelse"].ToString() != null) { txtaUddannelse.Text = test.Table.Rows[0]["Uddannelse"].ToString(); }
                    if (test.Table.Rows[0]["Karriere"].ToString() != null) { txtaKarriere.Text = test.Table.Rows[0]["Karriere"].ToString(); }
                    if (test.Table.Rows[0]["Bedrifter"].ToString() != null) { txtaBedrifter.Text = test.Table.Rows[0]["Bedrifter"].ToString(); }

                    if (!Directory.Exists(Server.MapPath(@"~/Uploads/" + ID)))
                    {
                        Directory.CreateDirectory(Server.MapPath(@"~/Uploads/" + ID));
                    }
                }
                catch (Exception ex)
                {
                }
            }
            
        }

            //Det der sker når der klikkes på knappen i bunden af siden
            protected void btnAfslutRedigering_Click(object sender, EventArgs e)
            {
            string ID = Request.QueryString["id"];
            //Hvis vores fileupload til profilbilleder har en fil
            if (fileUploadProfile.HasFile)
            {
                //Vi kører test på filstørrelse, filtype - Herefter navngives det uploadede billede profile.jpg og ligges i brugerens mappe
                try
                {
                    int filesize = 5000 * 1024;
                    if (fileUploadProfile.PostedFile.ContentType == "image/jpeg" || fileUploadProfile.PostedFile.ContentType == "image/png")
                    {
                        if (fileUploadProfile.PostedFile.ContentLength < filesize)
                        {
                            string filenameProfile = Path.GetFileName(fileUploadProfile.FileName);
                            fileUploadProfile.SaveAs(Server.MapPath("./Uploads/" + ID + "/profile.jpg"));
                        }
                        else
                        {
                            StatusLabelProfile.Text = "Fejl: Billedet skal være under 4 MB!";
                            StatusLabelProfile.Visible = true;
                        }
                    }
                    else
                    {
                        StatusLabelProfile.Text = "Fejl: Billedet skal være jpg, jpeg eller png!";
                        StatusLabelProfile.Visible = true;
                    }
                }
                catch(Exception ex)
                {
                    StatusLabelProfile.Visible = true;
                    StatusLabelProfile.Text = "Fejl:" + ex.Message;
                }

            }

            //Error Handling med try catch - Her kører vi en update der smider ændringerne over i vores SQL database
            try
            {
                SqlDataSource SqlDataSource2 = new SqlDataSource();
                SqlDataSource2.ID = "SqlDataSource2";
                this.Page.Controls.Add(SqlDataSource2);
                SqlDataSource2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
                SqlDataSource2.UpdateCommand = @"UPDATE [memorialtag].[dbo].[GravTabel] SET [Fornavn] = @Fornavn, [Efternavn] = @Efternavn, [Fødselsdato] = @Fødselsdato, [Dødsdato] = @Dødsdato, [Fødeby] = @Fødeby, [SidsteBopæl] = @SidsteBopæl, [Stilling] = @Stilling, [NærmestePårørende] = @NærmestePårørende, [FacebookLink] = @FacebookLink, [MyHeritageLink] = @MyHeritageLink, [Biografi] = @Biografi, [Uddannelse] = @Uddannelse, [Karriere] = @Karriere, [Bedrifter] = @Bedrifter WHERE ([GravID] = '" +ID+ "')";
                SqlDataSource2.UpdateParameters.Add("Fornavn", txtFornavn.Text);
                SqlDataSource2.UpdateParameters.Add("Efternavn", txtEfternavn.Text);
                SqlDataSource2.UpdateParameters.Add("Fødselsdato", DateTime.ParseExact(txtFødselsdato.Text, "dd-MM-yyyy", null).ToString("MM-dd-yyyy"));    //Her parser vi strengen så den passer med det format vores SQL kan læse
                SqlDataSource2.UpdateParameters.Add("Dødsdato", DateTime.ParseExact(txtDødsdato.Text, "dd-MM-yyyy", null).ToString("MM-dd-yyyy"));          //Her parser vi strengen så den passer med det format vores SQL kan læse
                SqlDataSource2.UpdateParameters.Add("Fødeby", txtFødeby.Text);
                SqlDataSource2.UpdateParameters.Add("SidsteBopæl", txtSidsteBopæl.Text);
                SqlDataSource2.UpdateParameters.Add("Stilling", txtStilling.Text);
                SqlDataSource2.UpdateParameters.Add("NærmestePårørende", txtNærmestePårørende.Text);
                SqlDataSource2.UpdateParameters.Add("FacebookLink", txtFacebookLink.Text);
                SqlDataSource2.UpdateParameters.Add("MyHeritageLink", txtMyHeritageLink.Text);
                SqlDataSource2.UpdateParameters.Add("Biografi", txtaBiografi.Text);
                SqlDataSource2.UpdateParameters.Add("Uddannelse", txtaUddannelse.Text);
                SqlDataSource2.UpdateParameters.Add("Karriere", txtaKarriere.Text);
                SqlDataSource2.UpdateParameters.Add("Bedrifter", txtaBedrifter.Text);
                SqlDataSource2.Update();
            }
            catch(Exception ex)
            {
                labError.Visible = true;
                labError.Text = ex.ToString();
                //labError.Text = "Der er desværre sket en fejl!";
            }
        }

        //Dette køres hver gang en fil bliver uploadet - Vi kigger på om der er plads til flere billeder og giver billedet det rigtige navn (max 8 billeder)
        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxFileUploadEventArgs e)
        {
            string ID = Request.QueryString["id"];
            string filename = e.FileName;
            if (filename.Contains(".jpg") || filename.Contains(".jpeg") || filename.Contains(".png"))
            {
                if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic1.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic1.jpg")); }
                else if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic2.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic2.jpg")); }
                else if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic3.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic3.jpg")); }
                else if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic4.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic4.jpg")); }
                else if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic5.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic5.jpg")); }
                else if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic6.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic6.jpg")); }
                else if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic7.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic7.jpg")); }
                else if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/pic8.jpg")))) { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/pic8.jpg")); }
                else { labError.Text = "Du må max uploade 8 billeder i alt!"; }
            }
            //Vi kigger også efter en videofil
            else if (filename.Contains(".mp4")) { if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/vid1.mp4"))))
                { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/vid1.mp4"));}
                else { labError.Text = "Du må max uploade 1 video!"; }
            } 
            //Og også efter en lydfil
            else if (filename.Contains(".mp3")) { if (!(System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/aud1.mp3"))))
                { AjaxFileUpload1.SaveAs(Server.MapPath("./Uploads/" + ID + "/aud1.mp3")); }
                else { labError.Text = "Du må max uploade 1 lydfil!"; }
            }
        }
    }
}