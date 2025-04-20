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

namespace _010662_010190_Bang_Dat
{
    public partial class formMain : Form
    {
        List<Guna.UI2.WinForms.Guna2Button> menuButtons;
        public formMain()
        {
            InitializeComponent();
            ucThemDanhBa ucThem = new ucThemDanhBa();
            addUserControlThaoTac(ucThem);

            menuButtons = new List<Guna2Button>
            {
                btnThem,
                btnXoa,
                btnSua,
                btnTimKiem,
                btnNhom,
                btnSapXep
            };


        }

        private void formMain_Load(object sender, EventArgs e)
        {

        }
        private void HighlightMenu(Guna2Button selectedBtn)
        {
            foreach (var btn in menuButtons)
                if (btn == selectedBtn)
                {
                    // Nút đang được chọn
                    btn.FillColor = Color.DodgerBlue;         // Màu nổi bật
                    btn.ForeColor = Color.White;
                }
                else
                {
                    // Các nút khác
                    btn.FillColor = Color.FromArgb(90, 95, 106); // Màu nền gốc của sidebar
                    btn.ForeColor = Color.White;
                }
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
        private void btnXoa_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnXoa);

            ucXoaDanhBa ucXoa = new ucXoaDanhBa();
            addUserControlThaoTac(ucXoa);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnSua);

            ucSuaDanhBa ucSua = new ucSuaDanhBa();
            addUserControlThaoTac(ucSua);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnTimKiem);

            ucTimKiemDanhBa ucTimKiem = new ucTimKiemDanhBa();
            addUserControlThaoTac(ucTimKiem);
        }

        private void btnNhom_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnNhom);

            ucNhomDanhBa ucNhom = new ucNhomDanhBa();
            addUserControlThaoTac(ucNhom);
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            HighlightMenu(btnSapXep);

            ucSapXepDanhBa ucSapXep = new ucSapXepDanhBa();
            addUserControlThaoTac(ucSapXep);
        }
    }
}
