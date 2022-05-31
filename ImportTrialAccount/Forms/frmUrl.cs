using ImportTrialAccount.Services;
using System;
using System.Windows.Forms;

namespace ImportTrialAccount.Forms
{
    public partial class frmUrl : Form
    {
        public frmUrl()
        {
            InitializeComponent();
        }

        private void frmUrl_Load(object sender, EventArgs e)
        {
            txtURL.Text = TempData.URL;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TempData.URL = txtURL.Text;
            MessageBox.Show("Saved!");
            try
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra.");
            }
        }
    }
}
