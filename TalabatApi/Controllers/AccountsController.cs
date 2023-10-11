using AutoMapper;
using CoreLayer.Entities.IdentityModule;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using TalabatApi.Dtos;
using TalabatApi.Errors;
using TalabatApi.Extensions;

namespace TalabatApi.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ItokenService _tokenSevice;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,
            ItokenService tokenSevice , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenSevice = tokenSevice;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto Model)
        {
            var user = await _userManager.FindByEmailAsync(Model.Email);
            if (user == null) return  Unauthorized(new ApiErrorResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, "Pa$$w0rd", false);
            if (!result.Succeeded) return Unauthorized(new ApiErrorResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenSevice.CreateTokenAsync(user, _userManager)
            }) ;
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto Model)
        {
            var user = new AppUser()
            {
                DisplayName = Model.DisplayName,
                Email = Model.Email,
                UserName = Model.Email.Split("@")[0],
                PhoneNumber = Model.PhoneNumber,
            };

            await _userManager.CreateAsync(user, "Pa$$w0rd");

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenSevice.CreateTokenAsync(user, _userManager)
            });      
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenSevice.CreateTokenAsync(user, _userManager)
            }) ;
            
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.GetUserWithAddressAsync(User);

            var mappedAddress = _mapper.Map<Address, AddressDto>(user.UserAddress);

            return Ok(mappedAddress);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto updatedAddress)
        {
            var Address = _mapper.Map<AddressDto, Address>(updatedAddress);

            var user = await _userManager.GetUserWithAddressAsync(User);

            Address.Id = user.UserAddress.Id;

            user.UserAddress = Address;


            var result = await _userManager.UpdateAsync(user);
            if (result is null) return BadRequest(new ApiErrorResponse(400));


            return Ok(updatedAddress);


        }

    }
}
