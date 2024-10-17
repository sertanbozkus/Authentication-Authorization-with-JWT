using CustomIdentityLiveClass.Dtos;
using CustomIdentityLiveClass.Jwt;
using CustomIdentityLiveClass.Models;
using CustomIdentityLiveClass.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomIdentityLiveClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await  _userService.AddUser(addUserDto);

            if (result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("login")] // HttpGet veriyi query stringte taşıyacağından url'de gösterir. GÜVENSİZ!
       public async Task<IActionResult> Login(LoginRequest request)
        {
            var loginUserDto = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };

           var result = await _userService.LoginUser(loginUserDto);

            if (!result.IsSucceed)
                return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwt(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)




            });

            return Ok( new LoginResponse
            {
                Message = "Giriş başarıyla tamamlandı.",
                Token = token
                
            });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Test()
        {


            return Ok("Test end-point cevabı.");
        }
       

        

    }
}
