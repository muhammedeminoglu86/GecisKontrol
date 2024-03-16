using AutoMapper;
using GecisKontrol.Domain.DTOs.UserDTOs;
using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using GecisKontrol.Domain.Model.IdentityModel;
using Microsoft.AspNetCore.Identity;


public class UserService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IMapper _mapper;

	public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_userManager = userManager;
		_signInManager = signInManager;
		_mapper = mapper;

	}

	public async Task<IEnumerable<User>> GetAllUsersAsync()
	{
		return await _unitOfWork._userRepository.GetAllUsersAsync();
	}

	public async Task<User> GetUserByIdAsync(int id)
	{
		return await _unitOfWork._userRepository.GetUserByIdAsync(id);
	}

	public async Task<int> AddUserAsync(User user)
	{
		try
		{
			var userId = await _unitOfWork._userRepository.AddUserAsync(user);
			await _unitOfWork.CommitAsync();
			return userId;
		}
		catch (Exception)
		{
			_unitOfWork.Rollback();
			throw;
		}
	}

	public async Task UpdateUserAsync(User user)
	{
		try
		{
			await _unitOfWork._userRepository.UpdateUserAsync(user);
			await _unitOfWork.CommitAsync();
		}
		catch (Exception)
		{
			_unitOfWork.Rollback();
			throw;
		}
	}

	public async Task DeleteUserAsync(int id)
	{
		try
		{
			await _unitOfWork._userRepository.DeleteUserAsync(id);
			await _unitOfWork.CommitAsync();
		}
		catch (Exception)
		{
			_unitOfWork.Rollback();
			throw;
		}
	}

	public async Task<IdentityResult> RegisterUserAsync(RegisterDTO registerDTO)
	{
		var user = _mapper.Map<ApplicationUser>(registerDTO);
		return await _userManager.CreateAsync(user, registerDTO.Password);
	}

	// DTO kullanarak kullanıcı girişi
	public async Task<SignInResult> LoginUserAsync(LoginDTO loginDTO)
	{
		return await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);
	}
}