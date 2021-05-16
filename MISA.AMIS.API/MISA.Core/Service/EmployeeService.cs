using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interface.Repository;
using MISA.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Khai báo biến toàn cục và ctor
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

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
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    throw new BadRequestException(Properties.Resources.emptyEmployeeName);
                }
                if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
                {
                    throw new BadRequestException(Properties.Resources.emptyEmployeePss);
                }
                //Check trùng mã
                var isExits = _employeeRepository.CheckEmployeeCodeExits(employee.EmployeeCode);
                if (isExits.Single())
                {
                    throw new BadRequestException(Properties.Resources.duplidateEmployeeCode);
                }

            }
        }
        #endregion

        #region validate cho sửa nhân viên
        /// <summary>
        /// Hàm validate cho sửa nhân viên
        /// </summary>
        /// <param name="entity"></param>
        protected override void CustomValidateUpdate(Employee entity)
        {
            var isExist = _employeeRepository.CheckEmployeeCodeExitsUpdate(entity.EmployeeCode, entity.EmployeeId);
            if (isExist.Single())
            {
                throw new BadRequestException(Properties.Resources.duplidateEmployeeCode);
            }
        }
        #endregion

        #region validate Paging
        /// <summary>
        /// Hàm validate Paging
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<Employee> GetPaging(int pageIndex, int pageSize, string filter)
        {
            if (pageIndex <= 0 || pageSize <= 0) throw new BadRequestException(Properties.Resources.pagingErr);
            var employees = _employeeRepository.GetPaging(pageIndex, pageSize, filter);
            return employees;
        }
        #endregion

        #region Hàm đếm số lượng bản ghi
        /// <summary>
        /// Hàm đếm số lượng bản ghi
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<int> GetEmployeeCount(string filter)
        {
            var count = _employeeRepository.GetEmployeeCount(filter);
            return count;
        }
        #endregion

        #region Hàm lấy mã lớn nhất
        /// <summary>
        /// Hàm lấy mã lớn nhất
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetMaxCode()
        {
            var count = _employeeRepository.GetMaxCode();
            return count;
        }
        #endregion

    }
}
