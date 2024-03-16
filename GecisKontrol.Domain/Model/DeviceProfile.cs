using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class DeviceProfile
	{
		public int Id { get; set; }
		public int DeviceId { get; set; }

		public int OfflineMode { get; set; }

		public int WeekendMode { get; set; }
		public int TimeLimited { get; set; }

		public DateTime StarTime { get; set; }
		public DateTime EndTime { get; set; }

		public int IsActive { get; set; }

		public DateTime CreationDate { get; set; }

		public int CreatedUserBy { get; set; }
	}
}
