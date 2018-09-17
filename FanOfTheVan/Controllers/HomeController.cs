using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanOfTheVan.Models;
using FanOfTheVan.Services;
using FanOfTheVan.Services.Models;
using MongoDB.Bson;

namespace FanOfTheVan.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMarketService _marketService;

        public HomeController(IMarketService marketService)
        {
            _marketService = marketService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var markets = _marketService.GetAllMarkets();
            return View(markets);
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

        [HttpPost]
        public IActionResult Create(Market newMarket)
        {
            _marketService.CreateMarket(newMarket);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string marketId)
        {
            var market = _marketService.GetMarket(marketId);
            return View(market);
        }

        [HttpPost]
        public IActionResult Edit(Market market)
        {
            _marketService.UpdateMarket(market);
            return RedirectToAction("Index");
        }
    }
}
