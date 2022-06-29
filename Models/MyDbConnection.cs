using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.Models
{
    public static class MyConnection
    {
        public static string DefaultConnection { get; set; }
    }
    public class ORMConnection
    {
        private static SqlConnection con;
        public static SqlConnection GetConnection()
        {
            //MyConnection m = new MyConnection(settings);
            //string c = settings.Value.DefaultConnection.ToString();
            //string _DefaultConnection = Configuration.GetSection("ConnectionString:DefaultConnection").ToString();
            ///con = new SqlConnection("Data Source=148.72.232.168;Initial Catalog=NidhiDB; user id=dbnidhiadmin; password=!Ki3b3h4;");
            //con = new SqlConnection("Data Source=SQL5050.site4now.net;Initial Catalog=DB_A548E9_NidhiDB;User Id=DB_A548E9_NidhiDB_admin;Password=Nidhi@2020#Rishi;");
            con = new SqlConnection(MyConnection.DefaultConnection);
            return con;
        }
    }
}
