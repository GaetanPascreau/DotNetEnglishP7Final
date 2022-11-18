using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;
using WebApi3.Validators;

namespace WebApi3.Services
{
    public class CurvePointService  : ICurvePointRepository
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointService(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        /// <summary>
        /// Method that calls the GetAllCurvePoints() method from CurvePointRepository 
        /// </summary>
        /// <returns></returns>
        public async Task<List<CurvePointDTO>> GetAllCurvePoints()
        {
            var curvePointsDTO = await _curvePointRepository.GetAllCurvePoints();
            return curvePointsDTO;
        }

        /// <summary>
        /// Method that calls the GetSingleCurvePoint() method from CurvePointRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CurvePointDTO> GetSingleCurvePoint(int id)
        {
            if (id == 0)
            {
                return null;
            }
            var curvePointDTO = await _curvePointRepository.GetSingleCurvePoint(id);
            return curvePointDTO;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the CreateCurvePoint() method from CurvePointRepository
        /// </summary>
        /// <param name="curvePointDTO"></param>
        public Task<List<CurvePointDTO>> CreateCurvePoint(CurvePointDTO curvePointDTOtoCreate)
        {
            var validator = new CurvePointValidator();
            var result = validator.Validate(curvePointDTOtoCreate);

            if (!result.IsValid)
            {
                return null;
            }

            // If the new CurvePoint is validated, save it in the database
            var curvePointDTOsAfterAddition = _curvePointRepository.CreateCurvePoint(curvePointDTOtoCreate);
            // Return the updated list of CurvePoints 
            return curvePointDTOsAfterAddition;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the UpdateCurvePoint() method from CurvePointRepository
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="curvePointDTOtoUpdate"></param>
        /// <returns></returns>
        public Task<CurvePointDTO> UpdateCurvePoint(int id, CurvePointDTO curvePointDTOtoUpdate)
        {
            var validator = new CurvePointValidator();
            var result = validator.Validate(curvePointDTOtoUpdate);

            if (!result.IsValid || id == 0 || curvePointDTOtoUpdate == null)
            {
                return null;
            }

            var curvePointDTOsAfterAddition = _curvePointRepository.UpdateCurvePoint(id, curvePointDTOtoUpdate);
            return curvePointDTOsAfterAddition;
        }

        /// <summary>
        /// Method that calls the DeleteCurvePoint() method from CurvePointRepository 
        /// </summary>
        /// <returns></returns>
        public Task<List<CurvePointDTO>> DeleteCurvePoint(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var curvePointDTOsAfterDeletion = _curvePointRepository.DeleteCurvePoint(id);
            return curvePointDTOsAfterDeletion;
        }
    }
}
