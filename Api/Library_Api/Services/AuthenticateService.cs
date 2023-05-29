using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Api.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Library_Api.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticateService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password,
                false, lockoutOnFailure: false);

            return result.Succeeded;

        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var libraryUser = new IdentityUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(libraryUser, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(libraryUser, isPersistent: false);
            }
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        
    }
}