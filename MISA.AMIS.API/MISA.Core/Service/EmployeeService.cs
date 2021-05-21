using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interface.Repository;
using MISA.Core.Interface.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #region Hàm validate thêm nhân viên
        /// <summary>
        /// Hàm validate nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// Created: TDDung
        /// Date: 10/5/2021
        protected override void CustomValidate(Employee entity)
        {
            if (entity is Employee)
            {
                var employee = entity;
                //Tên, mã, phòng ban khách hàng không được phép để trống
                
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    throw new BadRequestException(Properties.Resources.emptyEmployeeCode);
                }
                //Check trùng mã
                var isExits = _employeeRepository.CheckEmployeeCodeExits(employee.EmployeeCode);
                if (isExits.Single())
                {
                    throw new BadRequestException(Properties.Resources.duplidateEmployeeCodeBefore + entity.EmployeeCode + Properties.Resources.duplidateEmployeeCodeAfter);
                }
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    throw new BadRequestException(Properties.Resources.emptyEmployeeName);
                }
                if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
                {
                    throw new BadRequestException(Properties.Resources.emptyEmployeePss);
                }
                

            }
        }
        #endregion

        #region Hàm validate cho sửa nhân viên
        /// <summary>
        /// Hàm validate cho sửa nhân viên
        /// </summary>
        /// <param name="entity"></param>
        protected override void CustomValidateUpdate(Employee employee)
        {
            var isExist = _employeeRepository.CheckEmployeeCodeExitsUpdate(employee.EmployeeCode, employee.EmployeeId);
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                throw new BadRequestException(Properties.Resources.emptyEmployeeCode);
            }
            if (isExist.Single())
            {
                throw new BadRequestException(Properties.Resources.duplidateEmployeeCodeBefore + employee.EmployeeCode + Properties.Resources.duplidateEmployeeCodeAfter);
            }
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                throw new BadRequestException(Properties.Resources.emptyEmployeeName);
            }
            if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
            {
                throw new BadRequestException(Properties.Resources.emptyEmployeePss);
            }
        }
        #endregion

        /// <summary>
        /// Hàm validate Paging
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public Object GetPaging(int pageIndex, int pageSize, string filter)
        {
            if (pageIndex <= 0 || pageSize <= 0) throw new BadRequestException(Properties.Resources.pagingErr);
            var employees = _employeeRepository.GetPaging(pageIndex, pageSize, filter);
            return employees;
        }
        /// <summary>
        /// Hàm nhân bản nhân viên có thêm mã lớn nhất
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Employee GetEmployeeMaxCodeById(Guid entityId)
        {
            return _employeeRepository.GetEmployeeMaxCodeById(entityId);
        }

        /// <summary>
        /// Hàm lấy mã lớn nhất
        /// </summary>
        /// <returns></returns>
        public String GetMaxCode()
        {
            var count = _employeeRepository.GetMaxCode();
            return count;
        }
        /// <summary>
        /// Hàm xuất ra file excel
        /// </summary>
        /// <returns>file export</returns>
        public FileExport Export()
        {
            // query data from database  
            var listEmployee = _employeeRepository.GetAll();
            var stream = new MemoryStream();
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(Properties.Resources.nameExcelFile);
                // Style cột cho file excel
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 30;
                workSheet.Column(4).Width = 10;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 30;
                workSheet.Column(7).Width = 30;
                workSheet.Column(8).Width = 15;
                workSheet.Column(9).Width = 30;

                using (var range = workSheet.Cells[Properties.Resources.cellFirst])
                {
                    range.Merge = true;
                    range.Value = Properties.Resources.titleFile;
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 16;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Chia cột cho file cho excel.
                workSheet.Cells[3, 1].Value = Properties.Resources.cell1;
                workSheet.Cells[3, 2].Value = Properties.Resources.cell2;
                workSheet.Cells[3, 3].Value = Properties.Resources.cell3;
                workSheet.Cells[3, 4].Value = Properties.Resources.cell4;
                workSheet.Cells[3, 5].Value = Properties.Resources.cell5;
                workSheet.Cells[3, 6].Value = Properties.Resources.cell6;
                workSheet.Cells[3, 7].Value = Properties.Resources.cell7;
                workSheet.Cells[3, 8].Value = Properties.Resources.cell8;
                workSheet.Cells[3, 9].Value = Properties.Resources.cell9;

                //Style cho chữ dưới bảng
                using (var range = workSheet.Cells[Properties.Resources.cellThird])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.Font.Bold = true;
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int i = 0;
                // Cho dữ liệu từ list vào file
                foreach (var e in listEmployee)
                {
                    workSheet.Cells[i + 4, 1].Value = i + 1;
                    workSheet.Cells[i + 4, 2].Value = e.EmployeeCode;
                    workSheet.Cells[i + 4, 3].Value = e.EmployeeName;
                    workSheet.Cells[i + 4, 4].Value = e.GenderName;
                    workSheet.Cells[i + 4, 5].Value = e.DateOfBirth != null ? e.DateOfBirth?.ToString(Properties.Resources.stringDateFormat) : "";
                    workSheet.Cells[i + 4, 6].Value = e.EmployeePosition;
                    workSheet.Cells[i + 4, 7].Value = e.DepartmentName;
                    workSheet.Cells[i + 4, 8].Value = e.BankAccountNumber;
                    workSheet.Cells[i + 4, 9].Value = e.BankName;

                    using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                    {
                        range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }

                    i++;
                }

                package.Save();
            }
            stream.Position = 0;
            string excelName = Properties.Resources.excelFileName;

            return new FileExport(stream, Properties.Resources.excelFileLink, excelName);
        }
    }
}
