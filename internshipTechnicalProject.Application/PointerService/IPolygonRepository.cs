using System.Collections.Generic;
using System.Threading.Tasks;
using DomainPolygon = internshipTechnicalProject.Domain.Polygon;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface IPolygonRepository
    {
        Task<DomainPolygon> AddAsync(DomainPolygon polygon);
        Task<List<DomainPolygon>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
} 