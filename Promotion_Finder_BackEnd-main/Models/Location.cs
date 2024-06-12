using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SubwayPromotion.Models
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    public class Location
    {
        public string? Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}