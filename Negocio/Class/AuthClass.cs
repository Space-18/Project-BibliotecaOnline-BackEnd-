using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using Negocio.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Negocio.Class
{
    public class AuthClass : IAuth
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthClass(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<AuthResponse> Login(UserCredentials credentials)
        {
            var result = await _signInManager.PasswordSignInAsync
                (credentials.Email, credentials.Password,isPersistent:false,lockoutOnFailure:false);

            if (result.Succeeded)
            {
                return await BuildToken(credentials);
            }
            else
            {
                return null;
            }
        }

        public async Task<AuthResponse> Register(UserCredentials credentials)
        {
            var usuario = new IdentityUser { UserName = credentials.Email, Email = credentials.Email};

            var result = await _userManager.CreateAsync(usuario, credentials.Password);

            if (result.Succeeded)
            {
                return await BuildToken(credentials);
            }
            else
            {
                return null;
            }
        }

        public async Task<AuthResponse> Refresh(Claim claim)
        {
            var email = claim.Value;

            var credential = new UserCredentials()
            {
                Email = email
            };

            return await BuildToken(credential);
        }

        public async Task<IdentityResult> AddAdmin(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddClaimAsync(usuario, new Claim("admin","si"));

            return result;
        }

        public async Task<IdentityResult> RemoveAdmin(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.RemoveClaimAsync(usuario, new Claim("admin", "si"));

            return result;
        }

        private async Task<AuthResponse> BuildToken(UserCredentials credentials)
        {
            var claims = new List<Claim>() { new Claim("email",credentials.Email) };

            var usuario = await _userManager.FindByEmailAsync(credentials.Email);

            var claimDB = await _userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer:null, audience:null,claims: claims, expires: expiration, signingCredentials: creds);

            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }
    }
}
