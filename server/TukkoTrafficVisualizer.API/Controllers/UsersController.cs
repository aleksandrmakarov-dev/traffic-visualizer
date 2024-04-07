using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.API.Attributes;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Authorize(Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
    }
}
