using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBallDatabase
{
    public static class Unique
    {
        public static bool Check(List<int> drawing, string table, SqlConnection _con)
        {
                Dictionary<int, bool> checker = new Dictionary<int, bool>();
                DataTable datatable = new DataTable();
                int i = 0;
                foreach (int draw in drawing)
                {
                    string queryString =
                        String.Format("SELECT [SetOfNumbers] FROM {0} WHERE [SetOfNumbers] LIKE '%{1}%'", table, draw);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(
                        queryString, _con);
                    adapter.Fill(datatable);
                    checker[i] = (datatable.Rows.Count == 0);
                    i++;
                }
                bool isUnique = !checker.ContainsValue(false);
                return (isUnique);
            
        }
    }
}
