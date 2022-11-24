using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain;

namespace WebApi3.Repositories
{
    public interface IRatingRepository
    {
        Task<List<Rating>> GetAllRatings();
        Task<Rating> GetSingleRating(int id);
        Task<List<Rating>> CreateRating(Rating rating);
        Task<Rating> UpdateRating(int id, Rating rating);
        Task<List<Rating>> DeleteRating(int id);
    }
}
