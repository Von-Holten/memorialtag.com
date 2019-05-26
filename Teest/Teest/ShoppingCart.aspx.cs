using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace teest
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string Conn = WebConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(Conn);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);

            con.Close();
            return dt;
        }
        protected void btnKøb_Click(object sender, EventArgs e)
        {
            string ProduktID = Page.Request.QueryString["produktid"].ToString();
            string ProductQuantity = "1";

            DataListItem currentItem = (sender as Button).NamingContainer as DataListItem;

            if (Session["MyCart"] != null)
            {
                DataTable dt = (DataTable)Session["MyCart"];
                var checkProduct = dt.AsEnumerable().Where(r => r.Field<string>("ProduktID") == ProduktID);
                if (checkProduct.Count() == 0)
                {
                    string query = "select * from ProduktTabel WHERE ProduktID = " + ProduktID + "";
                    DataTable dtProducts = GetData(query);

                    DataRow dr = dt.NewRow();
                    dr["ProduktID"] = ProduktID;
                    dr["ProduktNavn"] = Convert.ToString(dtProducts.Rows[0]["ProduktNavn"]);
                    dr["ProduktPris"] = Convert.ToString(dtProducts.Rows[0]["ProduktPris"]);
                    dr["ProduktBeskrivelse"] = Convert.ToString(dtProducts.Rows[0]["ProduktBeskrivelse"]);
                    dr["image"] = Convert.ToString(dtProducts.Rows[0]["image"]);
                    dt.Rows.Add(dr);

                    Session["MyCart"] = dt;

                }
            }
            else
            {
                string query = "SELECT * FROM ProduktTabel WHERE ProduktID = " + ProduktID + "";
                DataTable dtProducts = GetData(query);

                DataTable dt = new DataTable();

                dt.Columns.Add("ProduktID", typeof(string));
                dt.Columns.Add("ProduktNavn", typeof(string));
                dt.Columns.Add("ProduktPris", typeof(string));
                dt.Columns.Add("ProduktBeskrivelse", typeof(string));
                dt.Columns.Add("image", typeof(string));

                DataRow dr = dt.NewRow();
                dr["ProduktID"] = ProduktID;
                dr["ProduktNavn"] = Convert.ToString(dtProducts.Rows[0]["ProduktNavn"]);
                dr["ProduktPris"] = Convert.ToString(dtProducts.Rows[0]["ProduktPris"]);
                dr["ProduktBeskrivelse"] = Convert.ToString(dtProducts.Rows[0]["ProduktBeskrivelse"]);
                dr["image"] = Convert.ToString(dtProducts.Rows[0]["image"]);
                dt.Rows.Add(dr);

                Session["MyCart"] = dt;
            }
        }

        
    }
}