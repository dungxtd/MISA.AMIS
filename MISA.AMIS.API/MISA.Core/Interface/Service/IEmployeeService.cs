using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interface.Service
{
    /// <summary>
    /// Service phục vụ cho nhân viên
    /// </summary>
    /// CreatedBy: TDDUNG (27/4/2021)
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Service lấy trang theo bộ lọc
        /// </summary>
        /// <param name="pageIndex">số thứ tự trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="filter">bộ lọc</param>
        /// <returns>mảng nhân viên và tổng số bản ghi</returns>
        Object GetPaging(int pageIndex, int pageSize, string filter);

        /// <summary>
        /// Service mã lớn nhất
        /// </summary>
        /// <returns>Mã lớn nhất trong hệ thống</returns>
        String GetMaxCode();

        /// <summary>
        /// Service lấy bản ghi theo id nhưng có mã lớn nhất
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>bản ghi đã chọn</returns>
        Employee GetEmployeeMaxCodeById(Guid entityId);

        /// <summary>
        /// Service xuất file excel
        /// </summary>
        /// <returns>file link</returns>
        FileExport Export();
    }
}
