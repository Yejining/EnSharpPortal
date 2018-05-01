using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;
using EnSharpPortal.Source.Data;
using System.Runtime.InteropServices;

namespace EnSharpPortal.Source.Main
{
    class FileIOManager
    {
        /// <summary>
        /// 엑셀파일을 열고 읽는 메소드입니다.
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="rows">엑셀 시작점</param>
        /// <param name="columns">엑셀 끝점</param>
        /// <returns>저장된 엑셀 배열</returns>
        public Array OpenAndReadFile(string path, string rows, string columns)
        {
            try
            {
                //  Excel Application 객체 생성
                Excel.Application ExcelApp = new Excel.Application();

                // Workbook 객체 생성 및 파일 오픈
                Excel.Workbook workbook = ExcelApp.Workbooks.Open(@path);

                //sheets에 읽어온 엑셀값을 넣기
                Excel.Sheets sheets = workbook.Sheets;

                // 특정 sheet의 값 가져오기
                Excel.Worksheet worksheet = sheets["Sheet1"] as Excel.Worksheet;

                // 범위 설정
                Excel.Range cellRange = worksheet.get_Range(rows, columns) as Excel.Range;
                // 설정한 범위만큼 데이터 담기
                Array data = cellRange.Cells.Value2;
                
                ExcelApp.Workbooks.Close();
                ExcelApp.Quit();

                return data;
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 엑셀 파일을 작성하는 메소드입니다.
        /// </summary>
        /// <param name="fileName">파일 이름</param>
        /// <param name="lecturesInExcelForm">엑셀로 저장할 데이터</param>
        public void CreateExcelFile(string fileName, string[,] lecturesInExcelForm)
        {
            string excelPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + fileName + ".xls";

            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                excelApp = new Excel.Application();

                wb = excelApp.Workbooks.Add(misValue);
                ws = (Excel.Worksheet)wb.Worksheets.get_Item(1);
                
                // 정보 입력
                for (int row = 0; row < 25; row++)
                    for (int column = 0; column < 6; column++)
                        ws.Cells[row + 1, column + 1] = lecturesInExcelForm[row, column];
                
                // 엑셀 파일로 저장
                wb.SaveAs(excelPath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                wb.Close();
                excelApp.Quit();

                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }
        }

        /// <summary>
        /// Excel 데이터를 Release해주는 메소드입니다.
        /// </summary>
        /// <param name="obj"></param>
        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
