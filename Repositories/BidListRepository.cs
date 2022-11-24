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
    public class BidListRepository : IBidListRepository
    {
        private readonly LocalDbContext _context;
        public BidListRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that GETs the list of BidList from the database,
        /// then converts it into a list of BidListDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <returns></returns>
        public async Task<List<BidListDTO>> GetAllBidLists()
        {
            var bidLists = await _context.BidList
                .Select(x => BidListToDTO(x))
                .ToListAsync();

            return bidLists;
        }

        /// <summary>
        /// Method that GETs a specific BidList by its Id from the database,
        /// then converts it into a list of BidListDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BidListDTO> GetSingleBidList(int id)
        {
            var bidList = await _context.BidList.SingleOrDefaultAsync(b => b.BidListId == id);
            if (bidList is null)
            {
                return null;
            }
            return BidListToDTO(bidList);

        }

        /// <summary>
        /// Method that CREATEs a BidList and save it to the database
        /// </summary>
        /// <param name="biListDTO"></param>
        /// <returns></returns>
        public async Task<List<BidListDTO>> CreateBidList(BidListDTO bidListDTO)
        {
            // Create new BidList 
            var bidList = new BidList
            {
                BidListId = bidListDTO.BidListId,
                Account = bidListDTO.Account,
                Type = bidListDTO.Type,
                BidQuantity = bidListDTO.BidQuantity,
                BidListDate = DateTime.Now,
                CreationDate = DateTime.Now,
                RevisionDate = DateTime.Now
            };

            // Save new BidList in the database
            _context.BidList.Add(bidList);
            await _context.SaveChangesAsync();

            // return the entire list of BidList with the newly added BidList
            var BidListsAfterAddition = GetAllBidLists();
            return await BidListsAfterAddition;
        }

        /// <summary>
        /// Method that UPDATEs a BidList with a given Id in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="bidListDTO"></param>
        /// <returns></returns>
        public async Task<BidListDTO> UpdateBidList(int id, BidListDTO bidListDTO)
        {
            var bidListToUpdate = _context.BidList.Find(id);
            if (bidListToUpdate is null)
            {
                return null;
            }

            bidListToUpdate.Account = bidListDTO.Account;
            bidListToUpdate.Type = bidListDTO.Type;
            bidListToUpdate.BidQuantity = bidListDTO.BidQuantity;
            bidListToUpdate.BidListDate = bidListDTO.BidListDate;
            bidListToUpdate.CreationDate = bidListDTO.CreationDate;
            bidListToUpdate.RevisionDate = bidListDTO.RevisionDate;


            await _context.SaveChangesAsync();

            // Return the updated CurvePoint
            var result = BidListToDTO(bidListToUpdate);
            return result;
        }

        /// <summary>
        /// Method to DELETE a BidList with a given Id from the database
        /// </summary>
        /// <param name="id"></param>
        public async Task<List<BidListDTO>> DeleteBidList(int id)
        {
            BidList bidListToDelete = _context.BidList.Find(id);
            if (bidListToDelete is null)
            {
                return null;
            }
            _context.BidList.Remove(bidListToDelete);
            _context.SaveChanges();

            // Return the entire list of BidList without the newly removed BidList
            var bidListsAfterDeletion = GetAllBidLists();
            return await bidListsAfterDeletion;
        }

        /// <summary>
        /// Method that converts a BidList into a BidListDTO
        /// </summary>
        /// <param name="bidList"></param>
        /// <returns></returns>
        private static BidListDTO BidListToDTO(BidList bidList) =>
        new BidListDTO
        {
            BidListId = bidList.BidListId,
            Account = bidList.Account,
            Type = bidList.Type,
            BidQuantity = bidList.BidQuantity,
            BidListDate = bidList.BidListDate,
            CreationDate = bidList.CreationDate,
            RevisionDate = bidList.RevisionDate
        };
    }
}
