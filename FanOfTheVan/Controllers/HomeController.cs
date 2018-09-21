using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanOfTheVan.Models;
using FanOfTheVan.Services;
using FanOfTheVan.Services.Models;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;

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
        public async Task<IActionResult> Index()
        {
            var markets = await _marketService.GetAllMarkets();
            return View(markets);
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Market newMarket)
        {
            await _marketService.CreateMarket(newMarket);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string marketId)
        {
            var market = await _marketService.GetMarket(marketId);
            return View(market);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var markets = await _marketService.GetAllMarkets();
            return View(markets);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Market market)
        {
            await _marketService.UpdateMarket(market);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<string> GetMarketsNearLocation(double lat, double longi, int distance)
        {
            var nearbyMarkets = await _marketService.GetMarketsWithinDistance(lat, longi, distance);
            return JsonConvert.SerializeObject(nearbyMarkets);
        }
    }
}
