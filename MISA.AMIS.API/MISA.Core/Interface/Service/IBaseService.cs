using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interface.Service
{
    /// <summary>
    /// Service dùng chung
    /// </summary>
    /// <typeparam name="MISAEntity"></typeparam>
    public interface IBaseService<MISAEntity>
    {
        public IEnumerable<MISAEntity> GetAll();
        public MISAEntity GetById(Guid entityId);
        public int Insert(MISAEntity entity);
        public int Update(MISAEntity entity);
        public int Delete(Guid entityId);
    }
}
