using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;
using WebApi3.Repositories;

namespace WebApi3.Services
{
    public class CurvePointService : ICurvePointService
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointService(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        /// <summary>
        /// Method that calls the GetCurvePoint() method from CurvePointRepository 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurvePointDTO>> GetCurvePoint()
        {
            var curvePointsDTO = await _curvePointRepository.GetCurvePoint();
            return curvePointsDTO;
        }

        /// <summary>
        /// Method that calls the GetCurvePointById() method from CurvePointRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CurvePointDTO> GetCurvePointById(int id)
        {
            //ADD validation here => check that the id exists ? or use annotations?
            if (id == 0)
            {
                //use modelErrors like in P3AddNewFunctionalityDotNetCore/ProductService.cs
            }
            var curvePointDTO = await _curvePointRepository.GetCurvePointById(id);
            return curvePointDTO;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the CreateCurvePoint() method from CurvePointRepository
        /// </summary>
        /// <param name="curvePointDTO"></param>
        public bool CreateCurvePoint(CurvePointDTO curvePointDTOtoCreate)
        {
            // Validation logic
            //if (!ValidateCurvePoint(curvePointDTOtoCreate))
            //{
            //    return false;
            //}
            // Database logic
            try
            {
                _curvePointRepository.CreateCurvePoint(curvePointDTOtoCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Methods that checks if passed data is valid, then calls the UpdateCurvePoint() method from CurvePointRepository
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="curvePointDTOtoUpdate"></param>
        /// <returns></returns>
        public bool UpdateCurvePoint(int Id, CurvePointDTO curvePointDTOtoUpdate)
        {
            // Validation logic
            //if (!ValidateCurvePoint(curvePointDTOtoUpdate))
            //{
            //    return false;
            //}
            // Database logic
            try
            {
                _curvePointRepository.UpdateCurvePoint(Id, curvePointDTOtoUpdate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method that calls the DeleteCurvePoint() method from CurvePointRepository 
        /// </summary>
        /// <returns></returns>
        public void DeleteCurvePoint(int Id)
        {
            _curvePointRepository.DeleteCurvePoint(Id);
        }
    }

    public interface ICurvePointService
    {
        Task<IEnumerable<CurvePointDTO>> GetCurvePoint();
        Task<CurvePointDTO> GetCurvePointById(int id);
        bool CreateCurvePoint(CurvePointDTO curvePointDTOtoCreate);
        bool UpdateCurvePoint(int Id, CurvePointDTO curvePointDTOtoUpdate);
        void DeleteCurvePoint(int Id);
        //bool ValidateCurvePoint(CurvePointDTO curvePointToValidate);
    }
}
