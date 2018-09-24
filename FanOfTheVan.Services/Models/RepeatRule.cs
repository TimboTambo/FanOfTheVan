using System;
using System.Collections.Generic;
using System.Text;

namespace FanOfTheVan.Services.Models
{
    public class RepeatRule
    {
        public RepeatType RepeatType { get; set; }
        // Really only applicable to monthly - could make monthly derive from base later
        public int WeekOfMonth { get; set; }
    }

    public enum RepeatType
    {
        Weekly = 0,
        Monthly = 1
    }
}
