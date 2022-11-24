using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public interface IBidListRepository
    {
        Task<List<BidListDTO>> GetAllBidLists();
        Task<BidListDTO> GetSingleBidList(int id);
        Task<List<BidListDTO>> CreateBidList(BidListDTO bidListDTO);
        Task<BidListDTO> UpdateBidList(int id, BidListDTO bidListDTO);
        Task<List<BidListDTO>> DeleteBidList(int id);
    }
}
