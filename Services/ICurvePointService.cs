using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;

namespace WebApi3.Services
{
    public interface ICurvePointService
    {
        Task<List<CurvePointDTO>> GetAllCurvePoints();
        Task<CurvePointDTO> GetSingleCurvePoint(int id);
        Task<List<CurvePointDTO>> CreateCurvePoint(CurvePointDTO curvePointDTOtoCreate);
        Task<List<CurvePointDTO>> UpdateCurvePoint(int id, CurvePointDTO curvePointDTOtoUpdate);
        Task<List<CurvePointDTO>> DeleteCurvePoint(int id);
    }
}
