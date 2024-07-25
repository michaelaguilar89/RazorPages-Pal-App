using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorPages_Pal_App.Data;
using RazorPages_Pal_App.Models;

namespace RazorPages_Pal_App.Service
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthService> _logger;
        public AuthService(SignInManager<ApplicationUser> signInManager,
                          ILogger<AuthService> logger, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;

        }

    }
}
