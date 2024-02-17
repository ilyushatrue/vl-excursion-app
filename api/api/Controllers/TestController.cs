using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController(ILogger<TestController> logger) : ControllerBase
    {
        [HttpGet(Name = "api/test")]
        public async Task<int[]> Test()
        {
            await Task.Delay(1000);
            return [1, 2, 3];
        }
    }
}
