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

namespace DXApplication1
{
    public partial class Unlock : DevExpress.XtraEditors.XtraForm
    {
        public Unlock()
        {
            InitializeComponent();
            this.MaximizeBox = false; //Tắt chức năng phóng to form
        }

        private void Unlock_Load(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now; //Lấy thông tin thời gian hiện tại
            txtCurrentTime.Text = currentTime.ToString("dd/MM/yyyy"); //Gán thông tin thời gian hiện tại vào TextBox
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtPass_OnValueChanged(object sender, EventArgs e)
        {
            txtPass.isPassword = true;         
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string MatKhau = txtPass.Text;
            if (txtPass.Text == "123456")
            {
                HomeAdmin homeAdmin = new HomeAdmin();
                homeAdmin.Show();
                this.Hide();
            }
            else if (string.IsNullOrEmpty(MatKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                return;
            }
            else
            {
                MessageBox.Show("Mật khẩu sai");
                return;
            }
        }
    }
}