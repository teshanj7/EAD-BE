/*********************************************** 
    Repository Interface of Rating Mgmt
    All the methods within Rating Mgmt
    Dilshan W.A.B. - IT21343216
 **********************************************/

using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IRatingRepository
    {
        Task<ObjectId> CreateRating(Rating rating);
        Task<IEnumerable<Rating>> GetRatingsByVendorId(string vendorId);
        Task<IEnumerable<Rating>> GetRatingsByCusId(string cusId);
        Task<bool> UpdateRatingById(ObjectId objectId, Rating rating);
        Task<bool> DeleteRatingById(ObjectId objectId);
    }
}
