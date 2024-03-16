using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class Employee
	{
		public int Id { get; set; }

		[StringLength(255)]
		public string Name { get; set; }

		[StringLength(255)]
		public string NationalId { get; set; }

		[StringLength(255)]
		public string CompanyGeneratedId { get; set; }

		public byte[] Photo { get; set; }

		public int IsActive { get; set; }

		public int CreatedUserBy { get; set; }

		public int CompanyId { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
