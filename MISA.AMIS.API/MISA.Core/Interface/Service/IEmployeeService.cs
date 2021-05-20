﻿using MISA.Core.Entities;
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
        Object GetPaging(int pageIndex, int pageSize, string filter);
        String GetMaxCode();
        Employee GetEmployeeMaxCodeById(Guid entityId);
        FileExport Export();
    }
}
