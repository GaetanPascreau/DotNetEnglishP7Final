using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;
using WebApi3.Validators;

namespace WebApi3.Services
{
    public class BidListService : IBidListRepository
    {
        private readonly IBidListRepository _bidListRepository;

        public BidListService(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }

        /// <summary>
        /// Method that calls the GetAllBidLists() method from BidListRepository 
        /// </summary>
        /// <returns></returns>
        public async Task<List<BidListDTO>> GetAllBidLists()
        {
            var bidListsDTO = await _bidListRepository.GetAllBidLists();
            return bidListsDTO;
        }

        /// <summary>
        /// Method that calls the GetSingleBidList() method from BidListRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BidListDTO> GetSingleBidList(int id)
        {
            if (id == 0)
            {
                return null;
            }
            var bidListDTO = await _bidListRepository.GetSingleBidList(id);
            return bidListDTO;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the CreateBidList() method from BidListRepository
        /// </summary>
        /// <param name="bidListDTO"></param>
        public Task<List<BidListDTO>> CreateBidList(BidListDTO bidListDTOtoCreate)
        {
            var validator = new BidListValidator();
            var result = validator.Validate(bidListDTOtoCreate);

            if (!result.IsValid)
            {
                return null;
            }

            // If the new BidList is validated, save it in the database
            var bidListDTOsAfterAddition = _bidListRepository.CreateBidList(bidListDTOtoCreate);
            // Return the updated list of CurvePoints 
            return bidListDTOsAfterAddition;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the UpdateBidList() method from BidListRepository
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="bidListDTOtoUpdate"></param>
        /// <returns></returns>
        public Task<BidListDTO> UpdateBidList(int id, BidListDTO bidListDTOtoUpdate)
        {
            var validator = new BidListValidator();
            var result = validator.Validate(bidListDTOtoUpdate);

            if (!result.IsValid || id == 0 || bidListDTOtoUpdate == null)
            {
                return null;
            }

            var bidListDTOsAfterAddition = _bidListRepository.UpdateBidList(id, bidListDTOtoUpdate);
            return bidListDTOsAfterAddition;
        }

        /// <summary>
        /// Method that calls the DeleteBidList() method from BidListRepository 
        /// </summary>
        /// <returns></returns>
        public Task<List<BidListDTO>> DeleteBidList(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var bidListDTOsAfterDeletion = _bidListRepository.DeleteBidList(id);
            return bidListDTOsAfterDeletion;
        }
    }
}
