using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Department
    {
        #region Properties phòng ban
        ///Thông tin phòng ban
        /// CreatedBy: TDDUNG (08/05/2021)
        ///<summary>
        ///Khoá chính
        /// </summary>
        public Guid DepartmentId { get; set; }

        ///<summary>
        ///Tên phòng ban
        /// </summary>
        //[MISARequired]
        public string DepartmentName { get; set; }

        ///<summary>
        ///Mô tả
        /// </summary>
        //[MISARequired]
        public string Description { get; set; }
        #endregion
    }
}
