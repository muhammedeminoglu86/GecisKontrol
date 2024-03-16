using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class EmployeeCardMapping
	{
		public int Id { get; set; }

		public int CardId { get; set; }

		public int EmployeeId { get; set; }

		public short IsActive { get; set; }

		public int CreatedUserById { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
