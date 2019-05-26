using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace teest.models
{
    public class DataAccess
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["memorialtagConnectionString"].ConnectionString.ToString();
            }
        }
        
        public static SqlParameter AddParameter(string parameterName, object value, SqlDbType DbType, int size)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.Value = value.ToString();
            param.SqlDbType = DbType;
            param.Size = size;
            param.Direction = ParameterDirection.Input;
            return param;
        }

        public static DataTable ExecuteDT(string ProcedureName, SqlParameter[] Params)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = ProcedureName;
            cmd.Parameters.AddRange(Params);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adopter = new SqlDataAdapter(cmd);
            DataTable dTable = new DataTable();

            try
            {
                adopter.Fill(dTable);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                adopter.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
                conn.Dispose();
            }
            return dTable;
        }
    }
}