using Dot.Net.WebApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface ICurvePointRepository
    {

        Task<IReadOnlyCollection<CurvePoint>> Add(CurvePoint curvePoint);
        Task<IReadOnlyCollection<CurvePoint>> Update(int id, CurvePoint curvePoint);
        Task<IReadOnlyCollection<CurvePoint>> GetAll();
        Task<CurvePoint> Get(int id);
        Task<IReadOnlyCollection<CurvePoint>> Delete(int id);
    }
}
