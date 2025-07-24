using internshipTechnicalProject.Application.Common;
using internshipTechnicalProject.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public class LineService : ILineService
    {
        private readonly IIUnitOfWork _unitOfWork;
        public LineService(IIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<Line>> CreateAsync(LineDto dto)
        {
            var line = new Line { Name = dto.Name, Geometry = dto.GeoJson };
            var created = await _unitOfWork.LineRepository.AddAsync(line);
            return Response<Line>.SuccessResponse(created, "Line created successfully.");
        }
        public async Task<Response<List<Line>>> GetAllAsync()
        {
            var lines = await _unitOfWork.LineRepository.GetAllAsync();
            return Response<List<Line>>.SuccessResponse(lines, "All lines retrieved.");
        }
        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var deleted = await _unitOfWork.LineRepository.DeleteAsync(id);
            if (!deleted)
                return Response<bool>.FailResponse("Line not found.");
            return Response<bool>.SuccessResponse(true, "Line deleted.");
        }
    }
} 