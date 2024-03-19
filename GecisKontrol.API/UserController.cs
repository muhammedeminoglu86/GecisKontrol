using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GecisKontrol.Domain.Model;
using GecisKontrol.Domain.DTOs.UserDTOs;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace GecisKontrol.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConnectionMultiplexer _redis;

        public UserController(UserService userService, IConnectionMultiplexer redis)
        {
            _userService = userService;
            _redis = redis;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var cacheKey = $"user_{id}";
            var db = _redis.GetDatabase();
            var cachedUser = await db.StringGetAsync(cacheKey);

            if (!cachedUser.IsNullOrEmpty)
            {
                var user = JsonConvert.DeserializeObject<User>(cachedUser);
                return Ok(ApiResponse<User>.SuccessResponse(user));
            }
            else
            {
                var user = await _userService.GetUserByIdAsync("select * from \"User\" where id = @id", new {id = id});
                if (user == null)
                {
                    return NotFound(ApiResponse<string>.ErrorResponse("Kullanıcı Bulunamadı"));
                }

                await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(user), TimeSpan.FromMinutes(5));
                return Ok(ApiResponse<User>.SuccessResponse(user));
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers(int page, int pageSize)
        {
            var cacheKey = $"users_page_{page}_size_{pageSize}";
            var db = _redis.GetDatabase();
            var cachedUsers = await db.StringGetAsync(cacheKey);

            if (!cachedUsers.IsNullOrEmpty)
            {
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(cachedUsers);
                return Ok(ApiResponse<IEnumerable<User>>.SuccessResponse(users));
            }
            else
            {
                int offset = (page - 1) * pageSize;
                var users = await _userService.GetAllUsersAsync("select * from \"User\" LIMIT @PageSize OFFSET @Offset", new { PageSize = pageSize, Offset = offset });
                await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(users));
                return Ok(ApiResponse<IEnumerable<User>>.SuccessResponse(users));
            }
        }


        [HttpPost("Insert")]
        public async Task<IActionResult> InsertUser([FromBody] UserInsertDTO userDTO)
        {
            var user = new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                Email = userDTO.Email,
                IsActive = userDTO.IsActive
            };

            int userId = await _userService.AddUserAsync(user);
            var response = ApiResponse<int>.SuccessResponse(userId);
            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO userDTO)
        {
            var user = await _userService.GetUserByIdAsync("select * from \"User\" where id = @id", new {id = id});
            if (user == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("User not found"));
            }

            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            user.IsActive = userDTO.IsActive;

            await _userService.UpdateUserAsync(user);
            return Ok(ApiResponse<User>.SuccessResponse(user));
        }
    }
}
