using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interface.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Check mã nhân viên tồn tại
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true: đã tồn tại</returns>
        /// <returns>false: chưa tồn tại</returns>
        public IEnumerable<bool> CheckEmployeeCodeExits(string employeeCode);

        /// <summary>
        /// Lấy page theo filter
        /// </summary>
        /// <param name="pageIndex">số thứ tự trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="filter">bộ lọc</param>
        /// <returns>mảng nhân viên và tổng số bản ghi</returns>
        Object GetPaging(int pageIndex, int pageSize,string filter);

        /// <summary>
        /// Lấy mã lớn nhất
        /// </summary>
        /// <returns>Mã lớn nhất trong hệ thống</returns>
        public String GetMaxCode();

        /// <summary>
        /// Check code trùng khi update
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <param name="employeeId">Id nhân viên</param>
        /// <returns>true: trùng</returns>
        /// <returns>false: không trùng</returns>
        public IEnumerable<bool> CheckEmployeeCodeExitsUpdate(string employeeCode, Guid employeeId);

        /// <summary>
        /// Clone bản ghi và thêm mã lớn nhất
        /// </summary>
        /// <param name="entityId">Id nhân viên</param>
        /// <returns>bản ghi đã chọn</returns>
        public Employee GetEmployeeMaxCodeById(Guid entityId);
    }
}
