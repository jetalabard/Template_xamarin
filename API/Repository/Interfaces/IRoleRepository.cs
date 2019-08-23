using System.Threading.Tasks;
using API.Repository.Generic;
using Entities.Model;

namespace API.Repository.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        void Initialize();

        Task<Role> Get(string id);
    }
}
