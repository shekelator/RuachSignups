using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RuachSignups.Models;
using Signups.Core;

namespace RuachSignups.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signups()
        {
            using (var client = new WebClient())
            {
                var jsonData = client.DownloadString("https://www.hebcal.com/hebcal/?v=1&cfg=json&year=now&month=x&maj=on&nx=on&ss=on&s=on&i=off");

                var readings = Parshiot.Parse(jsonData)
                    .Select(r => new Shabbat(r));

                return View(readings);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
