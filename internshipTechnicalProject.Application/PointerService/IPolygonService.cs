using internshipTechnicalProject.Application.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainPolygon = internshipTechnicalProject.Domain.Polygon;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface IPolygonService
    {
        Task<Response<DomainPolygon>> CreateAsync(PolygonDto dto);
        Task<Response<List<DomainPolygon>>> GetAllAsync();
        Task<Response<bool>> DeleteAsync(int id);
    }
} 