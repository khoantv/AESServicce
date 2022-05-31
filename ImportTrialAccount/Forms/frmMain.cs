using ImportTrialAccount.Models;
using ImportTrialAccount.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportTrialAccount.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;

            TempData.URL = ConfigurationManager.AppSettings["api-url"];

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

        private BackgroundWorker backgroundWorker1;
        private string filePath = null, sheetName;
        private List<string> sheetNames;
        private bool isOpen = false, isAllowCheck = false;
        private DataTable dtTable = new DataTable();
        private List<GiaoVienTrial> giaoViens;
        private List<EducationDepartmentGetIdCodeNameModel> educationDepartments;
        private List<string> headerNames;
        private string mess = "";

        #region Button Click
        private void btnOpen_Click(object sender, EventArgs e)
        {
            // open dialog box to select file
            OpenFileDialog file = new OpenFileDialog();
            // dilog box title name
            file.Title = "Word File";
            // set initial directory of computer system
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            file.InitialDirectory = Path.Combine(projectDirectory, "DataSample");
            // set restore directory
            file.RestoreDirectory = true;

            // execute if block when dialog result box click ok button
            if (file.ShowDialog() == DialogResult.OK)
            {
                // store selected file path
                filePath = file.FileName.ToString();
                textBox1.Text = filePath;
                isOpen = true;

                sheetNames = new List<string>();

                try
                {
                    sheetNames = ExcelService.GetSheetNames(filePath);

                    cbbSheetName.Items.Clear();
                    for (int i = 0; i < sheetNames.Count; i++)
                    {
                        cbbSheetName.Items.Add(sheetNames[i]);
                    }

                    cbbSheetName.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                isOpen = false;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (isOpen)
            {
                sheetName = cbbSheetName.Text;  //txtSheetName.Text.Trim();

                try
                {
                    DataTable dt = ReadExcel(filePath, sheetName);
                    if (dt != null)
                    {
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Không đọc được file.");
                }
            }
        }

        private async void btnImport_ClickAsync(object sender, EventArgs e)
        {
            await ImportDataAsync();
            MessageBox.Show("Import Done!");
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmChangePass frmChangePass = new frmChangePass();
            frmChangePass.ShowDialog();
        }

        private async void btnAutoImport_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Value = progressBar1.Minimum;
                lblProgress.Text = "Run First Sheet";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                HTSService.ExportExcel(saveFileDialog1.FileName, sheetName, headerNames, giaoViens);
                MessageBox.Show("Saved!");
            }
        }
        #endregion

        private async void frmMain_Load(object sender, EventArgs e)
        {
            var getEduDepart = await HTSService.GetAllEducationDepartment();

            if (getEduDepart != null && getEduDepart.status == "success" && getEduDepart.errors == null)
            {
                educationDepartments = getEduDepart.data;
                isAllowCheck = true;
            }
            else
            {
                MessageBox.Show("Không lấy được dữ liệu Sở giáo dục.");
                isAllowCheck = false;
            }
        }

        #region Menu Click
        private void changePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePass frmChangePass = new frmChangePass();
            frmChangePass.ShowDialog();
        }

        private void resetPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResetPass frmChangePass = new frmResetPass();
            frmChangePass.ShowDialog();
        }

        private void setTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetTraining frmSetTraining = new frmSetTraining();
            frmSetTraining.ShowDialog();
        }

        private void updateEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateEmail frmUpdateEmail = new frmUpdateEmail();
            frmUpdateEmail.ShowDialog();
        }
        #endregion

        #region Worker
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Import Thành công!");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
            {
                try
                {
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dtTable;
                    }

                    lblProgress.Text = e.ProgressPercentage + " %" + ". Process Sheet " + sheetName + ".";
                    progressBar1.Value = e.ProgressPercentage;
                    progressBar1.PerformStep();
                    //Application.DoEvents();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int numberSheets = sheetNames.Count;

            try
            {
                for (int i = 0; i < numberSheets; i++)
                {
                    // Read data sheet
                    sheetName = sheetNames[i];

                    dtTable = ReadExcel(filePath, sheetName);

                    // Import
                    var importResult = ImportDataAsync();
                    importResult.Wait();

                    // Export
                    string filePathOutput = txtOutput.Text.Trim();
                    if (!Directory.Exists(filePathOutput))
                    {
                        Directory.CreateDirectory(filePathOutput);
                    }

                    try
                    {
                        filePathOutput = Path.Combine(filePathOutput, sheetName + ".xlsx");
                        HTSService.ExportExcel(filePathOutput, sheetName, headerNames, giaoViens);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    backgroundWorker1.ReportProgress((int)(i / numberSheets * 100));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            backgroundWorker1.ReportProgress(100);
        }
        #endregion

        private void setURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUrl frm = new frmUrl();
            frm.ShowDialog();
        }

        private void insertTeacherTemplate2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportTeacher frmImport = new frmImportTeacher();
            frmImport.ShowDialog();
        }

        private void insertSchoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertSchool frmInsertSchool = new frmInsertSchool();
            frmInsertSchool.ShowDialog();
        }

        DataTable ReadExcel(string filePath, string sheetName)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            int rowIndex = 0, lastRowNum = 0;

            if (string.IsNullOrEmpty(sheetName)) return null;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheet(sheetName);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                headerNames = new List<string>();
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null) continue;

                    dtTable.Columns.Add(cell.ToString());
                    headerNames.Add(cell.ToString());
                }

                rowIndex = sheet.FirstRowNum + 1;
                lastRowNum = sheet.LastRowNum;
                giaoViens = new List<GiaoVienTrial>();

                for (int i = rowIndex; i <= lastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    #region Lưu dữ liệu dạng object
                    GiaoVienTrial giaoVienTrial = new GiaoVienTrial();

                    giaoVienTrial.STT = (row.GetCell(0) != null ? int.Parse(row.GetCell(0).ToString()) : 0);
                    giaoVienTrial.TenSo = (row.GetCell(1) != null ? row.GetCell(1).ToString() : string.Empty);
                    giaoVienTrial.IdSo = (row.GetCell(2) != null ? row.GetCell(2).ToString().Trim() : string.Empty);
                    giaoVienTrial.TenTaiKhoan = (row.GetCell(3) != null ? row.GetCell(3).ToString().Trim() : string.Empty);
                    giaoVienTrial.MatKhau = (row.GetCell(4) != null ? row.GetCell(4).ToString().Trim() : string.Empty);
                    giaoVienTrial.MonHoc = (row.GetCell(5) != null ? row.GetCell(5).ToString().Trim() : string.Empty);
                    giaoVienTrial.IdKhoaTapHuan = (row.GetCell(6) != null ? row.GetCell(6).ToString().Trim() : string.Empty);

                    if (!string.IsNullOrEmpty(giaoVienTrial.IdKhoaTapHuan))
                    {
                        if (giaoVienTrial.IdKhoaTapHuan.Contains(","))
                        {
                            giaoVienTrial.IdKhoaTapHuans = giaoVienTrial.IdKhoaTapHuan.Trim().Split(",").ToList();
                        }
                        else
                        {
                            giaoVienTrial.IdKhoaTapHuans = new List<string> { giaoVienTrial.IdKhoaTapHuan.ToString().Trim() };
                        }
                    }
                    giaoViens.Add(giaoVienTrial);
                    #endregion

                    #region Lưu dữ liệu vào DataTable để hiển thị
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            rowList.Add(row.GetCell(j).ToString());
                        }
                    }

                    if (rowList.Count > 0)
                        dtTable.Rows.Add(rowList.ToArray());
                    rowList.Clear();
                    #endregion
                }
            }
            return dtTable;
        }

        async Task ImportDataAsync()
        {
            int? educationDepartmentId = 0;
            if (!isAllowCheck)
            {
                return;
            }

            if (giaoViens == null || giaoViens.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu import!");
                return;
            }

            int gvNum = giaoViens.Count;

            for (int i = 0; i < gvNum; i++)
            {
                var educationDepartment = educationDepartments.FirstOrDefault(ed => ed.Code == giaoViens[i].IdSo);

                if (educationDepartment == null)
                {
                    educationDepartmentId = 0;
                }
                else
                {
                    educationDepartmentId = educationDepartment.EducationDepartmentId;
                }

                var gv = new CreateNewTeacherRequest
                {
                    code = giaoViens[i].TenTaiKhoan,
                    name = giaoViens[i].TenTaiKhoan,
                    PassWord = giaoViens[i].MatKhau,
                    educationDepartmentId = educationDepartmentId,
                    email = giaoViens[i].TenTaiKhoan + "@gmail.com"
                };

                try
                {
                    var createNewTeacherResponse = await HTSService.CreateNewTeacher(gv);

                    if (createNewTeacherResponse != null && createNewTeacherResponse.status == "success" && createNewTeacherResponse.errors == null)
                    {
                        var teacherInserted = createNewTeacherResponse.data;
                        giaoViens[i].UserId = teacherInserted.userId;
                        giaoViens[i].TeacherId = teacherInserted.teacherId;
                        giaoViens[i].KetQua = "Thành Công";

                        #region Gán khoá tập huấn
                        if (giaoViens[i].IdKhoaTapHuans != null && giaoViens[i].IdKhoaTapHuans.Count > 0)
                        {
                            List<bool> lsResult = new List<bool>();
                            foreach (string idTrainging in giaoViens[i].IdKhoaTapHuans)
                            {
                                try
                                {
                                    var InsertPermissionResponse = await HTSService.TeacherTrainingPermissonInsert(giaoViens[i].TeacherId, int.Parse(idTrainging));
                                    if (InsertPermissionResponse != null && InsertPermissionResponse.status == "success" && InsertPermissionResponse.errors == null)
                                    {
                                        lsResult.Add(true);
                                    }
                                    else
                                    {
                                        lsResult.Add(false);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    giaoViens[i].KetQuaGanKTH = "Thất Bại";
                                }
                            }

                            if (lsResult.All(x => x == true))
                            {
                                giaoViens[i].KetQuaGanKTH = "Thành Công";
                            }
                            else
                            {
                                giaoViens[i].KetQuaGanKTH = "Thất Bại";
                            }
                        }
                        else
                        {
                            giaoViens[i].KetQuaGanKTH = "Không có khoá tập huấn.";
                        }
                        #endregion
                    }
                    else
                    {
                        giaoViens[i].UserId = 0;
                        giaoViens[i].TeacherId = 0;
                        giaoViens[i].KetQua = "Thất Bại";
                        giaoViens[i].KetQuaGanKTH = "Thất Bại.";
                    }
                }
                catch (Exception ex)
                {
                    giaoViens[i].UserId = 0;
                    giaoViens[i].TeacherId = 0;
                    giaoViens[i].KetQua = "Thất Bại";
                }
            }
        }

    }
}
