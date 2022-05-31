using ImportTrialAccount.Services;
using System.Windows.Forms;

namespace ImportTrialAccount.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private async void btnLogin_Click(object sender, System.EventArgs e)
        {
            string tenDN = txtTenDangNhap.Text.Trim();
            string mk = txtMatKhau.Text.Trim();

            var loginResponse = await HTSService.LoginAsync(tenDN, mk);

            if (loginResponse.status == "success" && loginResponse.errors == null)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("login Failed!");
            }
        }
    }
}
