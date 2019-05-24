using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using teest.models;
using System.IO;

namespace Teest
{
    public partial class template : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Hvis brugeren er administrator
            try
            {
                string username = HttpContext.Current.Session["Username"].ToString();
                if (username == "admin@test.dk")
                {
                    RadioButton1.Visible = true;
                    RadioButton2.Visible = true;
                    Button1.Visible = true;
                    Label1.Visible = true;
                }
            }
            catch
            {
                
            }

            //Finder IDet fra vores QueryString (?id=X)
            string ID = Request.QueryString["id"];

            SqlDataSource SqlDataSource1 = new SqlDataSource();                                         //Laver en SqlDataSource
            SqlDataSource1.ID = "SqlDataSource1";                                                       //Giver den et ID
            this.Page.Controls.Add(SqlDataSource1);                                                     //Tilføjer den til siden
            SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
            SqlDataSource1.SelectCommand = "SELECT Valid FROM GravTabel WHERE GravID = '" + ID + "'";
            try
            {
                DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);       //Laver et test dataview til vores template
                string valid = test.Table.Rows[0]["Valid"].ToString();                                  //Tester om der er lagt data i vores dataview
                if (valid == "false")
                {
                    Response.Redirect("./NotApproved.aspx");
                }
            }
            catch
            {
                Response.Redirect("./NotApproved.aspx");
            }


            //Hvis der er uploadet et profilbillede vised dette og baggrunden gøres hvid
            if (System.IO.File.Exists(Server.MapPath("/Uploads/" + ID + "/profile.jpg")))               //Ser om der ligger et profilbillede i brugerens mappe
            { imgProfil.Visible = true; imgProfil.ImageUrl = "./Uploads/" + ID + "/profile.jpg";        //Gør billedet synligt og indsætter det 
            billedeSkift.Attributes.Add("style", "background:color(white/" + ID + "/profile.jpg); "); } //Skifter bagrunden fra placeholder til hvid

            //Forbindelse til SQL Databasen
            SqlDataSource1.SelectCommand = "SELECT [GravTabel].[Fornavn], [GravTabel].[Efternavn], [KirkeTabel].[KirkeNavn], [GravTabel].[GravID], [GravTabel].[Fødselsdato], [GravTabel].[Dødsdato], [GravTabel].[Fødeby], [GravTabel].[SidsteBopæl], [GravTabel].[Stilling], [GravTabel].[NærmestePårørende], [GravTabel].[FacebookLink], [GravTabel].[MyHeritageLink], [GravTabel].[Biografi], [GravTabel].[Uddannelse], [GravTabel].[Karriere], [GravTabel].[Bedrifter], [GravTabel].[Valid] FROM [GravTabel] INNER JOIN KirkeTabel ON GravTabel.KirkeID = KirkeTabel.KirkeID WHERE ([GravID] = " + ID + ")"; //Vores SELECT statement

            //Error handling med try catch
            try
            {
                DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);       //Laver et test dataview til vores template
                labFornavn.Text = test.Table.Rows[0]["Fornavn"].ToString();                             //Tester om der er lagt data i vores dataview
            }
            catch
            {
                Response.Redirect("./Error.aspx");                                                    //Hvis ikke der ligger data i vores dataview ...
            }

            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);             //Laver det egentlige dataview der bruges

            //Indsætter teksten fra placeringen i vores dataview i de valgte textboxes
            labFuldeNavn.Text = dv.Table.Rows[0]["Fornavn"].ToString() + " " + dv.Table.Rows[0]["Efternavn"].ToString();
            labKirkegård.Text = "Kirkegård: " + dv.Table.Rows[0]["KirkeNavn"].ToString();
            labID.Text = "ID: " + dv.Table.Rows[0]["GravID"].ToString();
            labFornavn.Text = dv.Table.Rows[0]["Fornavn"].ToString();
            labEfternavn.Text = dv.Table.Rows[0]["Efternavn"].ToString();
            DateTime føsdag = DateTime.Parse(dv.Table.Rows[0]["Fødselsdato"].ToString());
            labFødselsdato.Text = føsdag.ToShortDateString();
            tidslinjeFødselsår.Text = føsdag.Year.ToString();
            DateTime dødsdag = DateTime.Parse(dv.Table.Rows[0]["Dødsdato"].ToString());
            labDødsdato.Text = dødsdag.ToShortDateString();
            tidslinjeDødsår.Text = dødsdag.Year.ToString();
            labFødeby.Text = dv.Table.Rows[0]["Fødeby"].ToString();
            labSidsteBopæl.Text = dv.Table.Rows[0]["SidsteBopæl"].ToString();
            labStilling.Text = dv.Table.Rows[0]["Stilling"].ToString();
            labNærmestePårørende.Text = dv.Table.Rows[0]["NærmestePårørende"].ToString();
            btnLinkFB.PostBackUrl = dv.Table.Rows[0]["FacebookLink"].ToString();
            btnLinkMH.PostBackUrl = dv.Table.Rows[0]["MyHeritageLink"].ToString();
            labBiografi.Text = dv.Table.Rows[0]["Biografi"].ToString();
            labUddannelse.Text = dv.Table.Rows[0]["Uddannelse"].ToString();
            labKarriere.Text = dv.Table.Rows[0]["Karriere"].ToString();
            labBedrifter.Text = dv.Table.Rows[0]["Bedrifter"].ToString();
            Label1.Text = dv.Table.Rows[0]["Valid"].ToString();

            if (Label1.Text == "True")
            {
                Label1.Text = "Siden er godkendt";
            }
            else
            {
                Label1.Text = "Siden er ikke godkendt";
            }

            //Collapser links, labels og boxes hvis de er tomme
            if (btnLinkFB.PostBackUrl == "") { btnLinkFB.Visible = false; }
            if (btnLinkMH.PostBackUrl == "") { btnLinkMH.Visible = false; }
            if (labBiografi.Text == "") { labBiografi.Visible = false && labBiografiOverskrift.Visible == false; }
            if (labUddannelse.Text == "") { labUddannelse.Visible = false && labUdannelseOverskrift.Visible == false; }
            if (labKarriere.Text == "") { labKarriere.Visible = false && labKarriereOverskrift.Visible == false; }
            if (labBedrifter.Text == "") { labBedrifter.Visible = false && labBedrifterOverskrift.Visible == false; }
            if (labBiografi.Text == "" && labUddannelse.Text == "" && labKarriere.Text == "" && labBedrifter.Text == "" ) { Biografi.Visible = false; }

            PopulateImages();
        }

        //Funktion til at se om billederne eksistere, hvis ja gøres de synlige i galleriet
        private void PopulateImages()
        {
            string ID = Request.QueryString["id"];
            string path = "./Uploads/" + ID + "/pic";
            if (File.Exists(Server.MapPath("./Uploads/" +ID+ "/profile.jpg"))) { bigImage.ImageUrl = "./Uploads/" + ID + "/profile.jpg"; }
            if (File.Exists(Server.MapPath(path + "1.jpg"))) { Image1.Visible = true; Image1.ImageUrl = path + "1.jpg"; }
            if (File.Exists(Server.MapPath(path + "2.jpg"))) { Image2.Visible = true; Image2.ImageUrl = path + "2.jpg"; }
            if (File.Exists(Server.MapPath(path + "3.jpg"))) { Image3.Visible = true; Image3.ImageUrl = path + "3.jpg"; }
            if (File.Exists(Server.MapPath(path + "4.jpg"))) { Image4.Visible = true; Image4.ImageUrl = path + "4.jpg"; }
            if (File.Exists(Server.MapPath(path + "5.jpg"))) { Image5.Visible = true; Image5.ImageUrl = path + "5.jpg"; }
            if (File.Exists(Server.MapPath(path + "6.jpg"))) { Image6.Visible = true; Image6.ImageUrl = path + "6.jpg"; }
            if (File.Exists(Server.MapPath(path + "7.jpg"))) { Image7.Visible = true; Image7.ImageUrl = path + "7.jpg"; }
            if (File.Exists(Server.MapPath(path + "8.jpg"))) { Image8.Visible = true; Image8.ImageUrl = path + "8.jpg"; }
            if (File.Exists(Server.MapPath("./Uploads/" + ID + "/profile.jpg"))) { Image9.Visible = true; Image9.ImageUrl = "./Uploads/" + ID + "/profile.jpg"; }
            if (File.Exists(Server.MapPath("./Uploads/" + ID + "/vid1.mp4"))) { Image10.Visible = true; Image10.ImageUrl = "./Images/picVideo.jpg"; }
            if (File.Exists(Server.MapPath("./Uploads/" + ID + "/aud1.mp3"))) { Image11.Visible = true; Image11.ImageUrl = "./Images/picVideo.jpg"; }
        }

        //Smider billedet op i den store kasse når der klikkes på det (Video og lydfil afspilles automatisk)
        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image1.ImageUrl;
        }
        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image2.ImageUrl;
        }
        protected void Image3_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image3.ImageUrl;
        }
        protected void Image4_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image4.ImageUrl;
        }
        protected void Image5_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image5.ImageUrl;
        }
        protected void Image6_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image6.ImageUrl;
        }
        protected void Image7_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image7.ImageUrl;
        }
        protected void Image8_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image8.ImageUrl;
        }

        protected void Image9_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = true;
            video.Visible = false;
            bigImage.ImageUrl = Image9.ImageUrl;
        }

        protected void Image10_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = false;
            video.Visible = true;
            video.Src = "./Uploads/1/vid1.mp4";
        }
        protected void Image11_Click(object sender, ImageClickEventArgs e)
        {
            bigImage.Visible = false;
            video.Visible = true;
            video.Src = "./Uploads/1/aud1.mp3";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string ID = Request.QueryString["id"];
            SqlDataSource SqlDataSource2 = new SqlDataSource();
            SqlDataSource2.ID = "SqlDataSource2";
            this.Page.Controls.Add(SqlDataSource2);
            SqlDataSource2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;


            if (RadioButton1.Checked == true)
            {



                SqlDataSource2.UpdateCommand = "UPDATE [memorialtag].[dbo].[GravTabel] SET Valid = '1' where GravID ='" + ID + "'";
                //SqlDataSource2.UpdateParameters.Add("Vaild", "1"); 

                SqlDataSource2.Update();
            }
            if (RadioButton2.Checked == true)
            {

                SqlDataSource2.UpdateCommand = "UPDATE [memorialtag].[dbo].[GravTabel] SET Valid = '0' where GravID ='" + ID + "'";
                //SqlDataSource2.UpdateParameters.Add("Vaild", "1"); 

                SqlDataSource2.Update();

            }
        }
    }
}