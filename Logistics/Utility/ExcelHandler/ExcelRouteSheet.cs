using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Entities.RequestFeautures;
using Logistics.Utility.ExcelHandler.RouteSheetAdditions;
using Entities.Models;
using System.Linq;
using System.Windows;

namespace Logistics.Utility.ExcelHandler
{
    public class ExcelRouteSheet : ExcelReportBase
    {
        protected override string TemplatePath { get; set; }
        private Guid RouteId { get; set; }
        List<RouteUnit> Units { get; set; }

        public ExcelRouteSheet(Guid routeId)
        {
            TemplatePath = GetTemplatePath();
            RouteId = routeId;
            Units = new List<RouteUnit>();      
            BuildRouteSheetUnits();
        }

        private string GetTemplatePath()
            => Directory.GetCurrentDirectory() + "\\ReportTemplates\\RouteSheetTemplate.xlsx";

        protected override void EditTemplate()
        {
            BuildRouteSheetUnits();
            SetUnitsInfo();
            Worksheet.Range["J4"].Value = RouteId.ToString();
            Worksheet.Range["B4"].Value = DateTime.Now;
            Worksheet.Range["B6"].Value = Units.First().Driver;
            Worksheet.Range["B8"].Value = Units.First().Date.ToString("dd.MM.yyyy") + " - " +
                Units.Last().Date.ToString("dd.MM.yyyy");         
        }

        private void SetUnitsInfo()
        {
            var ind = 1;
            foreach(var unit in Units)
            {
                Worksheet.Range["A" + (11 + ind)].Value = ind;
                Worksheet.Range["C" + (11 + ind)].Value = unit.Date;
                Worksheet.Range["E" + (11 + ind)].Value = unit.Address;
                Worksheet.Range["H" + (11 + ind)].Value = unit.Purpose == Purpose.Loading ? 
                    "Загрузка" : 
                    "Выгрузка";
                Worksheet.Range["J" + (11 + ind)].Value = unit.Number.ToString();
                ind++;
            }
        }

        private async void BuildRouteSheetUnits()
        {
            Units = await Units.Init(RouteId);
            Units = Units.SortByDate();
        }

        protected override void ReportCreated()
        {
            MessageBox.Show("Отчет создан","Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
