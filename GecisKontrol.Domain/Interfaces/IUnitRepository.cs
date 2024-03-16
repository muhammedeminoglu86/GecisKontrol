using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IUnitRepository
	{
		Task<IEnumerable<Unit>> GetAllUnitsAsync();
		Task<Unit> GetUnitByIdAsync(long id);
		Task<long> AddUnitAsync(Unit unit);
		Task UpdateUnitAsync(Unit unit);
		Task DeleteUnitAsync(long id);
	}
}
