using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Tokyo.Core.Interfaces;
using Tokyo.Core.ViewModel.Auth;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly Serilog.ILogger _logger;

        public AuthenticationController(IAuthService authService, Serilog.ILogger logger)
        {
            _authService = authService;
            _logger = logger;
        }


        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            _logger.Information("Initiating Login API");
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Registeration(model, UserRoles.Admin);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRoles(UserRoleNamesModel roles)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var status = await _authService.CreateRoles(roles);
                if (!status)
                {
                    return BadRequest("Oops! Something went wrong.");
                }
                return CreatedAtAction(nameof(CreateRoles), status);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("assign-role-user/{nameIdentifier}")]
        public async Task<IActionResult> AssignRoleToUser(string nameIdentifier, UserRoleNamesModel userAndUserRole)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var status = await _authService.AssignRoleToUser(nameIdentifier, userAndUserRole);
                if (!status)
                {
                    return BadRequest("Oops! Something went wrong.");
                }
                return CreatedAtAction(nameof(AssignRoleToUser), status);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
