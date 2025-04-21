using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _010662_010190_Bang_Dat.ConnectSql.ConnectSql;

namespace _010662_010190_Bang_Dat.ucControl
{
    public partial class ucNhomDanhBa : UserControl
    {
        public ucNhomDanhBa()
        {
            InitializeComponent();
            using (SqlConnection conn = SqlConnectionManager.GetConnection())
            {
                conn.Open();
            }
        }
    }
}
