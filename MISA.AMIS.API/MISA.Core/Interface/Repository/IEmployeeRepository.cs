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
        public IEnumerable<bool> CheckEmployeeCodeExits(string employeeCode);
        Object GetPaging(int pageIndex, int pageSize,string filter);
        public IEnumerable<int> GetEmployeeCount(string filter);
        public String GetMaxCode();
        public IEnumerable<bool> CheckEmployeeCodeExitsUpdate(string employeeCode, Guid employeeId);
        public Employee GetEmployeeMaxCodeById(Guid entityId);
    }
}
