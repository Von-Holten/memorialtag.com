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

namespace Teest
{
    public partial class search : System.Web.UI.Page
    {
        //difinere vores sql connection
        SqlConnection vid = new SqlConnection(@"Data Source=13120-CHS\SQLEXPRESS; Initial Catalog = memorialtag; Integrated Security = True");

        protected void Page_Load(object sender, EventArgs e)
        {
            //importere tekst fra default sidens tekstfelt 
            string tet = Convert.ToString(Session["teksten"]);

            //tomme variabler for split (definere s� de kan bruges udenfor if)
            string ws = "";
            string ww = "";
            //deler tet/vores importeret tekst op udfra mellemrum
            int idx = tet.LastIndexOf(' ');

            //hvis der er mellemrum
            if (idx != -1)
            {
                //f�rst ord til ws
                Console.WriteLine(tet.Substring(0, idx));
                ws = (tet.Substring(0, idx));

                //andet ord til ww
                Console.WriteLine(tet.Substring(idx + 1));
                ww = (tet.Substring(idx + 1));
            }
            else
            {
                //hvis der ikke er noget mellemrum så tøm variablerne (cache)
                ws = "";
                ww = "";
            }


            //Matcher begge variabler (ws og ww) op i mod  fx Fornavn. like '%" + ws + "%') betyder alt der matcher/minder om skal "vises/matche" = alt der minder om alt foran ws og alt efter
            String str = "select * from [GravTabel] inner join KirkeTabel on GravTabel.KirkeID = [KirkeTabel].KirkeID where ";
            if (ws != "")
            {
                str = str + "([Fornavn] like '%" + ws + "%') OR ([Efternavn] like '%" + ww + "%') OR ([Fødselsdato] like '%" + ww + "%') OR" +
                " ([Fødselsdato] like '%" + ws + "%') OR ([Dødsdato] like '%" + ws + "%') OR ([Dødsdato] like '%" + ww + "%') OR " +
                " ([Kirke_TB].[KirkeNavn] like '%" + ww + "%') OR ([KirkeNavn] like '%" + ws + "%') OR ([Fødeby] like '%" + ws + "%') OR " +
                "([Fødeby] like '%" + ww + "%') OR ([SidsteBopæl] like '%" + ww + "%') OR ([SidsteBopæl] like '%" + ws + "%') OR ";
            }
            //Matcher på samme måde men kun med et ord/tet
            str = str + "([Fornavn] like '%" + tet + "%') OR ([Efternavn] like '%" + tet + "%') OR ([Fødselsdato] like '%" + tet + "%') OR" +
                " ([Dødsdato] like '%" + tet + "%') OR ([KirkeNavn] like '%" + tet + "%') OR ([Fødeby] like '%" + tet + "%') OR ([SidsteBopæl] like '%" + tet + "%');";


            //åbner vores sql forbindelse
            SqlDataSource SqlDataSource1 = new SqlDataSource();
            SqlDataSource1.ID = "SqlDataSource1";
            this.Page.Controls.Add(SqlDataSource1);
            SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString;
            SqlDataSource1.SelectCommand = str;
            //definer kirke_TB fra database
            //var kirkedata = new Kirke_TB();
            //definer GravTabel fra database
            //var gravdata = new GravTabel();

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









        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}