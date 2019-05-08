using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApp.Interfaces;

namespace WebApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ITimeService timeService;

        public TimeController(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetTime(string statusCode = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(statusCode))
                {
                    string result = await timeService.GetTimeAsync();
                    return new OkObjectResult(result);
                }

                if (Enum.TryParse(statusCode, true, out HttpStatusCode id))
                {
                    return new StatusCodeResult((int)id);
                }

                return new BadRequestResult();
            }
            catch (Exception)
            {
                var response = StatusCode((int)HttpStatusCode.InternalServerError, "Unexpected server error.");
                return response;
            }
        }
    }
}
