using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GecisKontrol.Domain.Model;
using Business.Services;

namespace GecisKontrol.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ErrorLogController : ControllerBase
    {
        private readonly ErrorLogService _errorLogService;

        public ErrorLogController(ErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetErrorLogById(int id)
        {
            var errorLog = await _errorLogService.GetErrorLogByIdAsync(id);
            if (errorLog == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse("Error log not found"));
            }

            return Ok(ApiResponse<ErrorLog>.SuccessResponse(errorLog));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllErrorLogs()
        {
            var errorLogs = await _errorLogService.GetAllErrorLogs();
            var response = ApiResponse<IEnumerable<ErrorLog>>.SuccessResponse(errorLogs);
            return Ok(response);
        }




    }
}
