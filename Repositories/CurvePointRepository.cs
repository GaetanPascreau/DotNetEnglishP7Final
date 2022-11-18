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
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _context;
        public CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that GETs the list of CurvePoint from the database,
        /// then converts it into a list of CurvePointDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <returns></returns>
        public async Task<List<CurvePointDTO>> GetAllCurvePoints()
        {
            var curvePoints = await _context.CurvePoint
                .Select(x => CurvePointToDTO(x))
                .ToListAsync();

            return curvePoints;
        }

        /// <summary>
        /// Method that GETs a specific CurvePoint by its Id from the database,
        /// then converts it into a list of CurvePointDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CurvePointDTO> GetSingleCurvePoint(int id)
        {
            var curvePoint = await _context.CurvePoint.SingleOrDefaultAsync(c => c.Id == id);
            if (curvePoint is null)
            {
                return null;
            }
            return CurvePointToDTO(curvePoint);

        }

        /// <summary>
        /// Method that CREATEs a curvePoint and save it to the database
        /// </summary>
        /// <param name="curvePointDTO"></param>
        /// <returns></returns>
        public async Task<List<CurvePointDTO>> CreateCurvePoint(CurvePointDTO curvePointDTO)
        {
            // Create new CurvePoint 
            var curvePoint = new CurvePoint
            {
                Id = curvePointDTO.Id,
                CurveId = curvePointDTO.CurveId,
                Term = curvePointDTO.Term,
                Value = curvePointDTO.Value,
                AsOfDate = DateTime.Now,
                CreationDate = DateTime.Now
            };

            // Save new CurvePoint in the database
            _context.CurvePoint.Add(curvePoint);
            await _context.SaveChangesAsync();

            // return the entire list of CurvePoints with the newly added CurvePoint
           var curvePointsAfterAddition = GetAllCurvePoints();
            return await curvePointsAfterAddition;
        }

        /// <summary>
        /// Method that UPDATEs a curvePoint with a given Id in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="curvePointDTO"></param>
        /// <returns></returns>
        public async Task<CurvePointDTO> UpdateCurvePoint(int id, CurvePointDTO curvePointDTO)
        {
            var curvePointToUpdate =  _context.CurvePoint.Find(id);
            if (curvePointToUpdate is null)
            {
                return null;
            }

            curvePointToUpdate.CurveId = curvePointDTO.CurveId;
            curvePointToUpdate.Term = curvePointDTO.Term;
            curvePointToUpdate.Value = curvePointDTO.Value;
            curvePointToUpdate.AsOfDate = curvePointDTO.AsOfDate;
            curvePointToUpdate.CreationDate = curvePointDTO.CreationDate;

            await _context.SaveChangesAsync();

            // Return the updated CurvePoint
            var result = CurvePointToDTO(curvePointToUpdate);
            return result;
        }

        /// <summary>
        /// Method to DELETE a curvePoint with a given Id from the database
        /// </summary>
        /// <param name="id"></param>
        public async Task<List<CurvePointDTO>> DeleteCurvePoint(int id)
        {
            CurvePoint curvePointToDelete = _context.CurvePoint.Find(id);
            if (curvePointToDelete is null)
            {
                return null;
            }
            _context.CurvePoint.Remove(curvePointToDelete);
            _context.SaveChanges();

            // Return the entire list of CurvePoints without the newly removed CurvePoint
            var curvePointsAfterDeletion = GetAllCurvePoints();
            return await curvePointsAfterDeletion;
        }

        /// <summary>
        /// Method that converts a Curvepoint into a CurvePointDTO
        /// </summary>
        /// <param name="curvePoint"></param>
        /// <returns></returns>
        private static CurvePointDTO CurvePointToDTO(CurvePoint curvePoint) =>
        new CurvePointDTO
        {
            Id = curvePoint.Id,
            CurveId = curvePoint.CurveId,
            Term = curvePoint.Term,
            Value = curvePoint.Value,
            AsOfDate = curvePoint.AsOfDate,
            CreationDate = curvePoint.CreationDate
        };
    }
}
