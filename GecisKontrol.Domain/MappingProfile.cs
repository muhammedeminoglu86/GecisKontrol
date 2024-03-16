using GecisKontrol.Domain.Model.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GecisKontrol.Domain.DTOs.UserDTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GecisKontrol.Domain
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<RegisterDTO, ApplicationUser>();
			CreateMap<LoginDTO, ApplicationUser>();
			// Diğer mappingler...
		}
	}
}
