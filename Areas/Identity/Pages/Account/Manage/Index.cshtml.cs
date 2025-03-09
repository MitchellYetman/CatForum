// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CatForum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatForum.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 
            /// 
            ///////////////////////////////////////
            // BEGIN: ApplicationUser Custom Fields
            ///////////////////////////////////////

            [Required]
            [Display(Name = "Name or Handle")]
            public string Name { get; set; }
            public string Location { get; set; }
            public string ImageFilename { get; set; }

            [Display(Name = "Change Profile Picture")]
            public IFormFile ImageFile { get; set; }

            /////////////////////////////////////
            // END: ApplicationUser Custom Fields
            /////////////////////////////////////


            //[Phone]
            //[Display(Name = "Phone number")]
            //public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                ///////////////////////////////////////
                // BEGIN: ApplicationUser Custom Fields
                ///////////////////////////////////////
                Name = user.Name,
                Location = user.Location,
                ImageFilename = user.ImageFilename
                ///////////////////////////////////////
                // END: ApplicationUser Custom Fields
                ///////////////////////////////////////
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //
            //** Leaving this here for my own reference. Was helpful for debugging **
            //FROM CHATGPT
            //

            //if (!ModelState.IsValid) {
            //    Console.WriteLine("model state not valid");
            //    Debug.WriteLine("model state not valid");

            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Console.WriteLine($"Error: {error.ErrorMessage}");
            //        Debug.WriteLine($"Error: {error.ErrorMessage}");
            //    }
            //}

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            ///////////////////////////////////////
            // BEGIN: ApplicationUser Custom Fields
            ///////////////////////////////////////

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }

            if (Input.Location != user.Location)
            {
                user.Location = Input.Location;
            }

            if (Input.ImageFile != null)
            {
                Console.WriteLine("image present");
                Debug.WriteLine("image present");
            }
            else
            {
                Console.WriteLine("image not present");
                Debug.WriteLine("image not present");
            }

            // Update the profile picture                
            if (Input.ImageFile != null)
            {
                string imageFilename = Guid.NewGuid().ToString() + Path.GetExtension(Input.ImageFile.FileName);

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile_img", imageFilename);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Input.ImageFile.CopyToAsync(fileStream);
                }

                user.ImageFilename = imageFilename;
                Debug.WriteLine($"Updated ImageFilename: {user.ImageFilename}");
                Console.WriteLine($"Updated ImageFilename: {user.ImageFilename}");
            }

            await _userManager.UpdateAsync(user);

            ///////////////////////////////////////
            // END: ApplicationUser Custom Fields
            ///////////////////////////////////////

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
