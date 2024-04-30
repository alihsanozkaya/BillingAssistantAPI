using BillingAssistant.Business.Abstract;
using BillingAssistant.EmailService;
using BillingAssistant.Entities.DTOs.AuthDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillingAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthService _authService;
        private IEmailSender _emailSender;
        private IUserService _userService;
        public AuthsController(IAuthService authService, IEmailSender emailSender, IUserService userService)
        {
            _authService = authService;
            _emailSender = emailSender;
            _userService = userService;
        }
        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("updateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserForUpdateProfileDto userForUpdateProfileDto)
        {
            var result = await _authService.UpdateProfile(userForUpdateProfileDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut("verification")]
        public async Task<IActionResult> Verification([FromBody] UserForVerifiedDto userForVerifiedDto)
        {
            var result = await _authService.Verification(userForVerifiedDto);
            if (result != null)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        [HttpGet("sendEmailAsync")]
        public async Task<IActionResult> SendEmailAsync(string email)
        {
            var message = new Message(new string[] { email }, "Doğrulama maili", "http://localhost:3000/verified/" + email);
            await _emailSender.SendEmailAsync(message);
            return Ok();
        }
        [HttpGet("verificationStatus")]
        public async Task<IActionResult> GetUserVerificationStatus(string email)
        {
            var result = await _authService.GetUserVerificationStatus(email);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("getUserProfile")]

        public async Task<IActionResult> GetUserProfile(int id)
        {
            var result = await _authService.GetUserProfile(id);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}