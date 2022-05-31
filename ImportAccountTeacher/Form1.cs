using ImportAccountTeacher.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ImportAccountTeacher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string filePath = null;
        private Boolean isOpen = false;
        private DataTable dtTable;

        private void btnOpen_Click(object sender, System.EventArgs e)
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
                //DataTable dt = ReadExcel(filePath);
                //if (dt != null)
                //{
                //    dataGridView1.DataSource = dt;
                //}
            }
            var ls = ReadExcel(filePath);
        }

        List<Phong> ReadExcel(string filePath)
        {
            string sheetName = txtSheetName.Text.Trim();
            if (string.IsNullOrEmpty(sheetName)) return null;

            dtTable = new DataTable();
            List<Phong> lsPhong = new List<Phong>();
            List<string> rowList = new List<string>();
            ISheet sheet;
            int rowIndex = 0, lastRowNum = 0;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                //sheet = xssWorkbook.GetSheetAt(0);
                sheet = xssWorkbook.GetSheet(sheetName);

                // Đọc dữ liệu header
                IRow headerRow = sheet.GetRow(sheet.FirstRowNum); //sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null) continue;

                    dtTable.Columns.Add(cell.ToString());
                }

                rowIndex = sheet.FirstRowNum + 1;
                lastRowNum = sheet.LastRowNum;

                Phong phong = new Phong();
                Truong truong = new Truong();

                // Đọc toàn bộ file
                for (int i = rowIndex; i <= lastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    string colB = string.Empty, // Họ tên
                           colC = string.Empty, // Mã/Tên Phòng
                           colD = string.Empty, // Mã/Tên Trường
                           colE = string.Empty, // Môn
                           colF = string.Empty, // Điện thoại
                           colG = string.Empty; // Email

                    string colNB = string.Empty, // Họ tên
                           colNC = string.Empty, // Mã/Tên Phòng
                           colND = string.Empty, // Mã/Tên Trường
                           colNE = string.Empty, // Môn
                           colNF = string.Empty, // Điện thoại
                           colNG = string.Empty; // Email

                    if (row.GetCell(1) != null) colB = row.GetCell(1).ToString();
                    if (row.GetCell(2) != null) colC = row.GetCell(2).ToString();
                    if (row.GetCell(3) != null) colD = row.GetCell(3).ToString();
                    if (row.GetCell(4) != null) colE = row.GetCell(4).ToString();
                    if (row.GetCell(5) != null) colF = row.GetCell(5).ToString();
                    if (row.GetCell(6) != null) colG = row.GetCell(6).ToString();

                    // Nếu dòng không chứa dữ liệu (D, E, F, G = blank) => Dữ liệu phòng/trường
                    // Nếu dòng tiếp theo cũng không chứa dữ liệu (D, E, F, G = blank) thì Dòng hiện tại là Phòng, Dòng tiếp theo là Trường
                    // Nếu dòng tiếp theo cũng chứa dữ liệu (D, E, F, G != blank)
                    // Dòng hiện tại là Trường. Dòng tiếp theo là Thông tin Giáo viên
                    if (!string.IsNullOrEmpty(colB) && !string.IsNullOrEmpty(colC) &&
                        string.IsNullOrEmpty(colD) && string.IsNullOrEmpty(colE) &&
                        string.IsNullOrEmpty(colF) && string.IsNullOrEmpty(colG))
                    {
                        // Lấy dòng tiếp theo (cần kiểm tra là dòng cuối)
                        IRow nextOneRow = sheet.GetRow(i + 1);

                        if (nextOneRow != null)
                        {
                            if (nextOneRow.GetCell(1) != null) colNB = nextOneRow.GetCell(1).ToString();
                            if (nextOneRow.GetCell(2) != null) colNC = nextOneRow.GetCell(2).ToString();
                            if (nextOneRow.GetCell(3) != null) colND = nextOneRow.GetCell(3).ToString();
                            if (nextOneRow.GetCell(4) != null) colNE = nextOneRow.GetCell(4).ToString();
                            if (nextOneRow.GetCell(5) != null) colNF = nextOneRow.GetCell(5).ToString();
                            if (nextOneRow.GetCell(6) != null) colNG = nextOneRow.GetCell(6).ToString();

                            // Nếu dòng tiếp theo cũng không chứa dữ liệu(D, E, F, G = blank)
                            // Có 2 trường hợp
                            // 1. Bắt đầu 1 Phòng mới.
                            // 2. Bắt đầu 1 Trường mới.
                            if (!string.IsNullOrEmpty(colNB) && !string.IsNullOrEmpty(colNC) &&
                                string.IsNullOrEmpty(colND) && string.IsNullOrEmpty(colNE) &&
                                string.IsNullOrEmpty(colNF) && string.IsNullOrEmpty(colNG))
                            {
                                // Nếu Bắt đầu 1 Phòng mới. Thì dòng tiếp theo cũng sẽ trống
                                // Lưu trường, phòng cũ, tạo phòng mới
                                IRow nextTwoRow = sheet.GetRow(i + 2);
                                if (nextTwoRow != null)
                                {
                                    string colNNB, colNNC, colNND, colNNE, colNNF, colNNG;
                                    colNNB = colNNC = colNND = colNNE = colNNF = colNNG = string.Empty;
                                    if (nextTwoRow.GetCell(1) != null) colNNB = nextTwoRow.GetCell(1).ToString();
                                    if (nextTwoRow.GetCell(2) != null) colNNC = nextTwoRow.GetCell(2).ToString();
                                    if (nextTwoRow.GetCell(3) != null) colNND = nextTwoRow.GetCell(3).ToString();
                                    if (nextTwoRow.GetCell(4) != null) colNNE = nextTwoRow.GetCell(4).ToString();
                                    if (nextTwoRow.GetCell(5) != null) colNNF = nextTwoRow.GetCell(5).ToString();
                                    if (nextTwoRow.GetCell(6) != null) colNNG = nextTwoRow.GetCell(6).ToString();

                                    if (!string.IsNullOrEmpty(colNNB) && !string.IsNullOrEmpty(colNNC) &&
                                         string.IsNullOrEmpty(colNND) && string.IsNullOrEmpty(colNNE) &&
                                         string.IsNullOrEmpty(colNNF) && string.IsNullOrEmpty(colNNG))
                                    {
                                        if (truong.MaTruong != null && truong.TenTruong != null & truong.GiaoViens != null) phong.Truongs.Add(truong);
                                        if (phong.MaPhong != null && phong.TenPhong != null & phong.Truongs != null) lsPhong.Add(phong);


                                    }
                                }

                                if (truong.MaTruong != null && truong.TenTruong != null & truong.GiaoViens != null) phong.Truongs.Add(truong);
                                if (phong.MaPhong != null && phong.TenPhong != null & phong.Truongs != null) lsPhong.Add(phong);

                                // Dòng hiện tại là Phòng, Dòng tiếp theo là Trường
                                phong = new Phong();
                                phong.TenPhong = colB;
                                phong.MaPhong = colC;
                                phong.Truongs = new List<Truong>();

                                truong = new Truong();
                                truong.TenTruong = colNB;
                                truong.MaTruong = colNC;
                                truong.GiaoViens = new List<GiaoVien>();

                                i = i + 1; // Bỏ qua dòng Trường
                                continue;
                            }
                            else // Nếu dòng tiếp theo chứa dữ liệu (D, E, F, G != blank)
                            {
                                // Dòng hiện tại là Trường. Dòng tiếp theo là Thông tin Giáo viên
                                // Lưu trường cũ, tạo trường mới
                                phong.Truongs.Add(truong);

                                truong = new Truong();
                                truong.TenTruong = colNB;
                                truong.MaTruong = colNC;
                                truong.GiaoViens = new List<GiaoVien>();

                                continue;
                            }
                        }
                    }
                    else // Nếu dòng chứa dữ liệu (D, E, F, G = blank) => Dữ liệu giáo viên
                    {
                        GiaoVien gv = new GiaoVien();
                        gv.HoTen = colB;
                        gv.DienThoai = colF;
                        gv.Email = colG;

                        Dictionary<string, string> monHocs = new Dictionary<string, string>();

                        if (colE.Contains(','))
                        {
                            var mons = colE.Split(',');
                            foreach (var mon in mons)
                            {
                                monHocs.Add(mon.Trim(), mon.Trim());
                            }
                        }
                        else if (colE.Contains('-'))
                        {
                            var mons = colE.Split('-');
                            foreach (var mon in mons)
                            {
                                monHocs.Add(mon.Trim(), mon.Trim());
                            }
                        }
                        else
                        {
                            monHocs.Add(colE.Trim(), colE.Trim());
                        }

                        gv.MonHocs = monHocs;

                        if (truong.GiaoViens == null) truong.GiaoViens = new List<GiaoVien>();
                        truong.GiaoViens.Add(gv);
                    }
                }

                //for (int i = rowIndex; i <= lastRowNum; i++)
                //{
                //    IRow row = sheet.GetRow(i);
                //    if (row == null) continue;

                //    for (int j = row.FirstCellNum; j < cellCount; j++)
                //    {
                //        if (row.GetCell(j) != null)
                //        {
                //            rowList.Add(row.GetCell(j).ToString());
                //        }
                //    }

                //    if (rowList.Count > 0)
                //        dtTable.Rows.Add(rowList.ToArray());
                //    rowList.Clear();
                //}

                return lsPhong;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

        }
    }
}
