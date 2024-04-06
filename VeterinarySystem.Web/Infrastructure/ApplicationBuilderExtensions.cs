using Microsoft.AspNetCore.Identity;
using VeterinarySystem.Data.DataSeeding.Admin;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services
                .GetRequiredService<UserManager<StaffMember>>();

            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminUser.AdminRoleName))
                {
                    return;
                }
                var role = new IdentityRole { Name = AdminUser.AdminRoleName };

                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByEmailAsync(AdminUser.AdminEmail);

                await userManager.AddToRoleAsync(admin, role.Name);
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
