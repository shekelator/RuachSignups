using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RuachSignups.Infrastructure;
using RuachSignups.Models;
using Signups.Core;

namespace RuachSignups.Controllers
{
    public class SignupsController : Controller
    {
        private readonly ShabbatRepository m_repository;

        public SignupsController(ShabbatRepository repository)
        {
            m_repository = repository;
        }

        // GET: Signups
        public ActionResult Index()
        {
            using (var client = new WebClient())
            {
                var jsonData = client.DownloadString("https://www.hebcal.com/hebcal/?v=1&cfg=json&year=now&month=x&maj=on&nx=on&ss=on&s=on&i=off");

                var readings = Parshiot.ParseWithAliyah(jsonData, 6)
                    .Select(r => new ShabbatModel(r));

                return View(readings);
            }
        }

        // GET: Signups/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Signups/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult CreateAll()
        {
            var shabbats = GetReadings().Select(Shabbat.parashaToShabbat);
            m_repository.UpsertMany(shabbats).GetAwaiter().GetResult();
            return View();
        }

        // POST: Signups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Signups/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Signups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Signups/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Signups/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<Parshiot.Reading> GetReadings()
        {
            using (var client = new WebClient())
            {
                var jsonData = client.DownloadString("https://www.hebcal.com/hebcal/?v=1&cfg=json&year=now&month=x&maj=on&nx=on&ss=on&s=on&i=off");

                var readings = Parshiot.Parse(jsonData);

                return readings;
            }
        }

    }
}