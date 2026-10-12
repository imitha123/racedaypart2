namespace poepart2.Model
{
    public class EventModel
    {
        public int ÉventId { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public string EventDate { get; set; }

        public int OrganizationId { get; set; }

        public int CategoryId { get; set; }
    }
}
