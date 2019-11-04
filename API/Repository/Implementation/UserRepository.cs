using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Repository.Generic;
using API.Repository.Interfaces;
using Core;
using Entities.Context;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Repository.Implementation
{
    public class UserRepository : GenericRepository<TemplateContext, User>, IUserRepository
    {
        public UserRepository(TemplateContext context)
            : base(context)
        {
        }

        public async void Initialize()
        {
            if (!Context.Users.Any())
            {
                var user = new User { Id = "admin", LastName = "admin", FirstName = "admin" };
                user.Role = new Role(RoleEnum.Admin);
                await Create(user, "Admin123!");
            }
        }

        public async Task<User> Get(string id)
        {
            return await Context.Users.
                Include(u => u.Role)
                .SingleOrDefaultAsync(user => user.Id == id);
        }

        public async new Task<IEnumerable<User>> GetAll()
        {
            return await Context.Users.Include(u => u.Role).ToListAsync();
        }

        public Task<User> Authenticate(string username, string password, string secret)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = Context.Users.Include(u => u.Role)
                .SingleOrDefault(x => x.Id == username);

            // check if user exists
            if (user == null)
            {
                return null;
            }

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Role, user.RoleId),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // authentication successful
            return Task.FromResult(user);
        }

        public async Task<bool> CheckPersonalIdExist(string personalId)
        {
            return await Context.Users.AnyAsync(x => x.PersonalId == personalId);
        }

        public async Task<User> Create(User user, string password)
        {
            Task<User> Action()
            {
                CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                user.Role = AffectDefaultRole(user);

                Context.Users.Add(user);
                Context.SaveChanges();

                return Task.FromResult(user);
            }

            return await ExecuteInTransaction(Action);
        }

        public Role AffectDefaultRole(User user)
        {
            if (user.Role == null)
            {
                user.Role = Context.Roles.FirstOrDefault(r => r.Id == RoleEnum.Default.ToString());
            }

            return user.Role;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedHash));
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<IEnumerable<User>> GetFilteredUsersByMatriculeOrName(string filter)
        {
            return await Context.Users.Include(p => p.Role).Where(c => c.Id.Contains(filter, StringComparison.OrdinalIgnoreCase)
                || c.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase)
                || c.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<bool> UpdatePassword(string userId, string password)
        {
            try
            {
                User user = await Get(userId);
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                return (await Edit(user).ConfigureAwait(false)) != null ? true : false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> UpdateEmail(string userId, string email)
        {
            try
            {
                User user = await Get(userId);

                user.Email = email;

                return await Edit(user);
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> UpdateRole(string userId, string roleId)
        {
            try
            {
                User user = await Get(userId);

                user.RoleId = roleId;

                return await Edit(user);
            }
            catch
            {
                throw;
            }
        }
    }
}
