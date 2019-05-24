using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Teest
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    if (HttpContext.Current.Session["Username"].ToString() != "")
                    {
                        SqlDataSource SqlDataSource1 = new SqlDataSource();                                         //Laver en SqlDataSource
                        SqlDataSource1.ID = "SqlDataSource1";                                                       //Giver den et ID
                        this.Page.Controls.Add(SqlDataSource1);                                                     //Tilføjer den til siden
                        SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
                        SqlDataSource1.SelectCommand = "SELECT COUNT(1) FROM LoginTabel WHERE Brugernavn= '" + HttpContext.Current.Session["Username"].ToString() + "' AND Adgangskode= '" + HttpContext.Current.Session["Password"].ToString() + "'"; //Vores SELECT statement
                        DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);           //Laver et test dataview til vores template
                        int count = Convert.ToInt32(test.Table.Rows[0][0]);
                        if (count == 1)
                        {
                            txtUsername.Enabled = false;
                            txtUsername.Text = HttpContext.Current.Session["Username"].ToString();
                            txtPassword.Enabled = false;
                            btnLogin.Text = "Gå til kontrolpanelet";
                        }
                    }
                }
                catch{}
            }    
        }

        //Det der sker når der klikkes på login knappen
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Session["Username"] = txtUsername.Text.Trim();
            HttpContext.Current.Session["Username"] = txtUsername.Text.Trim();
            HttpContext.Current.Session["Password"] = txtPassword.Text.Trim();

            if (btnLogin.Text == "Gå til kontrolpanelet")
                {
                    if (HttpContext.Current.Session["Username"].ToString() == "admin@test.dk")
                    {
                        Response.Redirect("./admin.aspx");
                    }
                Response.Redirect("./controlpanel.aspx");
                }
                //Åbner forbindelsen
                //Forbindelse til SQL Databasen
                SqlDataSource SqlDataSource1 = new SqlDataSource();                                             //Laver en SqlDataSource
                SqlDataSource1.ID = "SqlDataSource1";                                                           //Giver den et ID
                this.Page.Controls.Add(SqlDataSource1);                                                         //Tilføjer den til siden
                SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
                SqlDataSource1.SelectCommand = "SELECT COUNT(1) FROM LoginTabel WHERE Brugernavn= '" + txtUsername.Text + "' AND Adgangskode= '" + txtPassword.Text + "'"; //Vores SELECT statement
                DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);               //Laver et test dataview til vores template
                int count = Convert.ToInt32(test.Table.Rows[0][0]);
                if (count == 1)
                {
                //Vi opretter en sessions variabel som indeholder vores brugernavn

                

                if(remember.Checked == true)
                {
                    //Hvis husk mig er checked opretter vi en coockie
                    HttpCookie loginCookie = new HttpCookie("Login");
                    loginCookie.Values["Username"] = txtUsername.Text;
                    loginCookie.Values["Password"] = txtPassword.Text;
                    loginCookie.Path = Request.ApplicationPath;
                    loginCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(loginCookie);
                    Response.Redirect("controlpanel.aspx");
                }

                //Vi videresender brugeren til kontrolpanelet når hvis de har en eksisterende bruger
                Response.Redirect("./controlpanel.aspx");
                }
                else { labelLoginError.Visible = true; }
        }

        //Sender brugeren videre til registreringssiden når der trykkes på knappen
        protected void btnBestil_Click(object sender, EventArgs e)
        {
            Response.Redirect("./register.aspx");
        }

        //Sendere brugeren videre til søgesiden når der trykkes på knappen (Gemmer søgestrengen i en sessions variabel)
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["teksten"] = Search.Text;
            Response.Redirect("./search.aspx");
        }

        protected void btnPasswordReset_Click(object sender, EventArgs e)
        {
            SqlDataSource SqlDataSource1 = new SqlDataSource();                                         //Laver en SqlDataSource
            SqlDataSource1.ID = "SqlDataSource1";                                                       //Giver den et ID
            this.Page.Controls.Add(SqlDataSource1);                                                     //Tilføjer den til siden
            SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString; //Vores connection string som er defineret i web.config
            SqlDataSource1.SelectCommand = "SELECT COUNT(1) FROM LoginTabel WHERE Brugernavn= '" + txtForgotPass.Text + "'"; //Vores SELECT statement
            DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);           //Laver et test dataview til vores template
            int count = Convert.ToInt32(test.Table.Rows[0][0]);
            SqlDataSource1.SelectCommand = "SELECT Adgangskode FROM LoginTabel WHERE Brugernavn= '" + txtForgotPass.Text + "'"; //Vores SELECT statement
            test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);                    //Laver et test dataview til vores template
            if (count == 1)
            {
                MailMessage mm = new MailMessage("chsdk89@gmail.com", "chsecurity@live.dk");
                //MailMessage mm = new MailMessage("chsdk89@gmail.com", txtForgotPass.Text.Trim())
                mm.Subject = "Glemt Kodeord";
                mm.Body = "Dit kodeord er: " + test.Table.Rows[0][0].ToString() + "<br>Med venlig hilsen Memorial Tag.";
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
            else { labPassError.Visible = true; }
        }
    }
}