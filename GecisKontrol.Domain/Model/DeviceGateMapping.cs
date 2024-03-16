using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class DeviceGateMapping
	{
		public int Id { get; set; }

		public int DeviceId { get; set; }

		public int GateId { get; set; }

		[StringLength(255)]
		public string IpAddress { get; set; }

		[StringLength(255)]
		public string SessionGuid { get; set; }

		public int IsActive { get; set; }

		public DateTime LastOperation { get; set; }

		public int CreatedUserBy { get; set; }

		public DateTime CreationDate { get; set; }

		public int DeviceRoleId { get; set; }
	}
}
