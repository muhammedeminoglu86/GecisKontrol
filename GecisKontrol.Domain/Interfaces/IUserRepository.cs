using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAllUsersAsync(string query, object param);
		Task<User> GetUserByIdAsync(string query, object param);
		Task<int> AddUserAsync(User user);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int id);
	}
}
