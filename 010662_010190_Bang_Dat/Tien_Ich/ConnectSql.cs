using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _010662_010190_Bang_Dat.ConnectSql
{
    internal class ConnectSql
    {
        public static class SqlConnectionManager
        {
            private static readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyDanhBa;Integrated Security=True";

            public static SqlConnection GetConnection()
            {
                return new SqlConnection(connectionString);
            }
        }
    }
}
