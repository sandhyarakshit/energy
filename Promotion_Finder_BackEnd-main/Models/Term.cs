using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SubwayPromotion.Models
{
    public class TermDTO
    {
        public int Id { get; set; }
        public string TermConditon { get; set; }

    }
    public class Term
    {
        public string? Id { get; set; }
        public string TermConditon { get; set; }
    }
}
