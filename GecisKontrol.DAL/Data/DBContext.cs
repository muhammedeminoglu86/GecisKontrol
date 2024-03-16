using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GecisKontrol.Domain.Interfaces;
using Microsoft.Extensions.Configuration;


namespace GecisKontrol.DAL.Data
{
	public class DbContext : IDbContext
	{
		private readonly string _connectionString;

		public DbContext(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public IDbConnection CreateConnection()
		{
			return new NpgsqlConnection(_connectionString);
		}
	}
}
