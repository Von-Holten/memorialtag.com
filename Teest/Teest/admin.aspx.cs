using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using teest.models;

namespace teest
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string getsite = "select * from [GravTabel] inner join KirkeTabel on GravTabel.KirkeID = [KirkeTabel].KirkeID where ([Valid] like '0') ";

            SqlDataSource SqlDataSource1 = new SqlDataSource();
            SqlDataSource1.ID = "SqlDataSource1";
            this.Page.Controls.Add(SqlDataSource1);
            SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
            SqlDataSource1.SelectCommand = getsite;

            try
            {
                DataView test = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            }
            catch
            {
                labError.Text = "Din søgning gav ingen resultater!";
                labError.Visible = true;
            }
        }
    }
}