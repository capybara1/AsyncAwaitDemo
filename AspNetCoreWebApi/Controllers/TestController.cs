using AsyncAwait.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AsyncAwait.AspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTextTextAsync()
        {
            var result = await _testService.ProvideTestValueAsync();

            return Ok(result);
        }
        
    }
}
