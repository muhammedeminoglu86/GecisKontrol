using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IErrorLogRepository
	{
		Task AddErrorLogAsync(ErrorLog errorLog);
        Task<ErrorLog> GetErrorLogByIdAsync(int id);
        Task<IEnumerable<ErrorLog>> GetAllErrorLogsAsync();

    }
}
