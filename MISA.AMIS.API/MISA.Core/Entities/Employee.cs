using MISA.Core.AttributeCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Employee
    {
        #region Properties nhân viên
        ///Thông tin nhân viên
        /// CreatedBy: TDDUNG (08/05/2021)
        ///<summary>
        ///Khoá chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        ///<summary>
        ///Mã nhân viên
        /// </summary>
        [MISARequired]
        [MISAMaxLength(maxLength:20)]
        public string EmployeeCode { get; set; }

        ///<summary>
        ///Tên nhân viên
        /// </summary>
        //[MISARequired]
        public string EmployeeName { get; set; }

        ///<summary>
        //Giới tính
        /// </summary>
        public int? Gender { get; set; }

        ///<summary>
        ///Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        ///<summary>
        ///Số CMND
        /// </summary>
        public string IdentityNumber { get; set; }

        ///<summary>
        ///Ngày cấp CMND
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        ///<summary>
        ///Nơi cấp CMND
        /// </summary>
        public string IdentityPlace { get; set; }

        ///<summary>
        ///Chức vụ
        /// </summary>
        public string EmployeePosition { get; set; }

        ///<summary>
        ///Địa chỉ
        /// </summary>
        public string Address { get; set; }

        ///<summary>
        ///Mã phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        ///<summary>
        ///Tên phòng ban
        /// </summary>
        public String DepartmentName { get; set; }

        ///<summary>
        ///Số tài khoản
        /// </summary>
        public string BankAccountNumber { get; set; }

        ///<summary>
        ///Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        ///<summary>
        ///Chi nhánh tài khoản ngân hàng
        /// </summary>
        public string BankBranchName { get; set; }

        ///<summary>
        ///Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        ///<summary>
        ///Số điện thoại bàn
        /// </summary>
        public string TelephoneNumber { get; set; }

        ///<summary>
        ///Email
        /// </summary>
        public string Email { get; set; }

        ///<summary>
        ///Tình trạng
        /// </summary>
        public string AccountState { get; set; }
        #endregion
    }
}
