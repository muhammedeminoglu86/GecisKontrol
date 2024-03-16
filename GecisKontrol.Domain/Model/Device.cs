using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class Device
	{
		public int Id { get; set; }

		[StringLength(255)]
		public string Name { get; set; }

		[StringLength(255)]
		public string Description { get; set; }

		[StringLength(255)]
		public string HostName { get; set; }

		[StringLength(255)]
		public string MacAddress { get; set; }

		[StringLength(255)]
		public string ProtocolType { get; set; }

		public int DeviceRoleId { get; set; }

		public int IsActive { get; set; }

		public DateTime CreationDate { get; set; }

		public int CreatedUserBy { get; set; }
	}
}
