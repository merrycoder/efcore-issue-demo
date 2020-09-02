using System.Collections.Generic;

namespace EFGetStarted
{
    public class Property
    {
        public int ID { get; set; }

        public int Price { get; set; }

        public int PriceTypeId { get; set; }

        public int CategoryId { get; set; }

        public bool IsOnline { get; set; }

        public List<CommuteTime> CommuteTimes { get; } = new List<CommuteTime>();
    }
}