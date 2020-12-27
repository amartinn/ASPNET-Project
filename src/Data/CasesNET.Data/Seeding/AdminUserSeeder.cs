namespace CasesNET.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static CasesNET.Common.GlobalConstants.Domain;

    public class AdminUserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await this.SeedAdminUserAsync(userManager);
        }

        private async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                UserName = AdminUserEmail,
                Email = AdminUserEmail,
            };
            var createdUser = await userManager.CreateAsync(user, AdminUserPassword);

            if (createdUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, AdministratorRoleName);
            }
        }
    }
}
