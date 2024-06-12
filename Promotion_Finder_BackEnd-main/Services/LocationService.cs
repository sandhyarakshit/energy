using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SubwayPromotion.Models;
using SubwayPromotion.Settings;

namespace SubwayPromotion.Services

{
    public class LocationService
    {
        private readonly IMongoCollection<LocationDTO> _locations;
        public LocationService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));

            var database = client.GetDatabase("PromotionFinder");
            _locations = database.GetCollection<LocationDTO>("Locations");
        }
        
        public async Task<List<LocationDTO>> GetByLocationIds(IEnumerable<string> locationIds)
        {

            var locationFilter = Builders<LocationDTO>.Filter.In(x => x.Id, locationIds);
            var locations = await _locations.Find(locationFilter).ToListAsync();
            return locations.ToList();
        }

        public async Task<List<string>> GetAllCountries()
        {
            return await _locations.Distinct(x => x.Country, x => true).ToListAsync();
        }

        public async Task<IEnumerable<LocationDTO>> GetAll()
        {
            return await _locations.Find(x => true).ToListAsync();
        }
        public async Task Add(LocationDTO location)
        {
            if (location.Id == null)
            {
                location.Id = GenerateNewId();
            }
            await _locations.InsertOneAsync(location);
        }
    }
}