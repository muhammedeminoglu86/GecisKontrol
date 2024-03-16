using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class Department
	{
		public int Id { get; set; }

		[StringLength(255)]
		public string DepartmentName { get; set; }

		[StringLength(255)]
		public string DepartmentDescription { get; set; }

		public int CompanyId { get; set; }

		public int IsActive { get; set; }

		public DateTime CreationDate { get; set; }

		public int CreatedUserBy { get; set; }
	}
}
