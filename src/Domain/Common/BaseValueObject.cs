using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Common
{
    public abstract class BaseValueObject<T> where T : BaseValueObject<T>
    {
        public override bool Equals(object obj)
        {
            T val = obj as T;
            if (val == null)
            {
                return false;
            }

            return IsEqual(val);
        }

        protected abstract bool IsEqual(T other);

        
        public static bool operator ==(BaseValueObject<T> a, BaseValueObject<T> b)
        {
            if ((object)a == null && (object)b == null)
            {
                return true;
            }

            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(BaseValueObject<T> a, BaseValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
