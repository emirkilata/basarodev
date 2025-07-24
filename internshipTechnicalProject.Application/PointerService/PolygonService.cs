using internshipTechnicalProject.Application.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainPolygon = internshipTechnicalProject.Domain.Polygon;

namespace internshipTechnicalProject.Application.PointerService
{
    public class PolygonService : IPolygonService
    {
        private readonly IIUnitOfWork _unitOfWork;
        public PolygonService(IIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<DomainPolygon>> CreateAsync(PolygonDto dto)
        {
            var polygon = new DomainPolygon { Name = dto.Name, Geometry = dto.GeoJson };
            var created = await _unitOfWork.PolygonRepository.AddAsync(polygon);
            return Response<DomainPolygon>.SuccessResponse(created, "Polygon created successfully.");
        }
        public async Task<Response<List<DomainPolygon>>> GetAllAsync()
        {
            var polygons = await _unitOfWork.PolygonRepository.GetAllAsync();
            return Response<List<DomainPolygon>>.SuccessResponse(polygons, "All polygons retrieved.");
        }
        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var deleted = await _unitOfWork.PolygonRepository.DeleteAsync(id);
            if (!deleted)
                return Response<bool>.FailResponse("Polygon not found.");
            return Response<bool>.SuccessResponse(true, "Polygon deleted.");
        }
    }
} 