using System;
using System.Linq;
using API.Repository.Implementation;
using Entities.Context;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Models.Mock
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (TemplateContext context = new TemplateContext(serviceProvider.GetRequiredService<DbContextOptions<TemplateContext>>()))
            {
                // Look for any board games.
                if (context.Users.Any())
                {
                    // Data was already seeded
                    return;
                }

                InitMockData(context);
            }
        }

        private static async void InitMockData(TemplateContext context)
        {
            MockData.Roles.ForEach(x => context.Roles.Add(x));
            MockData.Users.ForEach(x => context.Users.Add(x));

            context.SaveChanges();

            string password = "Admin123!";
            foreach (User user in context.Users)
            {
                using (var repo = new UserRepository(context))
                {
                    _ = await repo.UpdatePassword(user.Id, password);
                }
            }
        }
    }
}
