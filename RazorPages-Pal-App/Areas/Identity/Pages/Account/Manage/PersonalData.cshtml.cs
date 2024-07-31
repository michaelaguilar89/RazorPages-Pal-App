// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Models;
using RazorPages_Pal_App.Service;

namespace RazorPages_Pal_App.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly AuthService _authService;

        public PersonalDataModel(
            UserManager<ApplicationUser> userManager,
            ILogger<PersonalDataModel> logger,
            AuthService authService)
        {
            _userManager = userManager;
            _logger = logger;
            _authService = authService;
        }
        public string Messages { get; set; }

        [BindProperty(SupportsGet =true)]
        public UserDto userDto { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user!=null)
            {
                userDto.UserName = user.UserName.ToString();
                Console.WriteLine("Date : " + DateTime.Now + " , User : " + user.UserName.ToString());

                return Page();
            }
            else
            {
                Messages = "Unable to load user ";
                return Page();

            }
        }
    }
}
