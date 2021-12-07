using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logistics.Utility
{
    public abstract class ExcelReportBase
    {
        protected Application Excel { get; set; }
        protected Workbook Workbook { get; set; }
        protected Worksheet Worksheet { get; set; }
        protected abstract string TemplatePath { get; set; }

        public virtual void BuildReport()
        {
            Configure();
            EditTemplate();
            SaveReport();
        }

        protected virtual void Configure()
        {
            Excel = new Application();
            Workbook = Excel.Workbooks.Open(TemplatePath);
            Worksheet = Workbook.Worksheets[1] as Worksheet;
        }

        protected abstract void EditTemplate();

        protected virtual void SaveReport()
        {
            Directory.CreateDirectory(GetFolderPath());

            var filePath = GetFolderPath() + "\\" + GetDocName();

            Workbook.SaveAs(filePath, XlFileFormat.xlWorkbookDefault,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                XlSaveAsAccessMode.xlShared, Type.Missing, Type.Missing);
        }

        private string GetFolderPath()
            => Directory.GetCurrentDirectory() + "\\Reports";

        private string GetDocName()
            => $"Report_{DateTime.Now:dd-MM-yyyy_hh:mm:ss}.xlsx";
    }
}
