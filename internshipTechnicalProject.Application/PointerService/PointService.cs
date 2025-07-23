using internshipTechnicalProject.Application.Common;
using internshipTechnicalProject.Domain.Point;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public class PointService : IPointService
    {
        private readonly IIUnitOfWork _unitOfWork;

        public PointService(IIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Point>> CreateAsync(Point point)
        {
            var created = await _unitOfWork.PointRepository.AddAsync(point);
            return Response<Point>.SuccessResponse(created, "Point created successfully.");
        }

        public async Task<Response<List<Point>>> GetAllAsync()
        {
            var points = await _unitOfWork.PointRepository.GetAllAsync();
            return Response<List<Point>>.SuccessResponse(points, "All points retrieved.");
        }

        public async Task<Response<Point>> GetByIdAsync(int id)
        {
            var point = await _unitOfWork.PointRepository.GetByIdAsync(id);
            if (point == null)
                return Response<Point>.FailResponse("Point not found.");

            return Response<Point>.SuccessResponse(point);
        }

        public async Task<Response<Point>> UpdateAsync(Point updatedPoint)
        {
            var point = await _unitOfWork.PointRepository.UpdateAsync(updatedPoint);
            if (point == null)
                return Response<Point>.FailResponse("Point not found.");

            return Response<Point>.SuccessResponse(point, "Point updated.");
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var deleted = await _unitOfWork.PointRepository.DeleteAsync(id);
            if (!deleted)
                return Response<bool>.FailResponse("Point not found.");

            return Response<bool>.SuccessResponse(true, "Point deleted.");
        }
    }
}
