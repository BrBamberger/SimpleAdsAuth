using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleAdsAuth.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SimpleAdsAuth.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=AdSite;Integrated Security=true;";
        
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login (string email, string password)
        {
            var claims = new List<Claim>
            {
                new Claim ("user", email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect("/home/secret");
        }
    }
}
