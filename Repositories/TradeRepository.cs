using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi3.Domain;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public class TradeRepository :ITradeRepository
    {
        private readonly LocalDbContext _context;
        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that GETs the list of Trades from the database,
        /// then converts it into a list of TradeDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <returns></returns>
        public async Task<List<TradeDTO>> GetAllTrades()
        {
            var trades = await _context.Trade
                .Select(x => TradeToDTO(x))
                .ToListAsync();

            return trades;
        }

        /// <summary>
        /// Method that GETs a specific Trade by its Id from the database,
        /// then converts it into a list of TradeDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TradeDTO> GetSingleTrade(int id)
        {
            var trade = await _context.Trade.SingleOrDefaultAsync(t => t.TradeId == id);
            if (trade is null)
            {
                return null;
            }
            return TradeToDTO(trade);

        }

        /// <summary>
        /// Method that CREATEs a Trade and save it to the database
        /// </summary>
        /// <param name="tradeDTO"></param>
        /// <returns></returns>
        public async Task<List<TradeDTO>> CreateTrade(TradeDTO tradeDTO)
        {
            // Create new Trade 
            var trade = new Trade
            {
                TradeId = tradeDTO.TradeId,
                Account = tradeDTO.Account,
                Type = tradeDTO.Type,
                BuyQuantity = tradeDTO.BuyQuantity,
                SellQuantity = tradeDTO.SellQuantity,
                BuyPrice = tradeDTO.BuyPrice,
                SellPrice = tradeDTO.SellPrice,
                TradeDate = DateTime.Now,
                CreationDate = DateTime.Now,
                RevisionDate = DateTime.Now,
            };

            // Save new Trade in the database
            _context.Trade.Add(trade);
            await _context.SaveChangesAsync();

            // return the entire list of Trades with the newly added Trade
            var tradesAfterAddition = GetAllTrades();
            return await tradesAfterAddition;
        }

        /// <summary>
        /// Method that UPDATEs a Trade with a given Id in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tradeDTO"></param>
        /// <returns></returns>
        public async Task<TradeDTO> UpdateTrade(int id, TradeDTO tradeDTO)
        {
            var TradeToUpdate = _context.Trade.Find(id);
            if (TradeToUpdate is null)
            {
                return null;
            }

            TradeToUpdate.Account = tradeDTO.Account;
            TradeToUpdate.Type = tradeDTO.Type;
            TradeToUpdate.BuyQuantity = tradeDTO.BuyQuantity;
            TradeToUpdate.SellQuantity = tradeDTO.SellQuantity;
            TradeToUpdate.BuyPrice = tradeDTO.BuyPrice;
            TradeToUpdate.SellPrice = tradeDTO.SellPrice;
            TradeToUpdate.TradeDate = tradeDTO.TradeDate;
            TradeToUpdate.CreationDate = tradeDTO.CreationDate;
            TradeToUpdate.RevisionDate = tradeDTO.RevisionDate;

            await _context.SaveChangesAsync();

            // Return the updated Trade
            var result = TradeToDTO(TradeToUpdate);
            return result;
        }

        /// <summary>
        /// Method to DELETE a Trade with a given Id from the database
        /// </summary>
        /// <param name="id"></param>
        public async Task<List<TradeDTO>> DeleteTrade(int id)
        {
            Trade tradeToDelete = _context.Trade.Find(id);
            if (tradeToDelete is null)
            {
                return null;
            }
            _context.Trade.Remove(tradeToDelete);
            _context.SaveChanges();

            // Return the entire list of Trades without the newly removed Trade
            var TradesAfterDeletion = GetAllTrades();
            return await TradesAfterDeletion;
        }

        /// <summary>
        /// Method that converts a Trade into a TradeDTO
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        private static TradeDTO TradeToDTO(Trade trade) =>
        new TradeDTO
        {
            TradeId = trade.TradeId,
            Account = trade.Account,
            Type = trade.Type,
            BuyQuantity = trade.BuyQuantity,
            SellQuantity = trade.SellQuantity,
            BuyPrice = trade.BuyPrice,
            SellPrice = trade.SellPrice,
            TradeDate = trade.TradeDate,
            CreationDate = trade.CreationDate,
            RevisionDate = trade.RevisionDate
        };
    }
}
