using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.XtraBars;
using DevExpress.XtraToolbox;
using DXApplication1.ChildForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class HomeAdmin : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        string correctPass = "admin";
        int clickCount = 0;
        public HomeAdmin()
        {
            InitializeComponent();
            this.MaximizeBox = false; //Tắt chức năng phóng to form
            //Khoá chức năng quản lý nhân viên
            accordionControlElement9.Visible = false;
            accordionControlElement10.Visible = false;
            accordionControlElement7.Visible = false;
        }



        private void HomeAdmin_FormClosing(object sender, FormClosingEventArgs e) //Luôn hiển thị hộp thoại xác nhận khi đóng form
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void accordionControlElement3_Click(object sender, EventArgs e) //Mở form "MemUpdate" 
        {
            MemUpdate MemUpdate = new MemUpdate();
            MemUpdate.ShowDialog();        
        }

        private void accordionControlElement2_Click(object sender, EventArgs e) //Mở form "MemView"
        {
            MemView MemView = new MemView();
            MemView.ShowDialog();
        }

        private void HomeAdmin_Load(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now; //Lấy thông tin thời gian hiện tại
            txtCurrentTime.Text = currentTime.ToString("dd/MM/yyyy"); //Gán thông tin thời gian hiện tại vào TextBox
        }

        private void accordionControlElement4_Click(object sender, EventArgs e) //Mở form "ToolUpdate"
        {
            ToolUpdate ToolUpdate = new ToolUpdate();
            ToolUpdate.ShowDialog();
        }

        private void accordionControlElement6_Click(object sender, EventArgs e) //Mở form "Pay"
        {
            Pay pay = new Pay();
            pay.ShowDialog();
        }

        private void accordionControlElement8_Click(object sender, EventArgs e) //Mở form "ToolView"
        {
            ToolView ToolView = new ToolView();
            ToolView.ShowDialog();
        }

        private void accordionControlElement9_Click(object sender, EventArgs e) //Mở form "StaffUpdate"
        {
            StaffUpdate StaffUpdate = new StaffUpdate();
            StaffUpdate.ShowDialog();
        }

        private void accordionControlElement10_Click(object sender, EventArgs e) //Mở form "StafView"
        {
            StaffView StaffView = new StaffView();
            StaffView.ShowDialog();        
        }

        private void labAdmin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clickCount++;
            if (clickCount == 1)
            {
                System.Windows.Forms.DialogResult result = MessageBox.Show("Chức năng này chỉ dành cho chủ phòng hoặc nhà phát triển !", "Xác nhận", MessageBoxButtons.YesNo);
            }
            else if (clickCount == 3)
            {
                string passInput = InputBox("Nhập mật khẩu");
                if (passInput == correctPass)
                {
                    OpenStaffMan();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng!");
                }
            }
        }
        public static string InputBox(string text)
        {
            string password;
            password = Microsoft.VisualBasic.Interaction.InputBox(text, "Nhập Mật Khẩu", "");
            return password;
        }

        private void OpenStaffMan()
        {
            accordionControlElement9.Visible = true;
            accordionControlElement10.Visible = true;
            accordionControlElement7.Visible = true;
            MessageBox.Show("Đã mở khoá quản lý Nhân viên !");
        }

    }
}
