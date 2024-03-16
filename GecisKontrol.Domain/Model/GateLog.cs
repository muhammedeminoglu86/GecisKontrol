using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class GateLog
	{
		public long Id { get; set; }

		public int DeviceEmployeeMappingId { get; set; }

		public DateTime ActionTime { get; set; }
	}
}
