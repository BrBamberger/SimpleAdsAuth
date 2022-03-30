using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleAdsAuth.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAdsAuth.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=AdSite;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
