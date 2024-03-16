using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GecisKontrol.Domain.Model;

namespace GecisKontrol.Domain.Interfaces
{
	public interface ICardRepository
	{
		Task<IEnumerable<Card>> GetAllCardsAsync();
		Task<Card> GetCardByIdAsync(int id);
		Task<int> AddCardAsync(Card card);
		Task UpdateCardAsync(Card card);
		Task DeleteCardAsync(int id);
	}
}
