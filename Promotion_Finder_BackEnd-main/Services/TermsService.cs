using MongoDB.Bson;
using MongoDB.Driver;
using SubwayPromotion.Models;
using SubwayPromotion.Settings;

namespace SubwayPromotion.Services

{
    public class TermsService
    {
        private readonly IMongoCollection<TermDTO> _terms;
        public TermsService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));

            var database = client.GetDatabase("PromotionFinder");
            _terms = database.GetCollection<TermDTO>("Terms");
        }

        //Add a new term
        public async Task Add(TermDTO term)
        {
            if (term.Id == null)
            {
                term.Id = ObjectId.GenerateNewId().ToString();
            }
            await _terms.InsertOneAsync(term);
        }
        //Get all terms
        public async Task<IEnumerable<TermDTO>> GetAll()
        {
            return await _terms.Find(x => true).ToListAsync();
        }
        public async Task<IEnumerable<TermDTO>> GetAllByIds(IEnumerable<string> termIds)
        {
            var termsFilter = Builders<TermDTO>.Filter.In(x => x.Id, termIds);
            var terms = await _terms.Find(termsFilter).ToListAsync();
            return terms;
        }

    }

}