using System;
using Xunit;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace PowerBallDatabasexUnitTest
{
    public class UnitTest1
    {
        private static string myConnection =
        "Server = DESKTOP-PJ6QB08; Database=Lottery;Integrated Security = True;";
        SqlConnection con = new SqlConnection(myConnection);
        List<int> drawingTrue = new List<int>();
        List<int> drawingFalse = new List<int>();
        SqlCommand cmd = new SqlCommand();
        private static string One = "[dbo].[Powerball_One]";
        private static string Two = "[dbo].[Powerball_Two]";
        private static string Three = "[dbo].[Powerball_Three]";
        private static string Four = "[dbo].[Powerball_Four]";
        private static string Five = "[dbo].[Powerball_Five]";
        private static string All = "[dbo].[Powerball_All]";

        [Fact]

        public void TestUnique()
        {
            drawingTrue.Add(1);
            Assert.True(PowerBallDatabase.Unique.Check(drawingTrue, One, con));
            drawingTrue.Add(2);
            Assert.True(PowerBallDatabase.Unique.Check(drawingTrue, Two, con));
            drawingTrue.Add(3);
            Assert.True(PowerBallDatabase.Unique.Check(drawingTrue, Three, con));
            drawingTrue.Add(4);
            Assert.True(PowerBallDatabase.Unique.Check(drawingTrue, Four, con));
            drawingTrue.Add(5);
            Assert.True(PowerBallDatabase.Unique.Check(drawingTrue, Five, con));
            drawingTrue.Add(1);
            Assert.True(PowerBallDatabase.Unique.Check(drawingTrue, All, con));
        }
    }
}
