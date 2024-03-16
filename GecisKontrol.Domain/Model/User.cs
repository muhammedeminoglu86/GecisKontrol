using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Model
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Username { get; set; }

		[Required]
		[StringLength(255)]
		public string Password { get; set; }

		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		public int IsActive { get; set; }

		public DateTime CreationDate { get; set; }

		public short CreatedUserId { get; set; }
	}

}
