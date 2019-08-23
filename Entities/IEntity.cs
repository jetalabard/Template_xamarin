using System.Collections.Generic;

namespace Entities
{
    public interface IEntity<T, X> : IEqualityComparer<X>
    {
        T Id { get; set; }
    }
}
