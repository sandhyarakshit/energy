using MongoDB.Driver;
using MongoDB.Bson;
using SubwayPromotion.Models;

namespace SubwayPromotion.Services
{
    public class PromotionService
    {
        private readonly IMongoCollection<PromotionDTO> _promotions;
        public PromotionService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));

            var database = client.GetDatabase("PromotionFinder");
            _promotions = database.GetCollection<PromotionDTO>("Promotions");
        }


        public async Task<IEnumerable<PromotionDTO>> GetAll()
        {
            return await _promotions.Find(x => true).ToListAsync();
        }

        public async Task<PromotionDTO> Get(int id)
        {
            return await _promotions.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PromotionDTO>> GetByCountry(string country)
        {

            var dateNow = DateTime.Now;
            var promotionFilter = Builders<PromotionDTO>.Filter.And(
                Builders<PromotionDTO>.Filter.Eq(x => x.Country, country),
                Builders<PromotionDTO>.Filter.Lte(x => x.ValidityStart, dateNow),
                Builders<PromotionDTO>.Filter.Gt(x => x.ValidityEnd, dateNow)
            );
            return await _promotions.Find(promotionFilter).ToListAsync();
        }

        public async Task Add(PromotionDTO promotion)
        {
            if (promotion.Id == null)
            {
                promotion.Id = ObjectId.GenerateNewId().ToString();
            }
            await _promotions.InsertOneAsync(promotion);
        }

        public async Task Update(PromotionDTO promotion)
        {

            var existingPromotion = await _promotions.Find(p => p.Id == promotion.Id).FirstOrDefaultAsync();
            if (existingPromotion != null)
            {
                await _promotions.ReplaceOneAsync(p => p.Id == promotion.Id, promotion);
            }
        }

        public async Task Delete(string id)
        {

            await _promotions.DeleteOneAsync(p => p.Id == id);
        }
    }
}