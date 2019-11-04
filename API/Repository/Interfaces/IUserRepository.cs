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

        Task<bool> UpdatePassword(string userId, string password);

        Task<User> UpdateEmail(string userId, string email);

        Task<User> UpdateRole(string userId, string roleId);

        Task<bool> CheckPersonalIdExist(string personalId);
    }
}
