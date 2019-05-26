using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace teest.models
{
    public class IndkøbsKurv
    {

        public string KundeID;
        public string Antal;
        public string ProduktID;

        public void BuyProduct()
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = models.DataAccess.AddParameter("@KundeID", KundeID, System.Data.SqlDbType.VarChar, 50);
            parameters[1] = models.DataAccess.AddParameter("@Antal", Antal, System.Data.SqlDbType.Int, 3);
            parameters[2] = models.DataAccess.AddParameter("@ProduktID", ProduktID, System.Data.SqlDbType.VarChar, 50);

            DataTable dt = models.DataAccess.ExecuteDT("købprodukt", parameters);
        }
    }
}