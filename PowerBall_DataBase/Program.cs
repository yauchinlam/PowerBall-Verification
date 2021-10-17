using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PowerBall_DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            string myConnection = "Data Source=DESKTOP-PJ6QB08;" +
            "Integrated Security=True;" +
            "Connect " +
            "Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            int lottoRange = 69;
            int redPowerball = 26;
            SqlCommand cmd = new SqlCommand();
            List<int> drawing = new List<int>();
            string One = "[dbo].[Powerball_One]";
            string Two = "[dbo].[Powerball_Two]";
            string Three = "[dbo].[Powerball_Three]";
            string Four = "[dbo].[Powerball_Four]";
            string Five = "[dbo].[Powerball_Five]";
            string All = "[dbo].[Powerball_All]";
            using (SqlConnection con = new SqlConnection(myConnection))
            {
                string cmdText = String.Empty;
                for(int a =1;a<=lottoRange;a++)
                {
                    cmdText =
                        String.Format("INSERT INTO [dbo].[Powerball_One] " +
                        "(One) VALUES ({0})", a);
                    ToDB(cmdText, con);
                    for(int b =1;b<=lottoRange-1;b++)
                    {
                        drawing.Add(a);
                        drawing.Add(b);
                        if (!(a == b) && CheckUniqueness(drawing, Two, con))
                        {
                            cmdText =
                            String.Format("INSERT INTO [dbo].[Powerball_Two] " +
                            "(One, Two, Set) VALUES ({0},{1}, '({0},{1})')", a, b);
                            ToDB(cmdText, con);
                            for (int c = 1; c <= lottoRange - 2; c++)
                            {
                                drawing.Add(c);
                                if(!(c==a || c==b) && CheckUniqueness(drawing, Three, con))
                                {
                                    cmdText =
                                    String.Format("INSERT INTO [dbo].[Powerball_Three] " +
                                    "(One, Two, Three, Set) VALUES ({0},{1},{2},'({0},{1},{2})')", a, b, c);
                                    ToDB(cmdText, con);
                                    for(int d =1; d<= lottoRange-3; d++)
                                    {
                                        drawing.Add(d);
                                        if(!(d==a || d==b || d==c) && CheckUniqueness(drawing, Four, con))
                                        {
                                            cmdText =
                                            String.Format("INSERT INTO [dbo].[Powerball_Four] " +
                                            "(One, Two, Three, Four, Set) VALUES ({0},{1},{2},{3}, ({0},{1},{2},{3}))", a, b, c, d);
                                            ToDB(cmdText, con);
                                            for(int e =1; e<= lottoRange -4; e++)
                                            {
                                                drawing.Add(e);
                                                if(!(e==d || e==c|| e==b || e==a) && CheckUniqueness(drawing, Five, con))
                                                {
                                                    cmdText =
                                                    String.Format("INSERT INTO [dbo].[Powerball_Five] " +
                                                    "(One, Two, Three, Four, Five, Set) VALUES ({0},{1},{2}, {3}, {4}, ({0},{1},{2}, {3}, {4}))", a, b, c, d);
                                                    ToDB(cmdText, con);
                                                    for(int p =1; p <= redPowerball; p++)
                                                    {
                                                        drawing.Add(p);
                                                        if (CheckUniqueness(drawing, All, con))
                                                        {
                                                            cmdText =
                                                            String.Format("INSERT INTO [dbo].[Powerball_All] " +
                                                            "(One, Two, Three, Four, Five, Set) VALUES ({0},{1},{2}, {3}, {4}, {5}, ({0},{1},{2}, {3}, {4}, {5}))", a, b, c, d, p);
                                                            ToDB(cmdText, con);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            void ToDB(string cmdtext, SqlConnection _con)
            {
                cmd.CommandText = cmdtext;
                cmd.Connection = _con;
                cmd.ExecuteNonQuery();
            }

            bool CheckUniqueness( List<int> drawing, string table, SqlConnection _con)
            {
                Dictionary<int,bool> checker = new Dictionary<int, bool>();
                DataTable datatable = new DataTable();
                int i = 0;
                foreach (int draw in drawing)
                {
                    string queryString =
                        String.Format("SELECT [Set] FROM {0} WHERE LIKE '%{1}%' ", table, draw);
                    cmd.Connection = _con;
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
}
