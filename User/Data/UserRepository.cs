using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities_aspnet.Core;
using Utilities_aspnet.User.Dtos;
using Utilities_aspnet.User.Entities;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Enums;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.User.Data
{
    public interface IUserRepository
    {
        Task<ApiResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto aspNetUser);
        Task<ApiResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto model);
        Task<ApiResponse> RegisterWithMobile(RegisterWithMobileDto aspNetUser);
        Task<ApiResponse<UserReadDto?>> LoginWithMobile(LoginWithMobileDto model);
        ApiResponse RequestVerificationCode(RequestVerificationCodeDto dto);
        ApiResponse VerifyMobileForLogin(RequestVerificationCodeDto dto);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IOtpService _otp;

        public UserRepository(AppDbContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager,
            IConfiguration config, IMapper mapper, IOtpService otp)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _otp = otp;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserReadDto?>> LoginWithEmail(LoginWithEmailDto model)
        {
            UserEntity? user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.NotFound, null, "Email not found");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result) return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.BadRequest, null, "The password is incorrect!");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            if (roles != null) claims.AddRange(roles.Select(role => new Claim("role", role)));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(_config["Tokens:Issuer"], _config["Tokens:Issuer"], claims,
                expires: DateTime.Now.AddDays(365), signingCredentials: creds);
            user.LastLogin = DateTime.Now;
            await _userManager.UpdateAsync(user);

            //return new{doc = GetProfile(user.Id), token = new JwtSecurityTokenHandler().WriteToken(token), message = "" };
            return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.Success,
                GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result, "Success");
        }

        public async Task<ApiResponse<UserReadDto?>> LoginWithMobile(LoginWithMobileDto model)
        {
            UserEntity? user = await _context.User.FirstOrDefaultAsync(x => x.PhoneNumber == model.Mobile);

            if (user == null) return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.NotFound, null, "Mobile not found");

            if (_otp.Verify(user.Id, model.VerificationCode) != OtpResult.Ok)
                return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.BadRequest, null, "Verification Code Is Not Valid");
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            if (roles != null) claims.AddRange(roles.Select(role => new Claim("role", role)));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(_config["Tokens:Issuer"], _config["Tokens:Issuer"], claims,
                expires: DateTime.Now.AddDays(365), signingCredentials: creds);
            user.LastLogin = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.Success,
                GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result, "Success");
        }

        public async Task<ApiResponse<UserReadDto?>> RegisterWithEmail(RegisterWithEmailDto aspNetUser)
        {
            UserEntity? model = _context.Users.FirstOrDefault(x => x.UserName == aspNetUser.UserName ||
                                                                   x.Email == aspNetUser.Email);
            if (model != null)
            {
                return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.BadRequest, null, "This email or username already exists");
            }

            UserEntity user = new UserEntity
            {
                Email = aspNetUser.Email,
                UserName = aspNetUser.UserName,
                LastLogin = null,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                CreateAccount = DateTime.Now
            };

            IdentityResult? result = await _userManager.CreateAsync(user, aspNetUser.Password);
            if (!result.Succeeded)
                return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.BadRequest, null, "The information was not entered correctly");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            if (roles != null) claims.AddRange(roles.Select(role => new Claim("role", role)));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(_config["Tokens:Issuer"], _config["Tokens:Issuer"], claims,
                expires: DateTime.Now.AddDays(365), signingCredentials: creds);

            return new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.Success,
                GetProfile(user.Id, new JwtSecurityTokenHandler().WriteToken(token)).Result.Result, "Success");
        }

        public async Task<ApiResponse> RegisterWithMobile(RegisterWithMobileDto aspNetUser)
        {
            UserEntity? model = _context.Users.FirstOrDefault(x => x.PhoneNumber == aspNetUser.Mobile);
            if (model != null)
            {
                _otp.SendOtp(model.Id);
                return new ApiResponse(UtilitiesStatusCodes.Success, "Success");
            }

            UserEntity user = new UserEntity
            {
                PhoneNumber = aspNetUser.Mobile,
                UserName = aspNetUser.Mobile.Replace("+", ""),
                LastLogin = null,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                CreateAccount = DateTime.Now
            };

            IdentityResult? result = await _userManager.CreateAsync(user, "P@ssw0rd!@#$%^&*");
            if (!result.Succeeded) return new ApiResponse(UtilitiesStatusCodes.BadRequest, "The information was not entered correctly");

            _otp.SendOtp(user.Id);
            return new ApiResponse(UtilitiesStatusCodes.Success, "Success");
        }

        public Task<ApiResponse<UserReadDto?>> GetProfile(string userId, string? token)
        {
            UserEntity? model = _context.Users.Include(u => u.Media).FirstOrDefault(u => u.Id == userId);
            UserReadDto userReadDto = _mapper.Map<UserReadDto>(model);
            userReadDto.Token = token;

            return Task.FromResult(new ApiResponse<UserReadDto?>(UtilitiesStatusCodes.Success, userReadDto, "Success"));
        }


        ApiResponse IUserRepository.RequestVerificationCode(RequestVerificationCodeDto dto)
        {
            throw new NotImplementedException();
        }

        ApiResponse IUserRepository.VerifyMobileForLogin(RequestVerificationCodeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}