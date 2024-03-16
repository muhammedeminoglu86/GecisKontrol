using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class Unit
	{
		public long Id { get; set; }

		[Required]
		[StringLength(255)]
		public string UnitName { get; set; }

		[StringLength(255)]
		public string UnitDesc { get; set; }

		public int DepartmentId { get; set; }

		public int IsActive { get; set; }

		public DateTime CreationDate { get; set; }

		public int CreatedUserBy { get; set; }
	}
}
