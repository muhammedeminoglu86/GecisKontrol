using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.Interfaces
{
	public interface IUnitOfWork
	{
		public IUserRepository _userRepository { get;  }

		public IUnitRepository _unitRepository { get;  }

		public IGateRepository _gateRepository { get;  }

		public IEmployeeRepository _employeeRepository { get;  }

		public IEmployeeCardMappingRepository _employeeCardMappingRepository { get;  }
		public IErrorLogRepository _errorLogRepository { get; }

		public IDeviceRepository _deviceRepository { get;  }

		public IDeviceRoleRepository _deviceRoleRepository { get;  }

		public IDeviceProfileRepository _deviceProfileRepository { get;  }

		public IDeviceGateMappingRepository _deviceGateMappingRepository { get;  }

		public IDeviceEmployeeMappingRepository _deviceEmployeeMappingRepository { get;  }

		public IDepartmentRepository _departmentRepository { get;  }

		public ICompanyRepository _companyRepository { get;  }

		public ICardRepository _cardRepository { get;  }
        public IEmployeeCardMappingDTORepository _employeeCardMappingDtoRepository { get; }

		// Diğer repository'ler...

		Task CommitAsync();
		void Rollback();
	}
}
