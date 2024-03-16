using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class ErrorLogService : IErrorLogService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ErrorLogService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task AddErrorLogAsync(ErrorLog errorLog)
		{
			await _unitOfWork._errorLogRepository.AddErrorLogAsync(errorLog);
			await _unitOfWork.CommitAsync();
		}

        public async Task<ErrorLog> GetErrorLogByIdAsync(int id)
        {
            return await _unitOfWork._errorLogRepository.GetErrorLogByIdAsync(id);
        }

        public  async Task<IEnumerable<ErrorLog>> GetAllErrorLogs()
        {
            return await _unitOfWork._errorLogRepository.GetAllErrorLogsAsync();
        }
    }
}
