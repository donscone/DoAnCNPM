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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = " Insert into ThietBi values(N'" + txtTenThietBi.Text + "' , '" + txtSoLuong.Text + "' , N'" + cmbTinhTrang.Text + "' , '" + dateBaoTri.Text + "')";
            command.ExecuteNonQuery();
            DataLoad();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = " Delete from ThietBi where MaThietBi ='" + txtMaThietBi.Text + "'";
            command.ExecuteNonQuery();
            DataLoad();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = " Update ThietBi set TenThietBi =N'" + txtTenThietBi.Text + "' , SoLuong ='" + txtSoLuong.Text + "' , TinhTrang =N'" + cmbTinhTrang.Text + "' ,  NgayBaoTri ='" + dateBaoTri.Text + "'  where MaThietBi ='" + txtMaThietBi.Text + "' ";
            command.ExecuteNonQuery();
            DataLoad();
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