using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;


namespace CS3750BankApp.Pages
{
    public class Index1Model : PageModel
    {
        public string emailExistsMsg;

        [BindProperty]
        public Credential Credentials { get; set; }

        public void OnGet()
        {
            emailExistsMsg = HttpContext.Session.GetString("EmailExists");
            Console.WriteLine("session= " + emailExistsMsg);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            if (DbRepository.UserExistsByEmail(Credentials.Email))
            {
                Console.WriteLine("User Email Exists");
                HttpContext.Session.SetString("EmailExists", "Email already exists.");                  
                return RedirectToPage("Register");
            }

            User user = CreateUserModel();
            DbRepository.CreateUser(user);
            HttpContext.Session.SetInt32("_Account", user.AccountNumber);

            return RedirectToPage("AccountsView");
        }

        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public User CreateUserModel()
        {
            User user = new User();
            string salt = GenerateSalt();
            user.Salt = salt;
            user.Email = Credentials.Email;
            string pass = Credentials.ConfirmPassword;
            user.HashedPass = DbRepository.HashPassword(Encoding.UTF8.GetBytes(pass), Encoding.UTF8.GetBytes(salt));

            return user;
        }

        public class Credential
        {
            [Required]
            public string Email { get; set; }
            [Required, MinLength(8)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required, Compare(nameof(Password), ErrorMessage ="Passwords must match")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
            public bool Correct { get; set; }
        }


    }
}
