using GecisKontrol.Domain.Model.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GecisKontrol.DAL.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		// Identity ile ilgili DbSets buraya eklenebilir. Örneğin:
		// public DbSet<YourCustomModel> YourCustomModels { get; set; }
	}
}