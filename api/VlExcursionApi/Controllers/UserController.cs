using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace VlExcursionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService userService;

        public UserController(
            ILogger<UserController> logger,
            UserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await userService.GetUserByIdAsync(id);
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userService.GetAllUsersAsync();
        }
        [HttpPost]
        public async Task<bool> CreateUserAsync(User user)
        {
            return await userService.CreateUserAsync(user);
        }
        [HttpPut]
        public async Task<bool> UpdateUserAsync(User user)
        {
            return await userService.UpdateUserAsync(user);
        }
        [HttpDelete]
        public async Task<bool> DeleteUserAsync(User user)
        {
            return await userService.DeleteUserAsync(user.Id);
        }
    }
}
