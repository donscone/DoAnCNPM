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
            MemUpdate.Show();
        }
    }
}
