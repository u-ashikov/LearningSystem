namespace LearningSystem.Web.Infrastructure.Extensions
{
    using Common.Constants;
    using Data;
    using LearningSystem.Data.Enums;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
		public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				scope.ServiceProvider.GetService<LearningSystemDbContext>().Database.Migrate();
			}

			return app;
		}

		public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var adminRole = Role.Administrator.ToString();

				var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
				var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

				Task.Run(async () =>
				{
					bool roleExists = await roleManager.RoleExistsAsync(adminRole);

					if (!roleExists)
					{
						await roleManager.CreateAsync(new IdentityRole()
						{
							Name = adminRole
						});
					}

					var admin = await userManager.FindByNameAsync(AdminConstants.Username);

					if (admin == null)
					{
						admin = new User()
						{
							UserName = AdminConstants.Username,
							Email = AdminConstants.Email,
							Name = AdminConstants.Name,
							BirthDate = AdminConstants.BirthDate
						};

						await userManager.CreateAsync(admin, AdminConstants.Password);

						await userManager.AddToRoleAsync(admin, adminRole);
					}
					else
					{
						bool isInRole = await userManager.IsInRoleAsync(admin, adminRole);

						if (!isInRole)
						{
							await userManager.AddToRoleAsync(admin, adminRole);
						}
					}
				})
				.GetAwaiter()
				.GetResult();
			}

			return app;
		}

		public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

				var roles = Enum.GetNames(typeof(Role));

				Task.Run(async () =>
				{
					foreach (var role in roles)
					{
						bool roleExists = await roleManager.RoleExistsAsync(role);

						if (!roleExists)
						{
							await roleManager.CreateAsync(new IdentityRole()
							{
								Name = role
							});
						}
					}
				})
				.Wait();
			}

			return app;
		}
    }
}
