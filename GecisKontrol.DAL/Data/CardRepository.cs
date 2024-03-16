using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System.Transactions;
using System.Data.Common;

namespace GecisKontrol.DAL.Data
{
	public class CardRepository : ICardRepository
	{
		private readonly DbContext _dbContext;

		public CardRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Card>> GetAllCardsAsync()
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QueryAsync<Card>("SELECT * FROM \"Card\"");
			}
		}

		public async Task<Card> GetCardByIdAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();
			{
				return await connection.QuerySingleOrDefaultAsync<Card>("SELECT * FROM \"Card\" WHERE Id = @Id", new { Id = id });
			}
		}

		public async Task<int> AddCardAsync(Card card)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "INSERT INTO \"Card\" (CardHex, CardDec, CardTypeId, IsActive, IsExpirable, ExpireDate, CreatedById, CreationDate) VALUES (@CardHex, @CardDec, @CardTypeId, @IsActive, @IsExpirable, @ExpireDate, @CreatedById, @CreationDate) RETURNING Id";
			var insertedId = await connection.ExecuteScalarAsync<int>(query, card);
			return insertedId;
		}

		public async Task UpdateCardAsync(Card card)
		{
			using var connection = _dbContext.CreateConnection();
			var query = "UPDATE \"Card\" SET CardHex = @CardHex, CardDec = @CardDec, CardTypeId = @CardTypeId, IsActive = @IsActive, IsExpirable = @IsExpirable, ExpireDate = @ExpireDate, CreatedById = @CreatedById, CreationDate = @CreationDate WHERE Id = @Id";
			await connection.ExecuteAsync(query, card);
		}

		public async Task DeleteCardAsync(int id)
		{
			using var connection = _dbContext.CreateConnection();

			var query = "DELETE FROM \"Card\" WHERE Id = @Id";
			await connection.ExecuteAsync(query, new { Id = id });
		}
	}
}
