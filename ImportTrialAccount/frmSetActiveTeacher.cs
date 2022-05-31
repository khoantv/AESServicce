using ImportTrialAccount.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportTrialAccount
{
    public partial class frmSetActiveTeacher : Form
    {
        public frmSetActiveTeacher()
        {
            InitializeComponent();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            //backgroundWorker1.DoWork += backgroundWorker1_DoWorkAsync;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async Task backgroundWorker1_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            int pageIndex = int.Parse(txtPageIndex.Text.Trim());
            int pageSize = int.Parse(txtPageSize.Text.Trim());

            var lsTeachersResponse = await HTSService.GetListTeacherAll();

            if (lsTeachersResponse != null && lsTeachersResponse.status == "success" && lsTeachersResponse.errors == null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(lsTeachersResponse.data.list);
                dtTable = JsonConvert.DeserializeObject<DataTable>(json);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private BackgroundWorker backgroundWorker1;
        private DataTable dtTable = new DataTable();
        private List<TeacherInfoModel> giaoViens;

        private void btnGetData_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                richTextBox1.Text = "Run First Sheet";
                backgroundWorker1.RunWorkerAsync();
            }
        }
    }
}
