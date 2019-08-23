using API.Repository.Generic;
using Entities.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        void Initialize();
        Task<User> Get(string id);
        Task<List<User>> GetFilteredUsersByMatriculeOrName(string filter);

        Task<User> Create(User user, string password);
        Task<User> Authenticate(string username, string password, string secret);
    }
}
