using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.XtraBars;
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
            this.MaximizeBox = false;
        }

        private void HomeAdmin_FormClosing(object sender, FormClosingEventArgs e) //Luôn hiển thị hộp thoại xác nhận khi đóng form
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void accordionControlElement3_Click(object sender, EventArgs e) //Mở form "MemUpdate" khi click 
        {
            MemUpdate MemUpdate = new MemUpdate();
            MemUpdate.ShowDialog();
            
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            MemPay MemPay = new MemPay();
            MemPay.ShowDialog();
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            MemView MemView = new MemView();
            MemView.ShowDialog();
        }

        private void HomeAdmin_Load(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            // Gán thông tin thời gian hiện tại vào TextBox
            txtCurrentTime.Text = currentTime.ToString("dd/MM/yyyy");
        }
    }
}
