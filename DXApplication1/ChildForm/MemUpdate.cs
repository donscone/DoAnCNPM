using DevExpress.XtraEditors;
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
using Guna.UI.WinForms;
using System.Xml.Linq;

namespace DXApplication1
{
    public partial class MemUpdate : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DEEPMOON-DESKTO\DUYCONGLAP;Initial Catalog=GymTitanDb;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public MemUpdate()
        {
            InitializeComponent();
            this.MaximizeBox = false; //Tắt chức năng phóng to form
        }

        void DataLoad()
        {
            command = connection.CreateCommand();
            command.CommandText = " Select * from THANHVIEN";
            adapter.SelectCommand = command;
            dt.Clear();
            adapter.Fill(dt);
            dgvUpdate.DataSource = dt;
        }

        private void MemUpdate_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            DataLoad();
        }

        private void MemUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void dgvUpdate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {         
            txtMaThanhVien.ReadOnly = true;

            txtMaThanhVien.Text = dgvUpdate.SelectedRows[0].Cells[0].Value.ToString();
            txtTenThanhVien.Text = dgvUpdate.SelectedRows[0].Cells[1].Value.ToString();
            txtSDTThanhVien.Text = dgvUpdate.SelectedRows[0].Cells[2].Value.ToString();
            cmbGioiTinh.Text = dgvUpdate.SelectedRows[0].Cells[3].Value.ToString();
            dateSinh.Text = dgvUpdate.SelectedRows[0].Cells[4].Value.ToString();
            dateThamGia.Text = dgvUpdate.SelectedRows[0].Cells[5].Value.ToString();
            cmbGoiTap.Text = dgvUpdate.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = " Insert into ThanhVien values(N'"+txtTenThanhVien.Text+"' , '"+txtSDTThanhVien.Text+"' , N'"+cmbGioiTinh.Text+"' , '"+dateSinh.Text+"' , '"+dateThamGia.Text+"' , N'"+cmbGoiTap.Text+"')";
            command.ExecuteNonQuery();
            DataLoad();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = " Delete from ThanhVien where MaThanhVien ='" + txtMaThanhVien.Text + "'";
            command.ExecuteNonQuery();
            DataLoad();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = " Update ThanhVien set TenThanhVien =N'"+ txtTenThanhVien.Text +"' , SoDienThoai ='"+ txtSDTThanhVien.Text + "' , GioiTinh =N'"+ cmbGioiTinh.Text + "' , NgaySinh ='"+dateSinh.Text + "' , NgayThamGia ='"+dateThamGia.Text + "' , GoiTap =N'"+cmbGoiTap.Text +"' where MaThanhVien ='"+txtMaThanhVien.Text+"' ";
            command.ExecuteNonQuery();
            DataLoad();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaThanhVien.Text = "";
            txtTenThanhVien.Text = "";
            txtSDTThanhVien.Text = "";
            cmbGioiTinh. Text = "";
            dateSinh.Text = "";
            dateThamGia.Text = "";
            cmbGoiTap.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}