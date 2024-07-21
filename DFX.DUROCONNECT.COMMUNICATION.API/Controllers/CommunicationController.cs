using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using DFX.DUROCONNECT.COMM.BAL;
using DFX.DUROCONNECT.COMM.ENTITIES;

namespace DFX.DUROCONNECT.COMMUNICATION.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        private readonly ILogger<CommunicationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        CommFacade CommFacade = new CommFacade();
        Communication communication = new Communication();
        public CommunicationController(ILogger<CommunicationController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> SendTrigger()
        {
            communication.GetUserDetails("9658041314");
            return Ok();
        }


    }
}
