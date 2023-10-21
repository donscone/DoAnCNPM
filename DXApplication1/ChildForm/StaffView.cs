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
    public partial class StaffView : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DEEPMOON-DESKTO\DUYCONGLAP;Initial Catalog=GymTitanDb;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public StaffView()
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

        private void StaffView_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void StaffView_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            DataLoad();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchValue))
            {
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM NhanVien WHERE SoDienThoai LIKE @SearchValue";
                command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");
                adapter.SelectCommand = command;
                dt.Clear();
                adapter.Fill(dt);
                dgvUpdate.DataSource = dt;
            }
            else
            {
                DataLoad(); // Nếu không có giá trị tìm kiếm, hiển thị tất cả dữ liệu
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}