using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using RazorPages_Pal_App.Data;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Models;
using System.Security.Policy;

namespace RazorPages_Pal_App.Service
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        public AuthService(SignInManager<ApplicationUser> signInManager,
                          ILogger<AuthService> logger, ApplicationDbContext context,
                           UserManager<ApplicationUser> userManager,
                           IUserStore<ApplicationUser> userStore)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    Console.WriteLine("result : "+result.ToJson);
                     
                    

                   
                 //   _context.Users.Update();
                    return "1";
                }
                return "0";
            }
            catch (Exception e)
            {

               return e.Message;
            }
            
        }
        public async Task<string> Register(RegisterDto registerDto)
        {
            try
            {
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, registerDto.Email, CancellationToken.None);
                user.CreationTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                user.LastLoginTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                user.IsActived = true;
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation(" Date : "+DateTime.Now+"User created a new account with password, "+user.Email);

                    var userId = await _userManager.GetUserIdAsync(user);
                    return "1";
                }
                else
                {
                    //internal error
                    Console.WriteLine("Date : "+DateTime.Now+" ,Internal error on auth service");
                    return "0";
                }
            }

            catch (Exception e)
            {

                return e.Message;
            }
        }

        [Authorize]
        public async Task<UserDto> getUserData(string userId)
        {
            UserDto _response = new();
            try
            {
             
                var _data = await _context.Users.FindAsync(userId);
                if (_data!=null)
                {
                                        return _response;
                }
                else
                {
                    
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Date : " + DateTime.Now + " , Error on AuthService.getUserData : " + e.Message);
                
                return null;
            }
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
