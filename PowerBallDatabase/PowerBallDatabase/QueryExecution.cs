using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBallDatabase
{
    public static class QueryExecution
    {
        public static void ToDB(string cmdtext, SqlConnection _con)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdtext;
            cmd.Connection = _con;
            cmd.ExecuteNonQuery();
        }
    }
}
