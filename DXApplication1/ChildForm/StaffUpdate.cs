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
    public partial class StaffUpdate : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DEEPMOON-DESKTO\DUYCONGLAP;Initial Catalog=GymTitanDb;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public StaffUpdate()
        {
            InitializeComponent();
            this.MaximizeBox = false; //Tắt chức năng phóng to form
        }

        void DataLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = " Select * from NHANVIEN";
            adapter.SelectCommand = command;
            dt.Clear();
            adapter.Fill(dt);
            dgvUpdate.DataSource = dt;
        }

        private void StaffUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void StaffUpdate_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            DataLoad();
        }

        private void dgvUpdate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNhanVien.ReadOnly = true;

            txtMaNhanVien.Text = dgvUpdate.SelectedRows[0].Cells[0].Value.ToString();
            txtTenNhanVien.Text = dgvUpdate.SelectedRows[0].Cells[1].Value.ToString();
            txtSDTNhanVien.Text = dgvUpdate.SelectedRows[0].Cells[2].Value.ToString();
            cmbChucVu.Text = dgvUpdate.SelectedRows[0].Cells[3].Value.ToString();
            dateThamGia.Text = dgvUpdate.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Lưu Thông Tin ? ", "Xác nhận lưu", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Insert into NhanVien values(N'" + txtTenNhanVien.Text + "' , '" + txtSDTNhanVien.Text + "' , N'" + cmbChucVu.Text + "' , '" + dateThamGia.Text + "')";
                command.ExecuteNonQuery();
                DataLoad();
                System.Windows.Forms.MessageBox.Show("Thêm Nhân Viên Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Xoá Thông Tin Nhân Viên ? ", "Xác nhận xoá", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Delete from NhanVien where MaNhanVien ='" + txtMaNhanVien.Text + "'";
                command.ExecuteNonQuery();
                DataLoad();
                System.Windows.Forms.MessageBox.Show("Đã Xoá Thông Tin Nhân Viên!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Sửa Thông Tin Nhân Viên ? ", "Xác nhận sửa", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Update NhanVien set TenNhanVien =N'" + txtTenNhanVien.Text + "' , SoDienThoai ='" + txtSDTNhanVien.Text + "' , ChucVu =N'" + cmbChucVu.Text + "' ,  NgayThamGia ='" + dateThamGia.Text + "'  where MaNhanVien ='" + txtMaNhanVien.Text + "' ";
                command.ExecuteNonQuery();
                DataLoad();
                System.Windows.Forms.MessageBox.Show("Đã Sửa Thông Tin Nhân Viên Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtSDTNhanVien.Text = "";
            cmbChucVu.Text = "";
            dateThamGia.Text = "";
        }
    }
}