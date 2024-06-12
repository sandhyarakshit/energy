using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SubwayPromotion.Models
{
    public class PromotionDTO
    {
        public int Id { get; set; }
        public string PromotionName { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string Country { get; set; }
        public List<string> Locations { get; set; }
        public List<string> TermsOfUse { get; set; }
    }

    public class PromotionUI
    {
        public string? PromotionID { get; set; }
        public string? PromotionName { get; set; }
        public List<string>? TermsOfUse { get; set; }
        public List<string>? Locations { get; set; }
    }
    public class Promotion
    {
        public string? PromotionID { get; set; }
        public string? PromotionName { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string? Country { get; set; }
        public List<string>? Locations { get; set; }
        public List<string>? TermsOfUse { get; set; }
    }
}
