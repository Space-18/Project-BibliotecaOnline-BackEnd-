using Microsoft.AspNetCore.Identity;
using Negocio.DTOs;
using Negocio.DTOs.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface IAuth
    {
        public Task<AuthResponse> Register(UserCredentials credentials);
        public Task<AuthResponse> Login(UserCredentials credentials);
        public Task<AuthResponse> Refresh(Claim claim);
        public Task<IdentityResult> AddAdmin(string email);
        public Task<IdentityResult> RemoveAdmin(string email);
    }
}
