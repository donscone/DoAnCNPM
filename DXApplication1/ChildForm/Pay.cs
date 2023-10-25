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
    public partial class Pay : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DEEPMOON-DESKTO\DUYCONGLAP;Initial Catalog=GymTitanDb;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dtPay = new DataTable();
        public Pay()
        {
            InitializeComponent();
            this.MaximizeBox = false; //Tắt chức năng phóng to form
        }

        void DataLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = " Select MaThanhVien, TenThanhVien, GoiTap from THANHVIEN";
            adapter.SelectCommand = command;
            dt.Clear();
            adapter.Fill(dt);
            dgvUpdate.DataSource = dt;
        }

        void DataLoad2()
        {
            command = connection.CreateCommand();
            command.CommandText = " Select * from THANHTOAN";
            adapter.SelectCommand = command;
            dtPay.Clear();
            adapter.Fill(dtPay);
            dgvPay.DataSource = dtPay;
        }


        private void Pay_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            DataLoad();
            DataLoad2();
        }

        private void Pay_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void dgvUpdate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbTinhTrang.Text = "";
            dateBatDau.Text = "";
            dateHetHan.Text = "";
           
            txtMaThanhVien.Text = dgvUpdate.SelectedRows[0].Cells[0].Value.ToString();
            txtTenThanhVien.Text = dgvUpdate.SelectedRows[0].Cells[1].Value.ToString();
            cmbGoiTap.Text = dgvUpdate.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void dgvPay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaThanhVien.Text = dgvPay.SelectedRows[0].Cells[0].Value.ToString();
            txtTenThanhVien.Text = dgvPay.SelectedRows[0].Cells[1].Value.ToString();
            cmbGoiTap.Text = dgvPay.SelectedRows[0].Cells[2].Value.ToString();
            cmbTinhTrang.Text = dgvPay.SelectedRows[0].Cells[3].Value.ToString();
            dateBatDau.Text = dgvPay.SelectedRows[0].Cells[4].Value.ToString();
            dateHetHan.Text = dgvPay.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaThanhVien.Text = "";
            txtTenThanhVien.Text = "";
            cmbGoiTap.Text = "";
            cmbTinhTrang.Text = "";
            dateBatDau.Text = "";
            dateHetHan.Text = "";
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Thanh Toán ? ", "Xác nhận thanh toán", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Insert into ThanhToan values(N'" + txtMaThanhVien.Text + "' , N'" + txtTenThanhVien.Text + "' , N'" + cmbGoiTap.Text + "' , N'" + cmbTinhTrang.Text + "' , '" + dateBatDau.Text + "' , '" + dateHetHan.Text + "')";
                command.ExecuteNonQuery();
                DataLoad2();
                System.Windows.Forms.MessageBox.Show("Thanh Toán Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Xoá Thông Tin Thanh Toán ? ", "Xác nhận xoá", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Delete from ThanhToan where MaThanhVien ='" + txtMaThanhVien.Text + "'";
                command.ExecuteNonQuery();
                DataLoad2();
                System.Windows.Forms.MessageBox.Show("Đã Xoá Thông Tin Thanh Toán!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
             System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Sửa Thông Tin Thanh Toán ? ", "Xác nhận sửa", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = "  Update ThanhToan set TinhTrang = N'" + cmbTinhTrang.Text + "' , NgayBatDauGoi = '" + dateBatDau.Text + "' , NgayHetHan = '" + dateHetHan.Text + "' where MaThanhVien ='" + txtMaThanhVien.Text + "'";
                command.ExecuteNonQuery();
                DataLoad2();
                System.Windows.Forms.MessageBox.Show("Đã Sửa Thông Tin Thanh Toán Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }
    }
}