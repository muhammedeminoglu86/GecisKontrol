using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IGateRepository
	{
		Task<IEnumerable<Gate>> GetAllGatesAsync();
		Task<Gate> GetGateByIdAsync(int id);
		Task<int> AddGateAsync(Gate gate);
		Task UpdateGateAsync(Gate gate);
		Task DeleteGateAsync(int id);
	}
}
