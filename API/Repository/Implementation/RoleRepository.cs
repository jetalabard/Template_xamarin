using API.Repository.Generic;
using API.Repository.Interfaces;
using Core;
using Entities.Context;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repository.Implementation
{
    public class RoleRepository : GenericRepository<TemplateContext, Role>, IRoleRepository
    {
        public RoleRepository(TemplateContext context) : base(context)
        {
        }

        public void Initialize()
        {
            if (!Context.Roles.Any())
            {
                Context.Roles.Add(new Role(RoleEnum.Admin));
                Context.Roles.Add(new Role(RoleEnum.Default));
                Context.Roles.Add(new Role(RoleEnum.None));

                Context.SaveChanges();
            }
        }


        public async Task<Role> Get(string id)
        {
            return await Context.Roles.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
