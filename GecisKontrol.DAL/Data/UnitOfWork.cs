using System.Data;
using System.Threading.Tasks;
using GecisKontrol.DAL.Data;
using GecisKontrol.Domain.Interfaces;

namespace GecisKontrol.DAL.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _dbContext;
		private IDbConnection _connection;
		private IDbTransaction _transaction;

		public IUserRepository _userRepository { get; private set; }

		public IUnitRepository _unitRepository { get; private set; }

		public IGateRepository _gateRepository { get; private set; }

		public IEmployeeRepository _employeeRepository { get; private set; }

		public IEmployeeCardMappingRepository _employeeCardMappingRepository { get; private set; }

		public IErrorLogRepository _errorLogRepository { get; private set; }
		public IDeviceRepository _deviceRepository { get; private set; }

		public IDeviceRoleRepository _deviceRoleRepository { get; private set; }

		public IDeviceProfileRepository _deviceProfileRepository { get; private set; }

		public IDeviceGateMappingRepository _deviceGateMappingRepository { get; private set; }

		public IDeviceEmployeeMappingRepository _deviceEmployeeMappingRepository { get; private set; }

		public IDepartmentRepository _departmentRepository { get; private set; }

		public ICompanyRepository _companyRepository { get; private set; }

        public IEmployeeCardMappingDTORepository _employeeCardMappingDtoRepository{ get; private set; }

    public ICardRepository _cardRepository { get; private set; }
		public UnitOfWork(
			DbContext dbContext,
			IUserRepository userRepository,
			IUnitRepository unitRepository,
			IGateRepository gateRepository,
			IEmployeeRepository employeeRepository,
			IEmployeeCardMappingRepository employeeCardMappingRepository,
			IDeviceRepository deviceRepository,
			IDeviceRoleRepository deviceRoleRepository,
			IDeviceProfileRepository deviceProfileRepository,
			IDeviceGateMappingRepository deviceGateMappingRepository,
			IDeviceEmployeeMappingRepository deviceEmployeeMappingRepository,
			IDepartmentRepository devDepartmentRepository,
			ICompanyRepository companyRepository,
			ICardRepository cardRepository,
			IErrorLogRepository errorLogRepository,
			IEmployeeCardMappingDTORepository employeeCardMappingDtoRepository
		)
		{
			_dbContext = dbContext;
			_connection = _dbContext.CreateConnection();
			_connection.Open();
			_transaction = _connection.BeginTransaction();

			_userRepository = userRepository;
			_unitRepository = unitRepository;
			_gateRepository = gateRepository;
			_employeeRepository = employeeRepository;
			_employeeCardMappingRepository = employeeCardMappingRepository;
			_deviceRepository = deviceRepository;
			_deviceRoleRepository = deviceRoleRepository;
			_deviceProfileRepository = deviceProfileRepository;
			_deviceGateMappingRepository = deviceGateMappingRepository;
			_deviceEmployeeMappingRepository = deviceEmployeeMappingRepository;
			_departmentRepository = devDepartmentRepository;
			_companyRepository = companyRepository;
			_cardRepository = cardRepository;
			_errorLogRepository= errorLogRepository;
            _employeeCardMappingRepository = employeeCardMappingRepository;

        }

		public async Task CommitAsync()
		{
			_transaction.Commit();
			_transaction.Dispose();
			_connection.Close();
		}

		public void Rollback()
		{
			_transaction.Rollback();
			_transaction.Dispose();
			_connection.Close();
		}
	}
}