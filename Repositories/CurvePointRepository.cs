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
        private CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that GETs the list of CurvePoint from the database,
        /// then converts it into a list of CurvePointDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurvePointDTO>> GetCurvePoint()
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
        public async Task<CurvePointDTO> GetCurvePointById(int id)
        {
            var curvePoint = await _context.CurvePoint.SingleOrDefaultAsync(c => c.Id == id);

            return CurvePointToDTO(curvePoint);

        }

        /// <summary>
        /// Method that CREATEs a curvePoint and save it to the database
        /// </summary>
        /// <param name="curvePointDTO"></param>
        /// <returns></returns>
        public async Task<CurvePointDTO> CreateCurvePoint(CurvePointDTO curvePointDTO)
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

            // Save new CurvePoint to the database
            _context.CurvePoint.Add(curvePoint);
            await _context.SaveChangesAsync();

            //return the corresponding CurvePointDTO
            return curvePointDTO;
        }

        /// <summary>
        /// Method that UPDATEs a curvePoint with a given Id in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="curvePointDTO"></param>
        /// <returns></returns>
        public async Task<CurvePointDTO> UpdateCurvePoint(int Id, CurvePointDTO curvePointDTO)
        {
            _context.Entry(curvePointDTO).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return curvePointDTO;
        }

        /// <summary>
        /// Method to DELETE a curvePoint with a given Id from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCurvePoint(int Id)
        {
            CurvePoint curvePoint = _context.CurvePoint.First(c => c.Id == Id);
            if (curvePoint != null)
            {
                _context.CurvePoint.Remove(curvePoint);
                _context.SaveChanges();
            }
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

    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePointDTO>> GetCurvePoint();
        Task<CurvePointDTO> GetCurvePointById(int id);
        Task<CurvePointDTO> CreateCurvePoint(CurvePointDTO curvePointDTO);
        Task<CurvePointDTO> UpdateCurvePoint(int Id, CurvePointDTO curvePointDTO);
        void DeleteCurvePoint(int Id);
    }
}
