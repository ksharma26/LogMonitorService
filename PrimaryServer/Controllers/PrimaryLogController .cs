using LogMonitorService.Providers;
using LogMonitorService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogMonitorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimaryLogController : ControllerBase
    {
        private readonly LogProvider _logProvider;
        private readonly Validator _validator;

        public PrimaryLogController(LogProvider logProvider, Validator validator)
        {
            _logProvider = logProvider;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs(string filename, string secondaryIp, int page = 1, int pageSize = 100, string keyword = null)
        {
            if (!_validator.IsValidIp(secondaryIp))
            {
                return BadRequest("Invalid IP address.");
            }

            if(!_validator.isFileNameValid(filename)) {
                return BadRequest("Invalid file name");
            }

            var logs = await _logProvider.GetLogsAsync(secondaryIp, filename, page, pageSize, keyword);
            return Ok(logs);
        }
    }
}
