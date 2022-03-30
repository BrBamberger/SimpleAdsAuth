using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleAdsAuth.Data;

namespace SimpleAdsAuth.Web.Models
{
    public class HomeIndexViewModel
    {
        public List <Ad> Ads { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
