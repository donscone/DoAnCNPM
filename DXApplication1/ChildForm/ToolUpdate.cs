using DevExpress.XtraEditors;
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

namespace DXApplication1.ChildForm
{
    public partial class ToolUpdate : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DEEPMOON-DESKTO\DUYCONGLAP;Initial Catalog=GymTitanDb;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public ToolUpdate()
        {
            InitializeComponent();
            this.MaximizeBox = false; //Tắt chức năng phóng to form
        }

        void DataLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = " Select * from THIETBI";
            adapter.SelectCommand = command;
            dt.Clear();
            adapter.Fill(dt);
            dgvUpdate.DataSource = dt;
        }

        private void ToolUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ToolUpdate_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            DataLoad();
        }

        private void dgvUpdate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaThietBi.ReadOnly = true;

            txtMaThietBi.Text = dgvUpdate.SelectedRows[0].Cells[0].Value.ToString();
            txtTenThietBi.Text = dgvUpdate.SelectedRows[0].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvUpdate.SelectedRows[0].Cells[2].Value.ToString();
            cmbTinhTrang.Text = dgvUpdate.SelectedRows[0].Cells[3].Value.ToString();
            dateBaoTri.Text = dgvUpdate.SelectedRows[0].Cells[4].Value.ToString();
        }

        private bool CheckMemberExists(string maThanhVien)
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM ThietBi WHERE MaThietBi = '" + txtMaThietBi.Text + "'";
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string soLuong = txtSoLuong.Text;
            string tenThietBi = txtTenThietBi.Text;
            string ngayBaoTri = dateBaoTri.Text;
            string tinhTrang = cmbTinhTrang.Text;
            bool memberExists = CheckMemberExists(txtMaThietBi.Text);
            if (string.IsNullOrEmpty(soLuong) || string.IsNullOrEmpty(tenThietBi) || string.IsNullOrEmpty(ngayBaoTri) || string.IsNullOrEmpty(tinhTrang))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (memberExists)
            {
                MessageBox.Show("Dụng cụ đã tồn tại!");
                return;
            }
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Lưu Thông Tin Thiết Bị ? ", "Xác nhận lưu", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Insert into ThietBi values(N'" + txtTenThietBi.Text + "' , '" + txtSoLuong.Text + "' , N'" + cmbTinhTrang.Text + "' , '" + dateBaoTri.Text + "')";
                command.ExecuteNonQuery();
                DataLoad();
                System.Windows.Forms.MessageBox.Show("Thêm Thiết Bị Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maThietBi = txtMaThietBi.Text;
            if (string.IsNullOrEmpty(maThietBi))
            {
                MessageBox.Show("Vui lòng chọn thiết bị để xoá!");
                return;
            }
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Xoá Thông Tin Thiết Bị ? ", "Xác nhận xoá", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Delete from ThietBi where MaThietBi ='" + txtMaThietBi.Text + "'";
                command.ExecuteNonQuery();
                DataLoad();
                System.Windows.Forms.MessageBox.Show("Đã Xoá Thông Tin Thiết Bị!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Sửa Thông Tin Thiết Bị ? ", "Xác nhận sửa", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Update ThietBi set TenThietBi =N'" + txtTenThietBi.Text + "' , SoLuong ='" + txtSoLuong.Text + "' , TinhTrang =N'" + cmbTinhTrang.Text + "' ,  NgayBaoTri ='" + dateBaoTri.Text + "'  where MaThietBi ='" + txtMaThietBi.Text + "' ";
                command.ExecuteNonQuery();
                DataLoad();
                System.Windows.Forms.MessageBox.Show("Đã Sửa Thông Tin Thiết Bị Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaThietBi.Text = "";
            txtTenThietBi.Text = "";
            txtSoLuong.Text = "";
            cmbTinhTrang.Text = "";
            dateBaoTri.Text = "";
        }
    }
}