using System.Collections.Generic;
using System.Threading.Tasks;
using API.Repository.Generic;
using Entities.Model;

namespace API.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        void Initialize();

        Task<User> Get(string id);

        Task<IEnumerable<User>> GetFilteredUsersByMatriculeOrName(string filter);

        Task<User> Create(User user, string password);

        Task<User> Authenticate(string username, string password, string secret);

        Task<User> UpdatePassword(User user, string password);

        Task<User> UpdateEmail(User user, string email);

        Task<User> UpdateRole(User user, string roleId);
    }
}
