using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class UnitService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UnitService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Unit>> GetAllUnitsAsync()
		{
			return await _unitOfWork._unitRepository.GetAllUnitsAsync();
		}

		public async Task<Unit> GetUnitByIdAsync(int id)
		{
			return await _unitOfWork._unitRepository.GetUnitByIdAsync(id);
		}

		public async Task<long> AddUnitAsync(Unit Unit)
		{
			try
			{
				var UnitId = await _unitOfWork._unitRepository.AddUnitAsync(Unit);
				await _unitOfWork.CommitAsync();
				return UnitId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateUnitAsync(Unit Unit)
		{
			try
			{
				await _unitOfWork._unitRepository.UpdateUnitAsync(Unit);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteUnitAsync(int id)
		{
			try
			{
				await _unitOfWork._unitRepository.DeleteUnitAsync(id);
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
