using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interface.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.MISA.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Hàm check tồn tại mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>true: mã đã tồn tại</returns>
        /// <returns>false: mã chưa tồn tại</returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<bool>  CheckEmployeeCodeExits(string employeeCode)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeCode", employeeCode);
                var isExist = dbConnection.Query<bool>("Proc_CheckCodeExist", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return isExist;
            }
        }

        /// <summary>
        /// Hàm phân trang có filter
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns>employees: danh sách nhân viên</returns
        /// <returns>count: tổng số bản ghi lọc theo filter</returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public Object GetPaging(int pageIndex, int pageSize, string filter)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@m_PageIndex", pageIndex);
                dynamicParameters.Add("@m_PageSize", pageSize);
                dynamicParameters.Add("@filter", filter);
                var employees = dbConnection.Query<Object>("Proc_EmployeeFilter", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@filter", filter);
                var count = dbConnection.Query<int>("Proc_EmployeeFilterCount", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var response = new
                {
                    employees = employees,
                    countEmployees = count
                };
                return response;
            }
        }

        /// <summary>
        /// Hàm lấy mã lớn nhất
        /// </summary>
        /// <returns>employeeCode: mã nhân viên lớn nhất cộng thêm 1</returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public String GetMaxCode()
        {

            using (dbConnection = new MySqlConnection(connectionString))
            {
                var count = dbConnection.Query<String>("Proc_EmployeeGetMaxCode", commandType: CommandType.StoredProcedure);
                var employeeCode = string.Join(",", count.ToArray());
                var temp = employeeCode.Split("-");
                employeeCode =
                  temp[0] + "-" + (long.Parse(temp[1]) + 1).ToString();
                return employeeCode;
            }
        }
        /// <summary>
        /// Hàm lấy bản ghi có mã lớn nhất
        /// </summary>
        /// <returns>employee: bản ghi có dữ liệu thep id và có mã lớn nhất cộng thêm 1</returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public Employee GetEmployeeMaxCodeById(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@employeeId", entityId);
                var employee = dbConnection.QueryFirstOrDefault<Employee>("Proc_GetEmployeeById", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var count = dbConnection.Query<String>("Proc_EmployeeGetMaxCode", commandType: CommandType.StoredProcedure);
                employee.EmployeeCode = string.Join(",", count.ToArray());
                var temp = employee.EmployeeCode.Split("-");
                employee.EmployeeCode =
                  temp[0] + "-" + (long.Parse(temp[1]) + 1).ToString();
                return employee;
            }
        }

        /// <summary>
        /// Hàm check tồn tại mã nhân viên khi sửa
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>isExist: mã nhân viên đã tồn tại trên hệ thống</returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<bool> CheckEmployeeCodeExitsUpdate(string employeeCode, Guid employeeId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeCode", employeeCode);
                dynamicParameters.Add("@EmployeeId", employeeId);
                var isExist = dbConnection.Query<bool>("Proc_CheckCodeExistUpdate", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return isExist;
            }
        }
    }
}
