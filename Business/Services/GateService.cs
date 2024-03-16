using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class GateService
	{
		private readonly IUnitOfWork _unitOfWork;

		public GateService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Gate>> GetAllGatesAsync()
		{
			return await _unitOfWork._gateRepository.GetAllGatesAsync();
		}

		public async Task<Gate> GetGateByIdAsync(int id)
		{
			return await _unitOfWork._gateRepository.GetGateByIdAsync(id);
		}

		public async Task<int> AddGateAsync(Gate Gate)
		{
			try
			{
				var GateId = await _unitOfWork._gateRepository.AddGateAsync(Gate);
				await _unitOfWork.CommitAsync();
				return GateId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateGateAsync(Gate Gate)
		{
			try
			{
				await _unitOfWork._gateRepository.UpdateGateAsync(Gate);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteGateAsync(int id)
		{
			try
			{
				await _unitOfWork._gateRepository.DeleteGateAsync(id);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}
	}
}
