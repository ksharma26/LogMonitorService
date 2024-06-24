using Microsoft.AspNetCore.Mvc;
using LogMonitorService.Readers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogMonitorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly LogReader _logReader;

        public LogController(LogReader logReader)
        {
            _logReader = logReader;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs(string filename, int page = 1, int pageSize = 100, string keyword = null)
        {
            try
            {
                var lines = await _logReader.GetLogLinesAsync(filename, page, pageSize, keyword);
                return Ok(lines);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
