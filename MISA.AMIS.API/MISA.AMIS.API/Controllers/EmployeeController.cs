using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interface.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeService _employeeService;
        IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService) : base(employeeService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        /// <summary>
        /// Hàm chia trang bằng filter
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns>response</returns>
        /// StatusCode: 400 - Dữ kiệu đầu vào không hợp lệ
        /// StatusCode: 500 - Có lỗi xảy ra phía server (exception,...)
        /// Created: TDDung
        /// Date: 10/5/2021
        [HttpGet("paging")]
        public IActionResult GetPaging(int pageIndex, int pageSize, string filter)
        {
            var employees = _employeeService.GetPaging(pageIndex, pageSize, filter);
            if (employees != null)
            {
                return Ok(employees);
            }
            else
            {
                return NoContent();
            }
        }


        /// <summary>
        /// Hàm xuất ra file excel
        /// </summary>
        /// <returns></returns>
        /// StatusCode: 500 - Có lỗi xảy ra phía server (exception,...)
        /// Created: TDDung
        /// Date: 10/5/2021
        [HttpGet("export")]
        public  IActionResult Export()
        {
            var result =  _employeeService.Export();
            if (result != null)
            {
                return File(result.FileStream, result.FileContent, result.FileName);
            }
            else
            {
                return NoContent();
            }
        
    }

        /// <summary>
        /// Hàm lấy bản ghi lớn nhất
        /// </summary>
        /// <returns></returns>
        /// StatusCode: 500 - Có lỗi xảy ra phía server (exception,...)
        /// Created: TDDung
        /// Date: 10/5/2021
        [HttpGet("getMaxCode")]
        public IActionResult GetMaxCode()
        {
            var count = _employeeService.GetMaxCode();
            if (count.Count() > 0)
            {
                return Ok(count);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpGet("employeeMaxCode/{id}")]
        public ActionResult GetEmployeeMaxCodeById(Guid id)
        {
            var employee = _employeeService.GetEmployeeMaxCodeById(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            else return NoContent();
        }
    }
}
