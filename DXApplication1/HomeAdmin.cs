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
        public HomeAdmin()
        {
            InitializeComponent();
            this.MaximizeBox = false; //Tắt chức năng phóng to form
            
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


        int clickCount = 0;
        private void labAdmin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clickCount++;
            if (clickCount == 7)
            {
                System.Windows.Forms.DialogResult result = MessageBox.Show("Bạn có phải là chủ phòng không - Hãy cẩn thận công việc của bạn", "Xác nhận", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    accordionControlElement9.Visible = true;
                    accordionControlElement10.Visible = true;
                    accordionControlElement7.Visible = true;
                }               
            }
        }
    }
}
