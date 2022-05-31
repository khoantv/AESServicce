using ImportTrialAccount.Models;
using ImportTrialAccount.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImportTrialAccount.Forms
{
    public partial class frmSetTraining : Form
    {
        public frmSetTraining()
        {
            InitializeComponent();
        }

        string filePath = null, sheetName;
        private bool isOpen = false, isAllowCheck = false;
        private DataTable dtTable;
        private List<TeacherTrainingPermission> giaoViens;
        private List<string> headerNames;

        private void frmSetTraining_Load(object sender, EventArgs e)
        {

        }

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

                List<string> sheetNames = new List<string>();

                sheetNames = ExcelService.GetSheetNames(filePath);

                for (int i = 0; i < sheetNames.Count; i++)
                {
                    cbbSheetName.Items.Add(sheetNames[i]);
                }

                cbbSheetName.SelectedIndex = 0;
            }
            else
            {
                isOpen = false;
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
                ExcelService.ExportTeacherTrainingPermissonInsertResult(saveFileDialog1.FileName, sheetName, headerNames, giaoViens);
                MessageBox.Show("Saved!");
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (giaoViens == null || giaoViens.Count <= 0)
            {
                MessageBox.Show("Không có dữ liệu import!");
                return;
            }

            int gvNum = giaoViens.Count;

            for (int i = 0; i < gvNum; i++)
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
                        giaoViens[i].KetQua = "Thất Bại";
                    }
                }

                if (lsResult.All(x => x == true))
                {
                    giaoViens[i].KetQua = "Thành Công";
                }
                else
                {
                    giaoViens[i].KetQua = "Thất Bại";
                }
            }

            MessageBox.Show("Import Thành công!");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (isOpen)
            {
                try
                {
                    DataTable dt = ReadExcel(filePath);
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

        DataTable ReadExcel(string filePath)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            int rowIndex = 0, lastRowNum = 0;

            sheetName = cbbSheetName.Text;
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
                giaoViens = new List<TeacherTrainingPermission>();

                for (int i = rowIndex; i <= lastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    #region Lưu dữ liệu dạng object
                    TeacherTrainingPermission teacherTraining = new TeacherTrainingPermission();

                    teacherTraining.STT = (row.GetCell(0) != null ? int.Parse(row.GetCell(0).ToString()) : 0);
                    teacherTraining.TenTaiKhoan = (row.GetCell(1) != null ? row.GetCell(1).ToString() : string.Empty);
                    teacherTraining.UserId = (row.GetCell(2) != null ? long.Parse(row.GetCell(2).ToString().Trim()) : 0);
                    teacherTraining.TeacherId = (row.GetCell(3) != null ? int.Parse(row.GetCell(3).ToString()) : 0);
                    if (row.GetCell(4) != null)
                    {
                        if (row.GetCell(4).ToString().Contains(","))
                        {
                            teacherTraining.IdKhoaTapHuans = row.GetCell(4).ToString().Trim().Split(",").ToList();
                        }
                        else
                        {
                            teacherTraining.IdKhoaTapHuans = new List<string> { row.GetCell(4).ToString().Trim() };
                        }
                    }

                    giaoViens.Add(teacherTraining);
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
    }
}
