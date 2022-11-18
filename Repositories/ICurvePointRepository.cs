using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public interface ICurvePointRepository
    {
        Task<List<CurvePointDTO>> GetAllCurvePoints();
        Task<CurvePointDTO> GetSingleCurvePoint(int id);
        Task<List<CurvePointDTO>> CreateCurvePoint(CurvePointDTO curvePointDTO);
        Task<CurvePointDTO> UpdateCurvePoint(int id, CurvePointDTO curvePointDTO);
        Task<List<CurvePointDTO>> DeleteCurvePoint(int id);
    }
}
