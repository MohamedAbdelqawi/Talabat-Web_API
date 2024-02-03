using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Apis.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IServices;

namespace Talabat.Apis.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var reslut = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!reslut.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto { DisplayName = user.DisplayName, Email = user.Email, Token = await _tokenService.CreateTokenAsync(user, _userManager) });

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExists(registerDto.Email).Result.Value) return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "this email is already in use!" } });
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]
            };
            var reslut = await _userManager.CreateAsync(user, registerDto.Password);
            if (!reslut.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)

            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {

            var user = await _userManager.FindUserWithAddressAsync(User);

            var address = _mapper.Map<AddressDto, Address>(updatedAddress);
            address.Id = user.Address.Id;

            var reslut = await _userManager.UpdateAsync(user);
            if (!reslut.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(updatedAddress);

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email) =>
        await _userManager.FindByEmailAsync(email) is not null;


    }
}
