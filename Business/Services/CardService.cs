using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class CardService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CardService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Card>> GetAllCardsAsync()
		{
			return await _unitOfWork._cardRepository.GetAllCardsAsync();
		}

		public async Task<Card> GetCardByIdAsync(int id)
		{
			return await _unitOfWork._cardRepository.GetCardByIdAsync(id);
		}

		public async Task<int> AddCardAsync(Card Card)
		{
			try
			{
				var CardId = await _unitOfWork._cardRepository.AddCardAsync(Card);
				await _unitOfWork.CommitAsync();
				return CardId;
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task UpdateCardAsync(Card Card)
		{
			try
			{
				await _unitOfWork._cardRepository.UpdateCardAsync(Card);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		public async Task DeleteCardAsync(int id)
		{
			try
			{
				await _unitOfWork._cardRepository.DeleteCardAsync(id);
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
