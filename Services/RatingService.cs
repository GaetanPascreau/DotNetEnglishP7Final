using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain;
using WebApi3.Repositories;
using WebApi3.Validators;

namespace WebApi3.Services
{
    public class RatingService : IRatingRepository
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        /// <summary>
        /// Method that calls the GetAllRatingss() method from RatingRepository 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Rating>> GetAllRatings()
        {
            var rating = await _ratingRepository.GetAllRatings();
            return rating;
        }

        /// <summary>
        /// Method that calls the GetSingleRating() method from RatingRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Rating> GetSingleRating(int id)
        {
            if (id == 0)
            {
                return null;
            }
            var rating = await _ratingRepository.GetSingleRating(id);
            return rating;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the CreateRating() method from RatingRepository
        /// </summary>
        /// <param name="ratingToCreate"></param>
        public Task<List<Rating>> CreateRating(Rating ratingToCreate)
        {
            var validator = new RatingValidator();
            var result = validator.Validate(ratingToCreate);

            if (!result.IsValid)
            {
                return null;
            }

            // If the new Rating is validated, save it in the database
            var ratingsAfterAddition = _ratingRepository.CreateRating(ratingToCreate);
            // Return the updated list of CurvePoints 
            return ratingsAfterAddition;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the UpdateRating() method from RatingRepository
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RatingtoUpdate"></param>
        /// <returns></returns>
        public Task<Rating> UpdateRating(int id, Rating RatingtoUpdate)
        {
            var validator = new RatingValidator();
            var result = validator.Validate(RatingtoUpdate);

            if (!result.IsValid || id == 0 || RatingtoUpdate == null)
            {
                return null;
            }

            var RatingAfterAddition = _ratingRepository.UpdateRating(id, RatingtoUpdate);
            return RatingAfterAddition;
        }

        /// <summary>
        /// Method that calls the DeleteRating() method from RatingRepository 
        /// </summary>
        /// <returns></returns>
        public Task<List<Rating>> DeleteRating(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var ratingsAfterDeletion = _ratingRepository.DeleteRating(id);
            return ratingsAfterDeletion;
        }
    }
}
