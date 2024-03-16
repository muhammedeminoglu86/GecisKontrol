using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class CompanyService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CompanyService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Company>> GetAllCompanysAsync()
		{
			return await _unitOfWork._companyRepository.GetAllCompaniesAsync();
		}

		public async Task<Company> GetCompanyByIdAsync(int id)
		{
			return await _unitOfWork._companyRepository.GetCompanyByIdAsync(id);
		}

		public async Task<int> AddCompanyAsync(Company Company)
		{
			try
			{
				var CompanyId = await _unitOfWork._companyRepository.AddCompanyAsync(Company);
				await _unitOfWork.CommitAsync();
				return CompanyId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateCompanyAsync(Company Company)
		{
			try
			{
				await _unitOfWork._companyRepository.UpdateCompanyAsync(Company);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteCompanyAsync(int id)
		{
			try
			{
				await _unitOfWork._companyRepository.DeleteCompanyAsync(id);
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
