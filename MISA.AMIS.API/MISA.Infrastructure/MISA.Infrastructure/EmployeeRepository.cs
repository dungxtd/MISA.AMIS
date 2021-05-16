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
        #region Hàm check tồn tại mã nhân viên
        /// <summary>
        /// Hàm check tồn tại mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
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
        #endregion

        #region Hàm phân trang có filter
        /// <summary>
        /// Hàm phân trang có filter
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<Employee> GetPaging(int pageIndex, int pageSize, string filter)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@m_PageIndex", pageIndex);
                dynamicParameters.Add("@m_PageSize", pageSize);
                dynamicParameters.Add("@filter", filter);
                var employees = dbConnection.Query<Employee>("Proc_EmployeeFilter", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return employees;
            }
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

            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@filter", filter);
                var count = dbConnection.Query<int>("Proc_EmployeeFilterCount", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return count;
            }
        }
        #endregion

        #region Hàm lấy bản ghi lớn nhất
        /// <summary>
        /// Hàm lấy bản ghi lớn nhất
        /// </summary>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<String> GetMaxCode()
        {

            using (dbConnection = new MySqlConnection(connectionString))
            {
                var count = dbConnection.Query<String>("Proc_EmployeeGetMaxCode", commandType: CommandType.StoredProcedure);
                return count;
            }
        }
        #endregion

        #region Hàm check tồn tại mã nhân viên khi sửa
        /// <summary>
        /// Hàm check tồn tại mã nhân viên khi sửa
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
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
        #endregion
    }
}
