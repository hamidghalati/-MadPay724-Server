using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MadPay724.Common.ErrorsAndMessage;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Dtos.Site.Admin;
using MadPay724.Data.Models;
using MadPay724.Repo.Infrastructure;
using MadPay724.Services.Site.Admin.Interface;
using MadPay724.Services.Site.Admin.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MadPay724.Presentation.Controllers.Site.Admin
{
    [Route("Site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<MadPayDbContext> _db;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IUnitOfWork<MadPayDbContext> dbContext, IAuthService authService, IConfiguration config)
        {
            _db = dbContext;
            _authService = new AuthService(_db);
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRepository.UserExists(userForRegisterDto.UserName))
            {
                return BadRequest(new ReturnMessage()
                {
                    status = false,
                    title = "خطا",
                    message = "این نام کاربری وجود دارد"
                });

            }

            var UserToCreate = new User
            {
                UserName = userForRegisterDto.UserName,
                Name = userForRegisterDto.Name,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Address = "",
                Gender = true,
                DateOfBirth = DateTime.Now,
                IsActive = true,
                Status = true
            };
            var CreateUser = await _authService.Register(UserToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authService.Login(userForLoginDto.UserName, userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized(new ReturnMessage()
                {
                    status = false,
                    title = "خطا",
                    message = "کاربری با این یوزر و پس وجود ندارد"
                });

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = userForLoginDto.IsRemember ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDes);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }



        //// GET api/values
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<string>>> Get()
        //{
        //    var user = new User()
        //    {
        //        Address = "شیراز",
        //        City = "",
        //        Name = "حمیدرضا سمیعی نیا",
        //        UserName = "hamidghalati",
        //        PhoneNumber = "09351360402",
        //        IsActive = true,
        //        PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20 },
        //        PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20 }

        //    };
        //    var u = await _authService.Register(user, "hamid2311");

        //    return Ok(u);
        //}
    }
}