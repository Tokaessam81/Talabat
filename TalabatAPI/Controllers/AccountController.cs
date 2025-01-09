using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using TalabatAPI.DTO;
using TalabatAPI.Errors;
using TalabatAPI.Extentions;

namespace TalabatAPI.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthServices _auth;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthServices auth, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _auth = auth;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> LoginAsync(LoginDTO userlogin)
        {
            var user = await _userManager.FindByEmailAsync(userlogin.Email);
            if (user == null) return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));
            var passwordcheck = await _signInManager.CheckPasswordSignInAsync(user, userlogin.Password, false);
            if (passwordcheck.Succeeded is false)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));
            return Ok(new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _auth.CreateToken(user, _userManager)
            });
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO register)
        {
            if (checkEmailAddress(register.Email).Result.Value)
                return BadRequest(new ApiResponseValidationError() { Errors = new string[] { "This User Has Registered" } });
            else
            {
                var user = new AppUser()
                {
                    DisplayName = register.DisplayName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    UserName = register.Email.Split('@')[0],
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded is false)
                    return BadRequest(new ApiResponse(400));
                return Ok(new UserDTO()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await _auth.CreateToken(user, _userManager)
                });
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(
                new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _auth.CreateToken(user, _userManager)
                }
                );
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindAddressByEmailAsync(User);
            if (user == null) return NotFound(new ApiResponse(404));
            return Ok(
                new AddressDTO()
                {
                    FName = user.address.FName,
                    LName = user.address.LName,
                    city = user.address.City
                    ,
                    Country = user.address.Country
                    ,
                    street = user.address.Street

                }

                );
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO addressdto)
        {
            var MappingAddress = _mapper.Map<AddressDTO, Address>(addressdto);
            var user = await _userManager.FindAddressByEmailAsync(User);
            MappingAddress.Id = user.address.Id;
            user.address = MappingAddress;
            var updatingaddress = await _userManager.UpdateAsync(user);
            if (!updatingaddress.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(user.address);
        }
        [HttpGet("CheckEmail")]
        public async  Task<ActionResult<bool>> checkEmailAddress(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}