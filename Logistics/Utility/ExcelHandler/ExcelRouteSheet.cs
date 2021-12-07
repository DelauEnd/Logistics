using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logistics.Utility.ExcelHandler
{
    public class ExcelRouteSheet : ExcelReportBase
    {
        protected override string TemplatePath { get; set; }
        private Guid RouteId { get; set; }
        public ExcelRouteSheet(Guid routeId)
        {
            TemplatePath = GetTemplatePath();
        }

        private string GetTemplatePath()
            => Directory.GetCurrentDirectory() + "\\ReportTemplates\\RouteSheetTemplate.xlsx";

        protected override void EditTemplate()
        {
            Worksheet.Range["A1"].Value = "Тест";
        }
    }
}
