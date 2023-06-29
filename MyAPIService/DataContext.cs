using Microsoft.EntityFrameworkCore;
using MyAPIService.Models;

namespace MyAPIService
{
	public class DataContext : DbContext
	{

		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<User> Users => Set<User>();
		public DbSet<Owner> Owners => Set<Owner>();
		public DbSet<Client> Clients => Set<Client>();
		public DbSet<Property> Properties => Set<Property>();
	}
}
