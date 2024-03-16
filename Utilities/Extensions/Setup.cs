using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GecisKontrol.Domain.Model.IdentityModel;

namespace Utilities
{
	public static class Setup
	{
		public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>(); // Bu satırı değiştirin

			string username = configuration["AdminUser:Name"];
			string email = configuration["AdminUser:Email"];
			string password = configuration["AdminUser:Password"];
			string role = configuration["AdminUser:Role"];

			if (await userManager.FindByNameAsync(username) == null)
			{
				if (await roleManager.FindByNameAsync(role) == null)
				{
					await roleManager.CreateAsync(new ApplicationRole(role));
				}

				ApplicationUser user = new ApplicationUser
				{
					UserName = username,
					Email = email
				};

				IdentityResult result = await userManager.CreateAsync(user, password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, role);
				}
			}
		}
	}
}