using MapperApp.Enums;

namespace MapperApp.Models
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DriverNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public DriverStatus Status { get; set; }
        public int WorldChampionships { get; set; }

    }
}
