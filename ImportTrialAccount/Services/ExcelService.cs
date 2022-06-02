using ImportTrialAccount.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ImportTrialAccount.Services
{
    public class ExcelService
    {
        public static List<string> GetSheetNames(string FilePath)
        {
            try
            {
                List<string> oList = new List<string>();

                byte[] CSVBytes = File.ReadAllBytes(FilePath);
                var excelStream = new MemoryStream(CSVBytes);

                DataTable dt = new DataTable();

                //get the uploaded file name  
                string FileName = Path.GetFileName(FilePath);

                //get the extension of uploaded excel file  
                var FileExt = Path.GetExtension(FileName);

                if (FileExt == ".xls")
                {
                    //HSSWorkBook object will read the Excel 97-2000 formats  
                    HSSFWorkbook hssfwb = new HSSFWorkbook(excelStream);

                    for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                    {
                        string SheetName = hssfwb.GetSheetName(i).Trim().Replace(" ", "").ToUpper();

                        if (!string.IsNullOrEmpty(SheetName))
                        {
                            oList.Add(SheetName);
                        }
                    }
                }
                else if (FileExt == ".xlsx")
                {
                    //XSSFWorkBook will read 2007 Excel format  
                    XSSFWorkbook hssfwb = new XSSFWorkbook(excelStream);

                    for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                    {
                        string SheetName = hssfwb.GetSheetName(i).Trim().Replace(" ", "").ToUpper();

                        if (!string.IsNullOrEmpty(SheetName))
                        {
                            oList.Add(SheetName);
                        }
                    }
                }

                return oList;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("File đang mở");
            }
        }

        public static void Export2ExcelResetResult(string fileName, string sheetName, List<string> headerNames, List<GiaoVienResetPass> giaoViens)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);

                headerNames.Add("Kết quả");

                int cellIndex = 0;
                foreach (string name in headerNames)
                {
                    row.CreateCell(cellIndex).SetCellValue(name);
                    cellIndex++;
                }

                int rowIndex = 1;
                int colLength = headerNames.Count;

                foreach (var gv in giaoViens)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    for (cellIndex = 0; cellIndex < colLength; cellIndex++)
                    {
                        row.CreateCell(cellIndex).SetCellValue(gv[cellIndex]);
                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }

        public static void Export2ExcelChangePassResult(string fileName, string sheetName, List<string> headerNames, List<GiaoVienResetPass> giaoViens)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);

                headerNames.Add("Kết quả");

                int cellIndex = 0;
                foreach (string name in headerNames)
                {
                    row.CreateCell(cellIndex).SetCellValue(name);
                    cellIndex++;
                }

                int rowIndex = 1;
                int colLength = headerNames.Count;

                foreach (var gv in giaoViens)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    for (cellIndex = 0; cellIndex < colLength; cellIndex++)
                    {
                        row.CreateCell(cellIndex).SetCellValue(gv[cellIndex]);
                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }

        public static void ExportTeacherTrainingPermissonInsertResult(string fileName, string sheetName, List<string> headerNames, List<TeacherTrainingPermission> giaoViens)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);

                headerNames.Add("Kết quả");

                int cellIndex = 0;
                foreach (string name in headerNames)
                {
                    row.CreateCell(cellIndex).SetCellValue(name);
                    cellIndex++;
                }

                int rowIndex = 1;
                int colLength = headerNames.Count;

                foreach (var gv in giaoViens)
                {
                    row = excelSheet.CreateRow(rowIndex);

                    for (cellIndex = 0; cellIndex < colLength; cellIndex++)
                    {
                        var s123 = gv[cellIndex].GetType().Name;
                        string val = "";
                        if (s123 == "List`1")
                        {
                            foreach (string s in gv[cellIndex])
                            {
                                val += s + ",";
                            }
                            if (gv[cellIndex].Count > 0)
                            {
                                val = val.Remove(val.Length - 1);
                            }
                            row.CreateCell(cellIndex).SetCellValue(val);
                        }
                        else
                        {
                            row.CreateCell(cellIndex).SetCellValue(gv[cellIndex]);
                        }
                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }

        public static void Export2ExcelChangeEmailResult(string fileName, string sheetName, List<string> headerNames, List<GiaoVienUpdateEmailModel> giaoViens)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);

                headerNames.Add("Kết quả");

                int cellIndex = 0;
                foreach (string name in headerNames)
                {
                    row.CreateCell(cellIndex).SetCellValue(name);
                    cellIndex++;
                }

                int rowIndex = 1;
                int colLength = headerNames.Count;

                foreach (var gv in giaoViens)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    for (cellIndex = 0; cellIndex < colLength; cellIndex++)
                    {
                        row.CreateCell(cellIndex).SetCellValue(gv[cellIndex]);
                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }

        public static void Export2ExcelImportSchoolResult(string fileName, string sheetName, List<string> headerNames, List<SchoolInsertModel> schools)
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                List<string> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);

                headerNames.Add("Id Trường");
                headerNames.Add("Code");
                headerNames.Add("Kết quả");

                int cellIndex = 0;
                foreach (string name in headerNames)
                {
                    row.CreateCell(cellIndex).SetCellValue(name);
                    cellIndex++;
                }

                int rowIndex = 1;
                int colLength = headerNames.Count;

                foreach (var gv in schools)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    for (cellIndex = 0; cellIndex < colLength; cellIndex++)
                    {
                        try
                        {
                            row.CreateCell(cellIndex).SetCellValue(gv[cellIndex] ?? "");
                        }
                        catch (System.Exception)
                        {
                        }

                    }

                    rowIndex++;
                }

                workbook.Write(fs);
            }
        }
    }
}
