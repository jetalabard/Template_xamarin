using API.Repository.Generic;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        void Initialize();
        Task<Role> Get(string id);
    }
}
