﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.DTOs.UserDTOs
{
	public class LoginDTO
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
