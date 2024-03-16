using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class DeviceRole
	{
		public int Id { get; set; }

		[StringLength(255)]
		public string RoleName { get; set; }

		[StringLength(255)]
		public string RoleDescription { get; set; }

		public int IsActive { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
