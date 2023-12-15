using System.Security.Claims;
using AutoMapper;
using JWTService.BLL.Models;
using JWTService.BLL.Models.Exceptions;
using JWTService.BLL.Services.Interfaces;
using JWTService.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWTService.DAL.Repositories.Interfaces;
using JWTService.DAL.Constants;

namespace JWTService.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;
        private readonly JwtOptions _options;
        private readonly IAuthRepository _authRepository;
        private static readonly Random random = new Random();


        public AuthService(UserManager<Customer> userManager, SignInManager<Customer> signInManager, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper, JwtOptions options, IAuthRepository authRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _options = options;
            _authRepository = authRepository;
        }
        
        public async Task<ResponseEntity<SignInResponse>> SignInAsync(SignInModel model)
        {
            var customer = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == model.Email);

            if (customer is null)
            {
                throw new NotFoundException("The user doesn't exist");
            }

            var validPassword = await _signInManager.CheckPasswordSignInAsync(customer, model.Password, false);

            if (!validPassword.Succeeded)
            {
                throw new NotFoundException("Passwords don't match");
            }

            var accessToken = JwtService.GenerateAccessToken(_options, await GenerateClaimsIdentityAsync(customer));
            var refreshToken = JwtService.GenerateRefreshToken(_options);

            return new ResponseEntity<SignInResponse>(System.Net.HttpStatusCode.Created, new SignInResponse() { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        public async Task<ResponseEntity<string>> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new ValidationException("No Accounts Registered with {model.Email}.");
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roleExists = Enum.GetNames(typeof(Authorization.Roles)).Any(x
                => x.ToLower() == model.Role.ToLower());
                if (roleExists)
                {
                    var validRole =
                    Enum.GetValues(typeof(Authorization.Roles)).Cast<Authorization.Roles>().Where(x =>
                    x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                    await _userManager.AddToRoleAsync(user, validRole.ToString());
                    return new ResponseEntity<string>(System.Net.HttpStatusCode.Created, $"Added {model.Role} to user {model.Email}.");
                }
                throw new NotFoundException($"Role {model.Role} not found.");
            }
            return new ResponseEntity<string>(System.Net.HttpStatusCode.Created, $"Incorrect Credentials for user {user.Email}.");
        }


        public async Task<ResponseEntity<SignInResponse>> SignUpAsync(SignUpModel model)
        {
            var customer = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == model.Email || user.PhoneNumber == model.PhoneNumber);

            if (customer is not null)
            {
                throw new ValidationException("The user with such credentials already exist");
            }

            var entity = _mapper.Map<Customer>(model);
            var result1 = await _userManager.CreateAsync(entity, model.Password);
            if (!result1.Succeeded)
            {
                throw new ValidationException(result1.Errors.Select(error => error.Description));
            }
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                var result2 = await _userManager.AddToRoleAsync(entity, model.Role);
                if (!result2.Succeeded)
                {
                    throw new ValidationException(result2.Errors.Select(error => error.Description));
                }
            }
            var accessToken = JwtService.GenerateAccessToken(_options, await GenerateClaimsIdentityAsync(entity));
            var refreshToken = JwtService.GenerateRefreshToken(_options);

            return new ResponseEntity<SignInResponse>(System.Net.HttpStatusCode.Created, new SignInResponse() { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        private async Task<ClaimsIdentity> GenerateClaimsIdentityAsync(DAL.Entities.Customer customer)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, customer.UserName),
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim("Id", customer.Id.ToString())
        };

            var roles = await _userManager.GetRolesAsync(customer);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return new ClaimsIdentity(claims);
        }
        public async Task<ResponseEntity> ChangePassword(SignInModel model)
        {
            var customer = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == model.Email);

            if (customer is null)
            {
                throw new NotFoundException("The user doesn't exist");
            }

            var accessToken = JwtService.GenerateAccessToken(_options, await GenerateClaimsIdentityAsync(customer));
            var refreshToken = JwtService.GenerateRefreshToken(_options);

            return new ResponseEntity<SignInResponse>(System.Net.HttpStatusCode.Created, new SignInResponse() { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        public bool RevokeToken(string token)
        {
           return _authRepository.RevokeToken(token);
        }

        public ResponseEntity<Customer> GetById(Guid id)
        {
            var customer = _authRepository.GetById(id);
            return new ResponseEntity<Customer>(System.Net.HttpStatusCode.Created, customer);
        }
    }
}