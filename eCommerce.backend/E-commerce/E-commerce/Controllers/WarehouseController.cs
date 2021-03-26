using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Core.Interfaces;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        [Authorize(Roles ="Director")]
        public IActionResult Create()
        {
            try
            {
                _warehouseService.Create();
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _warehouseService.Get();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [Authorize(Roles = "Director, Admin")]
        [HttpGet("filter")]
        public IActionResult GetFilter(string date=null, string name=null)
        {
            try
            {
                var result = _warehouseService.Get(date, name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("dates")]
        public IActionResult GetDates()
        {
            try
            {
                var result = _warehouseService.GetDates();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [Authorize(Roles ="Director")]
        [HttpGet("excel/{date}")]
        public IActionResult Excel(string date)
        {
            using (var workbook = new XLWorkbook())
            {
                
                var worksheet = workbook.Worksheets.Add($"Report {date}");
                worksheet.Cell(1, 1).Value = $"Report for {date}";
                var list = _warehouseService.GetReport(date);
                worksheet.Column("A").Width = 5;
                worksheet.Column("B").Width = 30;
                worksheet.Column("C").Width = 30;
                worksheet.Column("D").Width = 10;
                worksheet.Column("E").Width = 10;
                worksheet.Column("F").Width = 10;
                worksheet.Column("G").Width = 10;
                worksheet.Column("H").Width = 10;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Range("A1:H1").Merge();
                worksheet.Cell(2, 1).InsertTable(list);
                

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Report {date}.xlsx");
                }
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("excelmonth")]
        public IActionResult ExcelMonth(string Date, int BranchId)
        {
            using (var workbook = new XLWorkbook())
            {
                WarehouseMonthFilterVM filterVM = new WarehouseMonthFilterVM
                {
                    Date = Date,
                    BranchId = BranchId
                };
                var worksheet = workbook.Worksheets.Add($"Report {filterVM.Date.Substring(3)}");
                worksheet.Cell(1, 1).Value = $"Report for {filterVM.Date.Substring(3)}";
                var list = _warehouseService.GetMonthReport(filterVM);
                worksheet.Column("A").Width = 30;
                worksheet.Column("B").Width = 30;
                worksheet.Column("C").Width = 10;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Range("A1:C1").Merge();
                worksheet.Cell(2, 1).InsertTable(list);


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Report {filterVM.Date.Substring(3)}.xlsx");
                }
            }
        }
    }
}
