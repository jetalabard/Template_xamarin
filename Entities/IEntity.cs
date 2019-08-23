using System.Collections.Generic;

namespace Entities
{
    public interface IEntity<T, TX> : IEqualityComparer<TX>
    {
        T Id { get; set; }
    }
}
