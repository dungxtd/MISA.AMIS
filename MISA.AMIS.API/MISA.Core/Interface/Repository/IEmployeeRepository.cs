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
        IEnumerable<Employee> GetPaging(int pageIndex, int pageSize,string filter);
        public IEnumerable<int> GetEmployeeCount(string filter);
        public IEnumerable<String> GetMaxCode();
        public IEnumerable<bool> CheckEmployeeCodeExitsUpdate(string employeeCode, Guid employeeId);
    }
}
