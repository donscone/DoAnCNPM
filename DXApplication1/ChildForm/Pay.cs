using DevExpress.Data.Svg;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DXApplication1.ChildForm
{
    public partial class Pay : DevExpress.XtraEditors.XtraForm
    {
        bool isPrinted = false;
        PrintPreviewDialog printPreviewDialog;
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
            dateHetHan.ReadOnly = true;
            cmbGoiTap.Enabled = false;
            printPreviewDialog = new PrintPreviewDialog();
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
            txtGiaTien.Text = "";
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
            txtGiaTien.Text = dgvPay.SelectedRows[0].Cells[3].Value.ToString();
            dateBatDau.Text = dgvPay.SelectedRows[0].Cells[4].Value.ToString();
            dateHetHan.Text = dgvPay.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaThanhVien.Text = "";
            txtTenThanhVien.Text = "";
            cmbGoiTap.Text = "";
            txtGiaTien.Text = "";
            dateBatDau.Text = "";
            dateHetHan.Text = "";
        }

        private void btnPay_Click(object sender, EventArgs e) //Thanh toán 
        {
            string ngayBatDau = dateBatDau.Text;
            string maThanhVien = txtMaThanhVien.Text;
            if (string.IsNullOrEmpty(maThanhVien) || string.IsNullOrEmpty(ngayBatDau))
            {
                MessageBox.Show("Vui lòng chọn và nhập thông tin đầy đủ trước khi thanh toán!");
                return;
            }
            DateTime currentDate = DateTime.Now;
            DataRow[] paymentRows = dtPay.Select("MaThanhVien = '" + maThanhVien + "' AND NgayBatDau <= '" + currentDate + "' AND NgayHetHan >= '" + currentDate + "'");
            if (paymentRows.Length > 0)
            {
                MessageBox.Show("Thành viên đã thanh toán cho tháng này!");
                return;
            }
            else
            {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Thanh Toán ? ", "Xác nhận thanh toán", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Insert into ThanhToan values(N'" + txtMaThanhVien.Text + "' , N'" + txtTenThanhVien.Text + "' , N'" + cmbGoiTap.Text + "' , N'" + txtGiaTien.Text + "' , '" + dateBatDau.Text + "' , '" + dateHetHan.Text + "')";
                command.ExecuteNonQuery();
                DataLoad2();
                System.Windows.Forms.MessageBox.Show("Thanh Toán Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }
        }

        private void btnDelete_Click(object sender, EventArgs e) //Xoá thông tin thanh toán
        {
            string maThanhVien = txtMaThanhVien.Text;
            if (string.IsNullOrEmpty(maThanhVien))
            {
                MessageBox.Show("Vui lòng chọn thành viên để xoá!");
                return;
            }
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

        private void btnUpdate_Click(object sender, EventArgs e) //Sửa thông tin thanh toán
        {
            string maThanhVien = txtMaThanhVien.Text;
            if (string.IsNullOrEmpty(maThanhVien))
            {
                MessageBox.Show("Vui lòng chọn thành viên để sửa!");
                return;
            }
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Sửa Thông Tin Thanh Toán ? ", "Xác nhận sửa", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = " Update ThanhToan set NgayBatDau = '" + dateBatDau.DateTime + "' , NgayHetHan = '" + dateHetHan.DateTime + "' where MaThanhVien ='" + txtMaThanhVien.Text + "'";
                command.ExecuteNonQuery();
                DataLoad2();
                System.Windows.Forms.MessageBox.Show("Đã Sửa Thông Tin Thanh Toán Thành Công!", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void cmbGoiTap_SelectedIndexChanged(object sender, EventArgs e) //Gán giá tiền cho từng gói tập - Hiển thị giá trên hộp thông tin
        {
            string selectedValue = cmbGoiTap.SelectedItem.ToString().Trim(); 
            if (selectedValue == "1 Tháng")
            {
                txtGiaTien.Text = "200.000 đồng";        
            }
            else if (selectedValue == "3 Tháng")
            {
                txtGiaTien.Text = "570.000 đồng";
            }
            else if (selectedValue == "6 Tháng")
            {
                txtGiaTien.Text = "1.080.000 đồng";
            }
            else if (selectedValue == "1 Năm")
            {
                txtGiaTien.Text = "2.040.000 đồng";
            }
            else if (selectedValue == "3 Tháng Premium")
            {
                txtGiaTien.Text = "13.470.000 đồng";
            }
            else if (selectedValue == "6 Tháng Premium")
            {
                txtGiaTien.Text = "24.246.600 đồng";
            }
            else if (selectedValue == "1 Năm Premium")
            {
                txtGiaTien.Text = "45.798.000 đồng";
            }
        }

        private void dateBatDau_EditValueChanged(object sender, EventArgs e) //Tự động cộng thêm thời gian cho gói dựa theo ngày bắt đầu
        {
            if (cmbGoiTap.Text == "1 Tháng")
            {
                DateTime startDate = dateBatDau.DateTime;
                DateTime endDate = startDate.AddMonths(1);
                dateHetHan.DateTime = endDate;
            }
            else if (cmbGoiTap.Text == "3 Tháng")
            {
                DateTime startDate = dateBatDau.DateTime;
                DateTime endDate = startDate.AddMonths(3);
                dateHetHan.DateTime = endDate;
            }
            else if (cmbGoiTap.Text == "6 Tháng")
            {
                DateTime startDate = dateBatDau.DateTime;
                DateTime endDate = startDate.AddMonths(6);
                dateHetHan.DateTime = endDate;
            }
            else if (cmbGoiTap.Text == "1 Năm")
            {
                DateTime startDate = dateBatDau.DateTime;
                DateTime endDate = startDate.AddYears(1);
                dateHetHan.DateTime = endDate;
            }
            else if (cmbGoiTap.Text == "3 Tháng Premium")
            {
                DateTime startDate = dateBatDau.DateTime;
                DateTime endDate = startDate.AddMonths(3);
                dateHetHan.DateTime = endDate;
            }
            else if (cmbGoiTap.Text == "6 Tháng Premium")
            {
                DateTime startDate = dateBatDau.DateTime;
                DateTime endDate = startDate.AddMonths(6);
                dateHetHan.DateTime = endDate;
            }
            else if (cmbGoiTap.Text == "1 Năm Premium")
            {
                DateTime startDate = dateBatDau.DateTime;
                DateTime endDate = startDate.AddYears(1);
                dateHetHan.DateTime = endDate;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e) //In thẻ thành viên
        {
            if (string.IsNullOrEmpty(txtMaThanhVien.Text) || string.IsNullOrEmpty(dateBatDau.Text))
            {
                MessageBox.Show("Vui lòng chọn thông tin thành viên!");
                return;
            }
            if (isPrinted)
            {
                // Đã in trước đây, xác nhận in lại
                if (DialogResult.Yes != MessageBox.Show("Đã in trước đây, in lại ?", "Xác nhận", MessageBoxButtons.YesNo))
                    return;
            }
            printDocument1.Print();
            isPrinted = true;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) //Tạo thẻ thành viên
        {
            DataTable thanhToanDataTable = GetThanhToanData();
            int startX = 10;
            int startY = 10;
            int lineHeight = 20;
            e.Graphics.DrawString("\tTHẺ THÀNH VIÊN", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, startX, startY);
            e.Graphics.DrawString("\n--------------------------------------------", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, startX, startY);
            startY += lineHeight * 2;
            // Vẽ thông tin thành viên
            foreach (DataRow row in thanhToanDataTable.Rows)
            {
                if (row["MaThanhVien"].ToString() != txtMaThanhVien.Text)
                continue;
                //Gán thông tin của bảng ThanhToan từ Database
                string MaThanhVien = row["MaThanhVien"].ToString();
                string TenThanhVien = row["TenThanhVien"].ToString();
                string GoiTap = row["GoiTap"].ToString();
                string NgayBatDau = row["NgayBatDau"].ToString();
                string NgayHetHan = row["NgayHetHan"].ToString();
                e.Graphics.DrawString($"\n\tMã thành viên : {MaThanhVien}", new Font("Arial", 12), Brushes.Black, startX, startY);
                startY += lineHeight;
                e.Graphics.DrawString($"\n\tTên thành viên: {TenThanhVien}", new Font("Arial", 12), Brushes.Black, startX, startY);
                startY += lineHeight;
                e.Graphics.DrawString($"\n\tGói dịch vụ     : {GoiTap}", new Font("Arial", 12), Brushes.Black, startX, startY);
                startY += lineHeight;
                e.Graphics.DrawString($"\n\tNgày bắt đầu  : {NgayBatDau}", new Font("Arial", 12), Brushes.Black, startX, startY);
                startY += lineHeight;
                e.Graphics.DrawString($"\n\tNgày kết thúc : {NgayHetHan}", new Font("Arial", 12), Brushes.Black, startX, startY);
                startY += lineHeight;
                e.Graphics.DrawString("\n--------------------------------------------", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, startX, startY);
            }
        }

        private DataTable GetThanhToanData() //Lấy thông tin của bảng ThanhToan từ Database
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * From ThanhToan";
            adapter.SelectCommand = command;
            dtPay.Clear();
            adapter.Fill(dtPay);
            return dtPay;
        }

        private void btnPreview_Click(object sender, EventArgs e) //Xem trước thẻ thành viên
        {
            if (string.IsNullOrEmpty(txtMaThanhVien.Text) || string.IsNullOrEmpty(dateBatDau.Text))
            {
                MessageBox.Show("Vui lòng chọn thông tin thành viên!");
                return;
            }
            printPreviewDialog.Document = printDocument1;
            printPreviewDialog.ShowDialog();
        }

    
    }
}