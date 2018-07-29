using System;
using System.Collections.Generic;

namespace FanOfTheVan.Services.Models
{
    public class Stall
    {
        public int StallId { get; set; }
        public string Name { get; set; }
        public Guid OwnerUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateRunningSince { get; set; }
        public string Description { get; set; }
        public int PrimaryPhotoId { get; set; }
        public List<Cuisine> Cuisines { get; set; }
        public Menu Menu { get; set; }
    }
}
