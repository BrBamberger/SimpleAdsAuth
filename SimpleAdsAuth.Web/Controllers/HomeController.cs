using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleAdsAuth.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SimpleAdsAuth.Data;

namespace SimpleAdsAuth.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=AdSite;Integrated Security=true;";

        public IActionResult Index()
        {
            var repo = new SimpleAdsRepository(_connectionString);
            var vm = new HomeIndexViewModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated
            };
            if (!vm.IsAuthenticated)
            {
                vm.Ads= repo.GetAllAds();
            }
            
            
            return View(vm);
        }
        public IActionResult NewAd()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }      
                return View();
            
        }
        [Authorize]
        public IActionResult MyAccount(int id)
        {
            var repo = new SimpleAdsRepository(_connectionString);
            var vm = new HomeIndexViewModel
            {
                Ads = repo.GetAllAds(id)
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize]
        public IActionResult DeleteAd (int id)
        {
            var repo = new SimpleAdsRepository(_connectionString);
            repo.DeleteAd(id);
            return Redirect("/");
        }
       
    }
}
