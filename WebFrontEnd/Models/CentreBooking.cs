namespace WebFrontEnd.Models
{
    public class CentreBooking
    {
        public int Id { get; set; }
        public string booingCentreName { get; set; }
        public string userName { get; set; }
        public DateOnly startDate { get; set;}
        public DateOnly endDate { get; set;}
        public string description { get; set; }
    }
}
