/*********************************************** 
    Repository Class of Rating Mgmt
    All the methods within Rating Mgmt
    Dilshan W.A.B. - IT21343216
 **********************************************/

using EADEcommerceBE.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EADEcommerceBE.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IMongoCollection<Rating> _ratings;
        public RatingRepository(IMongoClient client)
        {
            var database = client.GetDatabase("RatingDB");
            var collection = database.GetCollection<Rating>(nameof(Rating));

            _ratings = collection;
        }

        //Create Rating
        public async Task<ObjectId> CreateRating(Rating rating)
        {
            await _ratings.InsertOneAsync(rating);
            return rating.Id;
        }

        //Delete Rating using Rating Id
        public async Task<bool> DeleteRatingById(ObjectId objectId)
        {
            var filter = Builders<Rating>.Filter.Eq(x => x.Id, objectId);
            var result = await _ratings.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        //Return Ratings using customer Id
        public async Task<IEnumerable<Rating>> GetRatingsByCusId(string cusId)
        {
            var filter = Builders<Rating>.Filter.Eq(x => x.CusId, cusId);
            var ratings = await _ratings.Find(filter).ToListAsync();
            return ratings;
        }

        //Return Ratings using Vendor Id
        public async Task<IEnumerable<Rating>> GetRatingsByVendorId(string vendorId)
        {
            var filter = Builders<Rating>.Filter.Eq(x => x.VendorId, vendorId);
            var ratings = await _ratings.Find(filter).ToListAsync();
            // Map the ratings if needed (you can modify this mapping as per your requirements)
            var ratingList = ratings.Select(rating => new Rating
            {
                Id = rating.Id, // ObjectId will be handled as is
                Name = rating.Name,
                CusId = rating.CusId,
                VendorId = rating.VendorId,
                Comment = rating.Comment,
                RatingNo = rating.RatingNo
                // Add any other properties you might have in the Rating class
            });

            return ratingList;
        }

        //Update Rating using rating Id
        public async Task<bool> UpdateRatingById(ObjectId objectId, Rating rating)
        {
            var filter = Builders<Rating>.Filter.Eq(x => x.Id, objectId);
            var update = Builders<Rating>.Update
                .Set(x => x.Comment, rating.Comment);

            var result = await _ratings.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }
    }
}
