using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class ErrorLog
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		[StringLength(255)]
		public string ControllerName { get; set; }

		[StringLength(255)]
		public string ActionName { get; set; }

		public string ErrorName { get; set; }

		public string Parameters { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
