using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public interface ITradeRepository
    {
        Task<List<TradeDTO>> GetAllTrades();
        Task<TradeDTO> GetSingleTrade(int id);
        Task<List<TradeDTO>> CreateTrade(TradeDTO tradeDTO);
        Task<TradeDTO> UpdateTrade(int id, TradeDTO tradeDTO);
        Task<List<TradeDTO>> DeleteTrade(int id);
    }
}
