using Dapper;
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
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity : class
    {
        #region Khai báo biến toàn cục 
        protected String tableName = typeof(MISAEntity).Name;
        protected String connectionString = "" +
        "Host = 47.241.69.179;" +
        "Port = 3306;" +
        "Database = MF826_AMIS_TDDUNG;" +
        "User Id = dev;" +
        "Password = 12345678;";
        protected IDbConnection dbConnection;
        #endregion

        #region Hàm lấy ra tất cả bản ghi
        /// <summary>
        /// Hàm lấy ra tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<MISAEntity> GetAll()
        {
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    var entities = dbConnection.Query<MISAEntity>($"Proc_Get{tableName}s", commandType: CommandType.StoredProcedure);
                    return entities;
                }

            }
        }
        #endregion

        #region Hàm lấy bản ghi theo id
        /// <summary>
        /// Hàm lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public MISAEntity GetById(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var entitity = dbConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{tableName}ById", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return entitity;
            }
        }
        #endregion

        #region Hàm thêm bản ghi
        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public int Insert(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}", entity);
                var rowsAffect = dbConnection.Execute($"Proc_Insert{tableName}", param: entity, commandType: CommandType.StoredProcedure);
                return rowsAffect;
            }
        }
        #endregion

        #region Hàm sửa bản ghi
        /// <summary>
        /// Hàm sửa bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public int Update(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}", entity);
                var rowsAffect = dbConnection.Execute($"Proc_Update{tableName}", param: entity, commandType: CommandType.StoredProcedure);
                return rowsAffect;
            }
        }
        #endregion

        #region Hàm xoá bản ghi
        /// <summary>
        /// Hàm xoá bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public int Delete(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId.ToString());
                var rowsEffect = dbConnection.Execute($"Proc_Delete{tableName}", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowsEffect;
            }
        }
        #endregion

    }
}
