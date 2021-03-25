using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Text;


namespace Core.Interfaces
{
    public interface IWarehouseService
    {
        void Create();
        List<WarehouseGetVM> Get(string Date=null);
        IEnumerable<string> GetDates();
        List<WarehouseReportVM> GetReport(string date);
        List<WarehouseMonthReportVM> GetMonthReport(WarehouseMonthFilterVM filter);

    }
}
