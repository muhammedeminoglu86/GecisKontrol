using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class Company
	{
		public int Id { get; set; }

		[StringLength(255)]
		public string CompanyName { get; set; }

		[StringLength(255)]
		public string Description { get; set; }

		public byte[] Logo { get; set; }

		public short IsActive { get; set; }

		public DateTime CreationDate { get; set; }

		public short CreatedUserBy { get; set; }
	}
}
