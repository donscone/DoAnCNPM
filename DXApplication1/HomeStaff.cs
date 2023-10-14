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
    public partial class HomeStaff : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public HomeStaff()
        {
            InitializeComponent();
        }

        private void HomeStaff_FormClosing(object sender, FormClosingEventArgs e) //Luôn hiển thị hộp thoại xác nhận khi đóng form
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Hủy việc đóng Form
                e.Cancel = true;
            }
        }
    }
}
