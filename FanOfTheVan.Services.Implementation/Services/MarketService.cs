using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanOfTheVan.Services.Implementation.Repositories;
using FanOfTheVan.Services.Models;

namespace FanOfTheVan.Services.Implementation.Services
{
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _repository;

        public MarketService(IMarketRepository repository)
        {
            _repository = repository;
        }

        public async Task<IMarket> GetMarket(string marketId)
        {
            return await _repository.GetMarketById(marketId);
        }

        public async Task CreateMarket(Market market)
        {
            await _repository.SaveMarket(market);
        }

        public async Task<IEnumerable<IMarket>> GetAllMarkets()
        {
            return await _repository.GetAllMarkets();
        }

        public async Task UpdateMarket(Market market)
        {
            await _repository.UpdateMarket(market);
        }

        public async Task<IEnumerable<IMarket>> GetMatchingMarkets(double latitude, double longitude, int distance, OpenStatus openStatus)
        {
            var markets = await GetAllMarkets();
            var marketsWithinDistance = new List<IMarket>();
            foreach (var market in markets)
            {
                var distanceToMarket = GetDistanceBetweenPoints(latitude, longitude, market.Latitude, market.Longitude);
                if (distanceToMarket > distance)
                {
                    continue;
                }
                var marketMatchesRequestedOpenStatus = DoesMarketMatchRequestedOpenStatus(market, openStatus);
                if (!marketMatchesRequestedOpenStatus)
                {
                    continue;
                }
                market.Distance = distanceToMarket;
                marketsWithinDistance.Add(market);
            }
            SetMarketColours(marketsWithinDistance);
            return SortSearchResults(marketsWithinDistance);
        }

        private bool ColourIsSimilarToOtherColours(int r, int g, int b, List<List<int>> coloursSelectedAlready)
        {
            for (var i = 0; i < coloursSelectedAlready.Count; i++)
            {
                var closeness = Math.Abs(r - coloursSelectedAlready[i][0]) + Math.Abs(r - coloursSelectedAlready[i][1]) + Math.Abs(r - coloursSelectedAlready[i][2]);
                if (closeness < 80)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetMarketColours(List<IMarket> marketsWithinDistance)
        {
            var rand = new Random();
            var coloursSelectedAlready = new List<List<int>>();
            foreach (var market in marketsWithinDistance)
            {
                int r = rand.Next(255);
                int g = rand.Next(255);
                int b = rand.Next(255);
                while (r + g + b > 600 && ColourIsSimilarToOtherColours(r, g, b, coloursSelectedAlready))
                {
                    r = rand.Next(255);
                    g = rand.Next(255);
                    b = rand.Next(255);
                }
                coloursSelectedAlready.Add(new List<int> { r, g, b });
                market.Colour = $"#{r.ToString("X2")}{g.ToString("X2")}{b.ToString("X2")}";
            }
        }

        private IEnumerable<IMarket> SortSearchResults(IEnumerable<IMarket> searchResults)
        {
            return searchResults.OrderBy(x => x.Distance);
        }

        private bool DoesMarketMatchRequestedOpenStatus(IMarket market, OpenStatus openStatus)
        {
            if (openStatus == OpenStatus.OpenOrClosed)
            {
                return true;
            }

            var dayOfWeekInQuestion = DateTime.Now.DayOfWeek;
            var dateInQuestion = DateTime.Now.Day;

            if (openStatus == OpenStatus.OpenTomorrow)
            {
                dayOfWeekInQuestion++;
                dateInQuestion++;
            }

            var isMarketEverOpenOnDayOfWeek = market.OpeningTimes.TryGetValue(dayOfWeekInQuestion, out var openingTimes);
            if (!isMarketEverOpenOnDayOfWeek) {
                return false;
            }

            market.ResultDayOfWeek = dayOfWeekInQuestion;
            market.ResultOpeningTimes = openingTimes;

            if (openingTimes.RepeatRule.RepeatType == RepeatType.Monthly)
            {
                var currentWeekOfMonth = ((dateInQuestion) / 7) + 1; 
                if (currentWeekOfMonth != openingTimes.RepeatRule.WeekOfMonth)
                {
                    return false;
                }
            }

            if (openStatus == OpenStatus.OpenToday || openStatus == OpenStatus.OpenTomorrow)
            {
                return openingTimes.OpenTime < openingTimes.CloseTime;
            }

            return openingTimes.OpenTime.Hours < DateTime.Now.Hour && openingTimes.CloseTime.Hours > DateTime.Now.Hour;
        }

        private double GetDistanceBetweenPoints(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var R = 6731f;
            var latRads1 = ToRadians(latitude1);
            var latRads2 = ToRadians(latitude2);
            var diffLatRads = ToRadians(latitude2 - latitude1);
            var diffLongRads = ToRadians(longitude2 - longitude1);

            var a = Math.Sin(diffLatRads / 2) * Math.Sin(diffLatRads / 2) + Math.Cos(latRads1) * Math.Cos(latRads2) * Math.Sin(diffLongRads / 2) * Math.Sin(diffLongRads / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * (float)Math.PI / 180f;
        }
    }
}
