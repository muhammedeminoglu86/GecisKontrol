using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IErrorLogService
	{
		Task AddErrorLogAsync(ErrorLog errorLog);
	}
}
