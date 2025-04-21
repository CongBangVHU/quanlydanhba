using _010662_010190_Bang_Dat.ucControl;
using Guna.UI2.WinForms;
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


namespace _010662_010190_Bang_Dat
{
    public partial class formMain : Form
    {
        private SqlConnection conn;
        List<Guna.UI2.WinForms.Guna2Button> menuButtons;

        public formMain()
        {
            InitializeComponent();

            ucThemDanhBa ucThem = new ucThemDanhBa();
            addUserControlThaoTac(ucThem);

            menuButtons = new List<Guna2Button>
            {
                btnThem,
                btnNhom,
                btnXem,
                btnLichSu,
                btnSaoLuu,
                btnQuanLy
            };
            using (SqlConnection conn = SqlConnectionManager.GetConnection())
            {
                conn.Open();
            }
        }
             
        private void HighlightMenu(Guna2Button selectedBtn)
        {
            foreach (var btn in menuButtons)
                if (btn == selectedBtn)
                {
                    btn.FillColor = Color.DodgerBlue;
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.FillColor = Color.FromArgb(90, 95, 106);
                    btn.ForeColor = Color.White;
                }
        }
        private void formMain_Load(object sender, EventArgs e)
        {
            HighlightMenu(btnThem);
        }
        private void addUserControlThaoTac(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            PanelThaoTac.Controls.Clear();
            PanelThaoTac.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnThem);

            ucThemDanhBa ucThem = new ucThemDanhBa();
            addUserControlThaoTac(ucThem);
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnLichSu);

            ucLichSuNguoiDung ucTimKiem = new ucLichSuNguoiDung();
            addUserControlThaoTac(ucTimKiem);
        }

        private void btnNhom_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnNhom);

            ucNhomDanhBa ucNhom = new ucNhomDanhBa();
            addUserControlThaoTac(ucNhom);
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnXem);

            ucXemDanhBa ucSapXep = new ucXemDanhBa();
            addUserControlThaoTac(ucSapXep);
        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnQuanLy);

            ucQuanLyNguoiDung ucQuanLy = new ucQuanLyNguoiDung();
            addUserControlThaoTac(ucQuanLy);
        }

        private void btnSaoLuu_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnSaoLuu);

            ucSaoLuuDanhBa ucSaoLuu = new ucSaoLuuDanhBa();
            addUserControlThaoTac(ucSaoLuu);
        }
    }
}
