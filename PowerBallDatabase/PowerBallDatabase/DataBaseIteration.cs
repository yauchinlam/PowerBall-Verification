using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PowerBallDatabase
{


    public static class DataBaseIteration
    {
        private static string myConnection = "Data Source=DESKTOP-PJ6QB08;" +
            "Integrated Security=True;" +
            "Connect " +
            "Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static int lottoRange = 69;
        private static int redPowerball = 26;
        private static SqlCommand cmd = new SqlCommand();
        private static List<int> drawing = new List<int>();
        //string One = "[dbo].[Powerball_One]";
        private static string Two = "[dbo].[Powerball_Two]";
        private static string Three = "[dbo].[Powerball_Three]";
        private static string Four = "[dbo].[Powerball_Four]";
        private static string Five = "[dbo].[Powerball_Five]";
        private static string All = "[dbo].[Powerball_All]";
        private static string cmdText = String.Empty;
        public static void LoopThrough()
        {
            using (SqlConnection con = new SqlConnection(myConnection))
            {
                ForEachA(con, lottoRange);
            }
        }

        private static void ForEachA(SqlConnection con, int lottoRange)
        {
            for (int a = 1; a <= lottoRange; a++)
            {
                drawing.Add(a);
                cmdText =
                    String.Format("INSERT INTO [dbo].[Powerball_One] " +
                    "(One) VALUES ({0})", a);
                QueryExecution.ToDB(cmdText, con);
                ForEachB(con, a);
            }
        }

        private static void ForEachB(SqlConnection con, int a)
        {
            for (int b = 1; b <= lottoRange; b++)
            {
                drawing.Add(b);
                if (!(a == b) && Unique.Check(drawing, Two, con))
                {
                    cmdText =
                    String.Format("INSERT INTO [dbo].[Powerball_Two] " +
                    "(One, Two, SetOfNumbersOfNumbers) VALUES ({0},{1}, '({0},{1})')", a, b);
                    QueryExecution.ToDB(cmdText, con);
                    ForEachC(con, a, b);
                }
            }
        }

        private static void ForEachC(SqlConnection con, int a, int b)
        {
            for (int c = 1; c <= lottoRange - 2; c++)
            {
                drawing.Add(c);
                if (!(c == a || c == b) && Unique.Check(drawing, Three, con))
                {
                    cmdText =
                    String.Format("INSERT INTO [dbo].[Powerball_Three] " +
                    "(One, Two, Three, SetOfNumbersOfNumbers) VALUES ({0},{1},{2},'({0},{1},{2})')", a, b, c);
                    QueryExecution.ToDB(cmdText, con);
                    ForEachD(con, a, b, c);

                }
            }
        }

        private static void ForEachD(SqlConnection con, int a, int b, int c)
        {
            for (int d = 1; d <= lottoRange - 3; d++)
            {
                drawing.Add(d);
                if (!(d == a || d == b || d == c) && Unique.Check(drawing, Four, con))
                {
                    cmdText =
                    String.Format("INSERT INTO [dbo].[Powerball_Four] " +
                    "(One, Two, Three, Four, SetOfNumbersOfNumbers) VALUES ({0},{1},{2},{3}, ({0},{1},{2},{3}))", a, b, c, d);
                    QueryExecution.ToDB(cmdText, con);
                    ForEachE(con, a, b, c, d);
                }
            }
        }

        private static void ForEachE(SqlConnection con, int a, int b, int c, int d)
        {
            for (int e = 1; e <= lottoRange - 4; e++)
            {
                drawing.Add(e);
                if (!(e == d || e == c || e == b || e == a) && Unique.Check(drawing, Five, con))
                {
                    cmdText =
                    String.Format("INSERT INTO [dbo].[Powerball_Five] " +
                    "(One, Two, Three, Four, Five, SetOfNumbersOfNumbers) VALUES ({0},{1},{2}, {3}, {4}, ({0},{1},{2}, {3}, {4}))", a, b, c, d);
                    QueryExecution.ToDB(cmdText, con);
                    ForEachPowerball(con, redPowerball, a, b, c, d, e);
                }
            }
        }

        private static void ForEachPowerball(SqlConnection con, int redPowerball, int a, int b, int c, int d, int e)
        { 
            for (int p = 1; p <= redPowerball; p++)
            {
                drawing.Add(p);
                if (Unique.Check(drawing, All, con))
                {
                    cmdText =
                    String.Format("INSERT INTO [dbo].[Powerball_All] " +
                    "(One, Two, Three, Four, Five, SetOfNumbersOfNumbers) VALUES ({0},{1},{2}, {3}, {4}, {5}, ({0},{1},{2}, {3}, {4}, {5}))", a, b, c, d, p);
                    QueryExecution.ToDB(cmdText, con);
                }
            }
                
        }
    }
    
}
