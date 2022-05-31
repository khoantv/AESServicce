using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;

namespace ImportAccountTeacher.Services
{
    /// <summary>
    /// Bao gồm các class hỗ trợ đọc dữ liệu từ file Excel.
    /// Thư viện hỗ trợ: Microsoft.Office.Interop.Excel;
    /// </summary>
    class ExcelImporterBase
    {
        public static System.Data.DataTable ReadFile(string filePath)
        {
            //get file extenstion
            string extension = Path.GetExtension(filePath);

            string conString = string.Empty;
            //check file extension
            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1'", filePath);
                    break;
                case ".xlsx": //Excel 07 and above.
                    conString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1'", filePath);
                    break;
            }

            //create datatable object
            System.Data.DataTable dt = new System.Data.DataTable();
            //conString = string.Format(conString, filePath);

            //Use OldDb to read excel
            using (OleDbConnection connExcel = new OleDbConnection(conString))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        //Get the name of First Sheet.
                        connExcel.Open();
                        System.Data.DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });                      //connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        //Read Data from First Sheet.
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }

            //bind datatable with GridView
            return dt;
        }

        public static List<List<string>> ReadFile(String filePath, string SheetName = "Sheet1", int startRowIndex = 1)
        {
            List<List<string>> Result = new List<List<string>>();

            //create the Application object we can use in the member functions.
            Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
            _excelApp.Visible = false;

            string fileName = filePath;

            //open the workbook
            Workbook workbook = _excelApp.Workbooks.Open(fileName,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            //select the first sheet        
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

            //find the used range in worksheet
            Microsoft.Office.Interop.Excel.Range excelRange = worksheet.UsedRange;

            //get an object array of all of the cells in the worksheet (their values)
            object[,] valueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

            //access the cells
            for (int row = 1; row <= worksheet.UsedRange.Rows.Count; row++)
            {
                List<string> Line = new List<string>();
                for (int col = 1; col <= worksheet.UsedRange.Columns.Count; col++)
                {
                    //access each cell
                    var Value = valueArray[row + startRowIndex, col];
                    Line.Add(Value != null ? Value.ToString().Trim() : "");
                }

                Result.Add(Line);
            }

            //clean up stuffs
            workbook.Close(false, Type.Missing, Type.Missing);
            Marshal.ReleaseComObject(workbook);

            _excelApp.Quit();
            Marshal.FinalReleaseComObject(_excelApp);

            return Result;
        }
    }
}
