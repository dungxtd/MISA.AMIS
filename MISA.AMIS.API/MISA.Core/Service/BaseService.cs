using MISA.Core.AttributeCustom;
using MISA.Core.Exceptions;
using MISA.Core.Interface.Repository;
using MISA.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : class
    {
        #region Khai báo biến toàn cục và ctor
        IBaseRepository<MISAEntity> _baseRepository;
        /// <summary>
        /// Cóntructer
        /// </summary>
        /// <param name="baseRepository"></param>
        /// Created: TDDung
        /// Date: 10/5/2021
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region service lấy tất cả bản ghi
        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public IEnumerable<MISAEntity> GetAll()
        {
            return _baseRepository.GetAll();
        }
        #endregion

        #region Service lấy bản ghi theo id
        /// <summary>
        /// Hàm lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public MISAEntity GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }
        #endregion

        #region Service thêm bản ghi
        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        public int Insert(MISAEntity entity)
        {
            Validate(entity);
            return _baseRepository.Insert((entity));
        }
        #endregion

        #region Hàm validate chung
        /// <summary>
        /// Hàm validate đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// Created: TDDung
        /// Date: 10/5/2021
        private void Validate(MISAEntity entity)
        {
            //Validate dữ liệu
            //Lấy ra tất cả các property của class
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var property in properties)
            {
                var requireProperies = property.GetCustomAttributes(typeof(MISARequired), true);
                var maxLengthProperies = property.GetCustomAttributes(typeof(MISAMaxLength), true);
                if (requireProperies.Length > 0)
                {
                    //Lấy giá trị
                    var propertyValue = property.GetValue(entity);
                    //Kiểm tra giá trị
                    //if (string.IsNullOrEmpty(propertyValue.ToString()))
                    //{
                    //    var msgError = (requireProperies[0] as MISARequired).MsgError;
                    //    if (string.IsNullOrEmpty(msgError))
                    //    {
                    //        throw new BadRequestException(property.Name + Properties.Resources.emptyErr);
                    //    }
                    //    throw new BadRequestException(msgError);
                    //}
                }
                if (maxLengthProperies.Length > 0)
                {
                    //Lấy giá trị
                    var propertyValue = property.GetValue(entity);
                    var maxLength = (maxLengthProperies[0] as MISAMaxLength).MaxLength;
                    //Kiểm  tra giá trị
                    if (propertyValue.ToString().Length > maxLength)
                    {
                        throw new BadRequestException(Properties.Resources.employeeLengthErr);
                    }
                }
            }
            CustomValidate(entity);
        }
        #endregion
        /// <summary>
        /// Hàm custome vatlidate
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void CustomValidate(MISAEntity entity)
        {

        }

        #region Hàm sửa
        /// <summary>
        /// Service sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(MISAEntity entity)
        {
            ValidateUpdate(entity);
            return _baseRepository.Update(entity);
        }
        #endregion

        #region Hàm validate sửa
        private void ValidateUpdate(MISAEntity entity)
        {
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var property in properties)
            {
                var requireProperies = property.GetCustomAttributes(typeof(MISARequired), true);
                var maxLengthProperies = property.GetCustomAttributes(typeof(MISAMaxLength), true);
                if (requireProperies.Length > 0)
                {
                    //Lấy giá trị
                    var propertyValue = property.GetValue(entity);
                    //Kiểm tra giá trị
                    if (string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        var msgError = (requireProperies[0] as MISARequired).MsgError;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            throw new BadRequestException(property.Name + Properties.Resources.emptyErr);
                        }
                        throw new BadRequestException(msgError);
                    }
                }
                if (maxLengthProperies.Length > 0)
                {
                    //Lấy giá trị
                    var propertyValue = property.GetValue(entity);
                    var maxLength = (maxLengthProperies[0] as MISAMaxLength).MaxLength;
                    //Kiểm  tra giá trị
                    if (propertyValue.ToString().Length > maxLength)
                    {
                        throw new BadRequestException(Properties.Resources.employeeLengthErr);
                    }
                }
            }

            CustomValidateUpdate(entity);
        }
        #endregion

        #region Hàm validate sửa để cho employee service ghi đè
        /// <summary>
        /// Hàm validate sửa để cho employee service ghi đè
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void CustomValidateUpdate(MISAEntity entity)
        {
        }
        #endregion

        #region Hàm xoá
        /// <summary>
        /// Service xoá
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int Delete(Guid entityId)
        {
            return _baseRepository.Delete(entityId);
        }
        #endregion
    }
}
