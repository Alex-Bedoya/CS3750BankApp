using CS3750BankApp.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CS3750BankApp.Models;
using System.Text;

namespace CS3750BankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BankDbContext bankDb;

        /* public IndexModel(BankDbContext bankDb)
         {
             this.bankDb = bankDb;
         }*/



        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            User user = new User();
            string accountId = Request.Form["id"];
            string password = Request.Form["password"];

            try
            {              
                user = DbRepository.findUser(accountId);
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            
            if(user == null)
            {
                Console.WriteLine("no user");
            }
            try
            {
                if (user.AccountNumber == Int32.Parse(accountId))
                {
                    if (user.HashedPass == DbRepository.HashPassword(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(user.Salt)))
                    {
                        Console.WriteLine("logged in");
                    }
                    else { Console.WriteLine("wrong password"); }
                }
            }catch(Exception e) { Console.WriteLine(e); }
        }
        

        
        
    }
}