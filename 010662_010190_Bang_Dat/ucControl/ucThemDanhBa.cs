using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static _010662_010190_Bang_Dat.ConnectSql.ConnectSql;


namespace _010662_010190_Bang_Dat.ucControl
{
    public partial class ucThemDanhBa : UserControl
    {
        private SqlConnection conn;

        public ucThemDanhBa()
        {
            InitializeComponent();
            using (SqlConnection conn = SqlConnectionManager.GetConnection())
            {
                conn.Open();
            }
        }

        
    }
}
