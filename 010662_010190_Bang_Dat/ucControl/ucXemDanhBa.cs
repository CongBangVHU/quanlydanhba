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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _010662_010190_Bang_Dat.ucControl
{
    public partial class ucXemDanhBa : UserControl
    {
        SqlConnection conn;
        DataSet ds_LIENHE = new DataSet();

        public ucXemDanhBa()
        {
            InitializeComponent();
            conn = SqlConnectionManager.GetConnection();

            this.Load += new System.EventHandler(this.ucXemDanhBa_Load);
            this.Leave += new System.EventHandler(this.ucXemDanhBa_Leave);

            txtTimKiem.KeyDown += txtTimKiem_KeyDown;

            cbSapXep.Items.Add("Mã liên hệ");
            cbSapXep.Items.Add("A - Z");
            cbSapXep.Items.Add("Họ tên");
            cbSapXep.Items.Add("Địa chỉ");
            cbSapXep.Items.Add("Ngày sinh");
            cbThuTu.Items.Add("Tăng dần");
            cbThuTu.Items.Add("Giảm dần");
            cbSapXep.Text = "Mã liên hệ";
            cbThuTu.Text = "Tăng dần";
        }
        void LoadDuLieu()
        {
            string strsel = "select MaLienHe, HoTen, SDT, Email, DiaChi, GhiChu, NgaySinh from LIENHE";
            SqlDataAdapter da = new SqlDataAdapter(strsel, conn);
            da.Fill(ds_LIENHE, "LIENHE");
            dgvXemDanhBa.DataSource = ds_LIENHE.Tables["LIENHE"];
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_LIENHE.Tables["LIENHE"].Columns["MaLienHe"];
            ds_LIENHE.Tables["LIENHE"].PrimaryKey = key;
        }
        private void ucXemDanhBa_Load(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                    LoadDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }
        private void ucXemDanhBa_Leave(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        private void cbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void cbThuTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string cotSapXep = "";
            string thuTu = cbThuTu.Text == "Tăng dần" ? "ASC" : "DESC";

            switch (cbSapXep.Text)
            {
                case "Mã liên hệ":
                    cotSapXep = "MaLienHe";
                    break;
                case "A - Z":
                case "Họ tên":
                    cotSapXep = "HoTen";
                    break;
                case "Địa chỉ":
                    cotSapXep = "DiaChi";
                    break;
                case "Ngày sinh":
                    cotSapXep = "NgaySinh";
                    break;
            }

            DataTable dt = ds_LIENHE.Tables["LIENHE"];
            if (dt == null) return;

            DataView dv = new DataView(dt);

            string tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                dv.RowFilter = $"HoTen LIKE '%{tuKhoa}%' OR DiaChi LIKE '%{tuKhoa}%'";
            }

            dv.Sort = $"{cotSapXep} {thuTu}";

            dgvXemDanhBa.DataSource = dv;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            DataTable dt = ds_LIENHE.Tables["LIENHE"];

            if (string.IsNullOrEmpty(tuKhoa))
            {
                dgvXemDanhBa.DataSource = dt;
            }
            else
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = $"HoTen LIKE '%{tuKhoa}%' OR DiaChi LIKE '%{tuKhoa}%'";
                dgvXemDanhBa.DataSource = dv;
            }
            txtTimKiem.Focus();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
                txtTimKiem.Focus();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                dgvXemDanhBa.DataSource = ds_LIENHE.Tables["LIENHE"];
            }
        }
    }
}
