using EADEcommerceBE.Models;
using EADEcommerceBE.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EADEcommerceBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rating rating)
        {
            var id = await _ratingRepository.CreateRating(rating);
            return Ok(new { Message = "Rating created successfully", RatingId = id.ToString() });
        }

        [HttpGet("GetRatingsByCusId/{cusId}")]
        public async Task<IActionResult> GetRatingsByCusId(string cusId)
        {
            var ratings = await _ratingRepository.GetRatingsByCusId(cusId);
            // Convert ObjectId to string for Id field in each rating
            var ratingList = ratings.Select(rating => new
            {
                ratingId = rating.Id.ToString(),  // Convert ObjectId to string
                rating.Name,
                rating.CusId,
                rating.VendorId,
                rating.Comment,
                rating.RatingNo
            });

            return Ok(new
            {
                Message = "Ratings retrieved successfully",
                Ratings = ratingList
            });
        }

        [HttpGet("GetRatingsByVendorId/{vendorId}")]
        public async Task<IActionResult> GetRatingsByVendorId(string vendorId)
        {
            var ratings = await _ratingRepository.GetRatingsByVendorId(vendorId);

            // Convert ObjectId to string for Id field in each rating
            var ratingList = ratings.Select(rating => new
            {
                ratingId = rating.Id.ToString(),  // Convert ObjectId to string
                rating.Name,
                rating.CusId,
                rating.VendorId,
                rating.Comment,
                rating.RatingNo
            });

            return new JsonResult(ratingList);
        }

        [HttpPut("UpdateRatingById/{id}")]
        public async Task<IActionResult> UpdateRatingById(string id, [FromBody] Rating rating)
        {
            if (!ObjectId.TryParse(id, out var ratingObjectId))
                return BadRequest("Invalid RatingId");

            var result = await _ratingRepository.UpdateRatingById(ratingObjectId, rating);
            if (!result)
                return NotFound("Rating not found or no changes made");

            // Check if CusId is null
            if (rating.CusId == null)
                return BadRequest("CusId cannot be null");

            // Retrieve the updated rating
            var updatedRating = await _ratingRepository.GetRatingsByCusId(rating.CusId);
            var ratingDetails = updatedRating.FirstOrDefault(r => r.Id == ratingObjectId);

            if (ratingDetails == null)
                return NotFound("Updated rating not found");

            return Ok(new
            {
                Message = "Rating updated successfully",
                Rating = new
                {
                    Id = ratingDetails.Id.ToString(),
                    Name = ratingDetails.Name,
                    CusId = ratingDetails.CusId,
                    VendorId = ratingDetails.VendorId,
                    Comment = ratingDetails.Comment,
                    RatingNo = ratingDetails.RatingNo
                }
            });
        }

        [HttpDelete("DeleteRatingById/{id}")]
        public async Task<IActionResult> DeleteRatingById(string id)
        {
            if (!ObjectId.TryParse(id, out var ratingObjectId))
                return BadRequest("Invalid RatingId");

            var result = await _ratingRepository.DeleteRatingById(ratingObjectId);
            if (!result)
                return NotFound("Rating not found");

            return Ok("Rating deleted successfully");
        }
    }
}
