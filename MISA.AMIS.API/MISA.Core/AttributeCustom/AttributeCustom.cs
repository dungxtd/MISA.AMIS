using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.AttributeCustom
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public string MsgError = string.Empty;
        public MISARequired(string msgError = "")
        {
            MsgError = msgError;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAMaxLength : Attribute
    {
        public int MaxLength = 0;
        public MISAMaxLength(int maxLength = 0)
        {
            MaxLength = maxLength;

        }
    }
}
