using System.Collections.Generic;

namespace Core.Dto
{
    public class DtoComparer<T> : IEqualityComparer<T> where T : Dto
    {
        public bool Equals(T x, T y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
