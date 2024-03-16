using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class Card
	{
		public int Id { get; set; }
		public string CardHex { get; set; }
		public string CardDec { get; set; }
		public int CardTypeId { get; set; }
		public int IsActive { get; set; }
		public int IsExpirable { get; set; }
		public DateTime? ExpireDate { get; set; }
		public int CreatedById { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
