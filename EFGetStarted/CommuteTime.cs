namespace EFGetStarted
{
    public class CommuteTime
    {
        public int ID { get; set; }

        public virtual int? TravelTimeCycling { get; set; }

        public virtual int? TravelTimeDriving { get; set; }

        public virtual int? TravelTimePublicTransport { get; set; }
    }
}