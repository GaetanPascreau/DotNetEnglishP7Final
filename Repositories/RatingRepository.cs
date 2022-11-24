using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi3.Domain;

namespace WebApi3.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _context;
        public RatingRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that GETs the list of Ratings from the database,
        /// then returning it (to the Service)
        /// </summary>
        /// <returns></returns>
        public async Task<List<Rating>> GetAllRatings()
        {
            var ratings = await _context.Rating
                .ToListAsync();

            return ratings;
        }

        /// <summary>
        /// Method that GETs a specific Rating by its Id from the database,
        /// then returning it (to the Service)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Rating> GetSingleRating(int id)
        {
            var rating = await _context.Rating.SingleOrDefaultAsync(r => r.Id == id);
            if (rating is null)
            {
                return null;
            }
            return rating;

        }

        /// <summary>
        /// Method that CREATEs a Rating and save it to the database
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public async Task<List<Rating>> CreateRating(Rating rating)
        {
            // Create new Rating 
            var newRating = new Rating
            {
                Id = rating.Id,
                MoodysRating = rating.MoodysRating,
                SandPRating = rating.SandPRating,
                FitchRating = rating.FitchRating,
                OrderNumber = rating.OrderNumber
            };

            // Save new Rating in the database
            _context.Rating.Add(newRating);
            await _context.SaveChangesAsync();

            // return the entire list of Ratings with the newly added Rating
            var RatingsAfterAddition = GetAllRatings();
            return await RatingsAfterAddition;
        }

        /// <summary>
        /// Method that UPDATEs a Rating with a given Id in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        public async Task<Rating> UpdateRating(int id, Rating rating)
        {
            var ratingToUpdate = _context.Rating.Find(id);
            if (ratingToUpdate is null)
            {
                return null;
            }

            ratingToUpdate.MoodysRating = rating.MoodysRating;
            ratingToUpdate.SandPRating = rating.SandPRating;
            ratingToUpdate.FitchRating = rating.FitchRating;
            ratingToUpdate.OrderNumber = rating.OrderNumber;

            await _context.SaveChangesAsync();

            // Return the updated Rating
            return ratingToUpdate;
        }

        /// <summary>
        /// Method to DELETE a Rating with a given Id from the database
        /// </summary>
        /// <param name="id"></param>
        public async Task<List<Rating>> DeleteRating(int id)
        {
            Rating ratingToDelete = _context.Rating.Find(id);
            if (ratingToDelete is null)
            {
                return null;
            }
            _context.Rating.Remove(ratingToDelete);
            _context.SaveChanges();

            // Return the entire list of Ratings without the newly removed Rating
            var ratingsAfterDeletion = GetAllRatings();
            return await ratingsAfterDeletion;
        }
    }
}
