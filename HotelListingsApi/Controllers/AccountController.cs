using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingsApi.Data;
using HotelListingsApi.DTO;
using HotelListingsApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelListingsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<HotelListingsApiUser> _userManager;
        // private readonly SignInManager<HotelListingsApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public AccountController(
           UserManager<HotelListingsApiUser> userManager,
        //    SignInManager<HotelListingsApiUser> signInManager,
           ILogger<AccountController> logger,
           IMapper mapper,
           IAuthService authService )
        {
            _userManager = userManager;
            // _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("register")]
        // [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            _logger.LogInformation($"Registration Attempt for {registerDTO.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<HotelListingsApiUser>(registerDTO);
                user.UserName = registerDTO.Email;
                var result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                    // return BadRequest($"User Registartion Attempt Failed");
                }

                await _userManager.AddToRolesAsync(user, registerDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation($"Login Attempt for {loginDTO.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
               if (!await _authService.ValidateUser(loginDTO))
               {
                   return Unauthorized();
               }

               return Accepted(new { Token = await _authService.CreateToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(Login)}");
                return Problem($"Somthing Went Wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }

}