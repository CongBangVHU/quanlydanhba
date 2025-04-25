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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace _010662_010190_Bang_Dat.ucControl
{
    public partial class ucThemDanhBa : UserControl
    {
        SqlConnection conn;
        DataSet ds_LIENHE = new DataSet();
        public ucThemDanhBa()
        {
            InitializeComponent();
            conn = SqlConnectionManager.GetConnection();

        }
        void LoadDuLieu()
        {
            ds_LIENHE.Tables.Clear();

            string strsel = "select MaLienHe, HoTen, SDT, Email, DiaChi, GhiChu, NgaySinh from LIENHE";
            SqlDataAdapter da = new SqlDataAdapter(strsel, conn);
            da.Fill(ds_LIENHE, "LIENHE");

            ds_LIENHE.Tables["LIENHE"].Columns["MaLienHe"].AutoIncrement = true;
            ds_LIENHE.Tables["LIENHE"].Columns["MaLienHe"].AutoIncrementSeed = -1;
            ds_LIENHE.Tables["LIENHE"].Columns["MaLienHe"].AutoIncrementStep = -1;

            DataColumn[] key = new DataColumn[1];
            key[0] = ds_LIENHE.Tables["LIENHE"].Columns["MaLienHe"];
            ds_LIENHE.Tables["LIENHE"].PrimaryKey = key;

            dgvThemDanhBa.DataSource = ds_LIENHE.Tables["LIENHE"];
        }
        private void ucThemDanhBa_Load(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                LoadDuLieu();
                txtHoTen.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void ucThemDanhBa_Leave(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        /*private void btnThem_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string SDT = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            string ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");

            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(SDT))
            {
                MessageBox.Show("Vui lòng nhập Họ tên và Số điện thoại.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Thay đổi cách thêm dữ liệu bằng cách sử dụng câu lệnh INSERT trực tiếp
                string insertQuery = @"INSERT INTO LIENHE (HoTen, SDT, Email, DiaChi, GhiChu, NgaySinh) 
                             VALUES (@HoTen, @SDT, @Email, @DiaChi, @GhiChu, @NgaySinh)";

                using (SqlConnection conn = SqlConnectionManager.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@SDT", SDT);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : (object)email);
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(diaChi) ? DBNull.Value : (object)diaChi);
                        cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(ghiChu) ? DBNull.Value : (object)ghiChu);
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thêm thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa nội dung các TextBox
                txtHoTen.Clear();
                txtSDT.Clear();
                txtEmail.Clear();
                txtDiaChi.Clear();
                txtGhiChu.Clear();

                // Tải lại dữ liệu
                LoadDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/


        private void btnThem_Click(object sender, EventArgs e)
        {

            string hoTen = txtHoTen.Text.Trim();
            string SDT = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            string ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");

            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(SDT))
            {
                MessageBox.Show("Vui lòng nhập Họ tên và Số điện thoại.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTenFilter = hoTen.Replace("'", "''");
            DataRow[] existingRows = ds_LIENHE.Tables["LIENHE"].Select($"hoTen = '{hoTenFilter}'");

            if (existingRows.Length > 0)
            {
                MessageBox.Show("Dữ liệu đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataRow newRow = ds_LIENHE.Tables["LIENHE"].NewRow();
                newRow["hoTen"] = hoTen;
                newRow["SDT"] = SDT;
                newRow["email"] = email;
                newRow["diaChi"] = diaChi;
                newRow["ghiChu"] = ghiChu;
                newRow["ngaySinh"] = ngaySinh;

                ds_LIENHE.Tables["LIENHE"].Rows.Add(newRow);

                using (SqlConnection conn = SqlConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT MaLienHe, HoTen, SDT, Email, DiaChi, GhiChu, NgaySinh FROM LIENHE", conn);
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                    da.Update(ds_LIENHE, "LIENHE");
                }

                MessageBox.Show("Thêm thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Clear();
                txtSDT.Clear();
                txtEmail.Clear();
                txtDiaChi.Clear();
                txtGhiChu.Clear();

                LoadDuLieu();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            List<int> danhSachXoa = new List<int>();

            foreach (DataGridViewRow row in dgvThemDanhBa.Rows)
            {
                if (row.Cells["colChon"].Value != null && Convert.ToBoolean(row.Cells["colChon"].Value) == true)
                {
                    int maLienHe = Convert.ToInt32(row.Cells["MaLienHe"].Value);
                    danhSachXoa.Add(maLienHe);
                }
            }

            if (danhSachXoa.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một dòng để xóa.");
                return;
            }

                foreach (int maLienHe in danhSachXoa)
                {
                    string query = "DELETE FROM LIENHE WHERE MaLienHe = @MaLienHe";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLienHe", maLienHe);
                        cmd.ExecuteNonQuery();
                    }
                }
            MessageBox.Show("Đã xóa " + danhSachXoa.Count + " dòng.");
            LoadDuLieu();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadDuLieu();
        }

       
    }
}
