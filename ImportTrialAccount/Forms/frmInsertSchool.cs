using ImportTrialAccount.Models;
using ImportTrialAccount.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportTrialAccount.Forms
{
    public partial class frmInsertSchool : Form
    {
        public frmInsertSchool()
        {
            InitializeComponent();

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        private BackgroundWorker backgroundWorker1;
        private string filePath = null, sheetName;
        private List<string> sheetNames;
        private bool isOpen = false, isAllowCheck = false;
        private DataTable dtTable = new DataTable();
        private List<SchoolInsertModel> schools;

        private void frmInsertSchool_Load(object sender, EventArgs e)
        {
        }

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
                sheetName = cbbSheetName.Text;

                try
                {
                    DataTable dt = ReadExcel(filePath, sheetName);
                    if (dt != null)
                    {
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không đọc được file.");
                }
            }
        }

        private async void btnImport_ClickAsync(object sender, EventArgs e)
        {
            string code = string.Empty;

            if (schools == null || schools.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu import!");
                return;
            }

            int schoolNum = schools.Count;

            for (int i = 0; i < schoolNum; i++)
            {
                var schoolCodeResult = await HTSService.GetSchoolCode(schools[i].IdPhong, schools[i].LoaiTruong);

                if (schoolCodeResult != null && schoolCodeResult.status == "success" && schoolCodeResult.errors == null)
                {
                    code = schoolCodeResult.data;
                }

                if (string.IsNullOrEmpty(code))
                {
                    schools[i].IdTruong = 0;
                    schools[i].Code = "";
                    schools[i].KetQua = "Thất Bại";
                }
                else
                {
                    var sc = new CreateNewSchoolRequest
                    {
                        code = code,
                        name = schools[i].TenTruong,
                        educationDepartmentId = schools[i].IdSo,
                        phongGDDTId = schools[i].IdPhong,
                        typeId = schools[i].LoaiTruong
                    };

                    try
                    {
                        var createNewSchoolResponse = await HTSService.CreateNewSchool(sc);

                        if (createNewSchoolResponse != null && createNewSchoolResponse.status == "success" && createNewSchoolResponse.errors == null)
                        {
                            var schoolInserted = createNewSchoolResponse.data;
                            schools[i].IdTruong = schoolInserted.schoolId;
                            schools[i].Code = schoolInserted.code;
                            schools[i].KetQua = "Thành Công";
                        }
                        else
                        {
                            schools[i].IdTruong = 0;
                            schools[i].Code = "";
                            schools[i].KetQua = "Thất Bại";
                        }
                    }
                    catch (Exception ex)
                    {
                        schools[i].IdTruong = 0;
                        schools[i].Code = "";
                        schools[i].KetQua = "Thất Bại";
                    }
                }
            }

            Export();
        }

        private async void btnAutoImport_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
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
                        ExcelService.Export2ExcelImportSchoolResult(filePathOutput, sheetName, headerNames, schools);
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

        #region Function

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
                schools = new List<SchoolInsertModel>();

                for (int i = rowIndex; i <= lastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    #region Lưu dữ liệu dạng object
                    SchoolInsertModel schoolInsertModel = new SchoolInsertModel();

                    schoolInsertModel.STT = (row.GetCell(0) != null ? int.Parse(row.GetCell(0).ToString()) : 0);
                    schoolInsertModel.TenTruong = (row.GetCell(1) != null ? row.GetCell(1).ToString() : string.Empty);
                    schoolInsertModel.TenPhong = (row.GetCell(2) != null ? row.GetCell(2).ToString().Trim() : string.Empty);
                    if (row.GetCell(3) != null)
                    {
                        schoolInsertModel.IdPhong = int.Parse(row.GetCell(3).ToString());
                    }
                    schoolInsertModel.TenSo = (row.GetCell(4) != null ? row.GetCell(4).ToString().Trim() : string.Empty);
                    if (row.GetCell(5) != null)
                    {
                        schoolInsertModel.IdSo = int.Parse(row.GetCell(5).ToString());
                    }
                    schoolInsertModel.LoaiHinh = (row.GetCell(6) != null ? row.GetCell(6).ToString().Trim() : string.Empty);
                    schoolInsertModel.LoaiTruong = (row.GetCell(7) != null ? int.Parse(row.GetCell(7).ToString().Trim()) : 0);

                    schools.Add(schoolInsertModel);
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
            string code = string.Empty;

            if (!isAllowCheck)
            {
                return;
            }

            if (schools == null || schools.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu import!");
                return;
            }

            int schoolNum = schools.Count;

            for (int i = 0; i < schoolNum; i++)
            {
                var schoolCodeResult = await HTSService.GetSchoolCode(schools[i].IdPhong, schools[i].LoaiTruong);

                if (schoolCodeResult != null && schoolCodeResult.status == "success" && schoolCodeResult.errors == null)
                {
                    code = schoolCodeResult.data;
                }

                var sc = new CreateNewSchoolRequest
                {
                    code = code,
                    name = schools[i].TenTruong,
                    educationDepartmentId = schools[i].IdSo,
                    phongGDDTId = schools[i].IdPhong,
                    typeId = schools[i].LoaiTruong
                };

                try
                {
                    var createNewSchoolResponse = await HTSService.CreateNewSchool(sc);

                    if (createNewSchoolResponse != null && createNewSchoolResponse.status == "success" && createNewSchoolResponse.errors == null)
                    {
                        var schoolInserted = createNewSchoolResponse.data;
                        schools[i].IdTruong = schoolInserted.schoolId;
                        schools[i].Code = schoolInserted.code;
                        schools[i].KetQua = "Thành Công";
                    }
                    else
                    {
                        schools[i].IdTruong = 0;
                        schools[i].Code = "";
                        schools[i].KetQua = "Thất Bại";
                    }
                }
                catch (Exception ex)
                {
                    schools[i].IdTruong = 0;
                    schools[i].Code = "";
                    schools[i].KetQua = "Thất Bại";
                }
            }
        }

        public void Export()
        {
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = @"C:\";
            //saveFileDialog1.Title = "Save text Files";
            //saveFileDialog1.DefaultExt = "xlsx";
            //saveFileDialog1.Filter = "Excel files (*.xls)|*.xlsx|All files (*.*)|*.*";
            //saveFileDialog1.FilterIndex = 2;
            //saveFileDialog1.RestoreDirectory = true;

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    ExcelService.Export2ExcelImportSchoolResult(saveFileDialog1.FileName, sheetName, headerNames, schools);
            //    MessageBox.Show("Saved!");
            //}

            string filePathOutput = txtOutput.Text.Trim();
            if (!Directory.Exists(filePathOutput))
            {
                Directory.CreateDirectory(filePathOutput);
            }

            try
            {
                filePathOutput = Path.Combine(filePathOutput, sheetName + ".xlsx");
                ExcelService.Export2ExcelImportSchoolResult(filePathOutput, sheetName, headerNames, schools);
                MessageBox.Show("Exported");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
