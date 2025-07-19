using internshipTechnicalProject.Application.Common;
using internshipTechnicalProject.Domain.Point;
using internshipTechnicalProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace internshipTechnicalProject.Application.PointerService
{
    public class PointService : IPointService
    {
        private readonly AppDbContext _context;

        public PointService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Point>> CreateAsync(Point point)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync();
            return Response<Point>.SuccessResponse(point, "Point created successfully.");
        }

        public async Task<Response<List<Point>>> GetAllAsync()
        {
            var points = await _context.Points.ToListAsync();
            return Response<List<Point>>.SuccessResponse(points, "All points retrieved.");
        }

        public async Task<Response<Point>> GetByIdAsync(int id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point == null)
                return Response<Point>.FailResponse("Point not found.");

            return Response<Point>.SuccessResponse(point);
        }

        public async Task<Response<Point>> UpdateAsync(Point updatedPoint)
        {
            var point = await _context.Points.FindAsync(updatedPoint.Id);
            if (point == null)
                return Response<Point>.FailResponse("Point not found.");

            point.X = updatedPoint.X;
            point.Y = updatedPoint.Y;

            await _context.SaveChangesAsync();
            return Response<Point>.SuccessResponse(point, "Point updated.");
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point == null)
                return Response<bool>.FailResponse("Point not found.");

            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
            return Response<bool>.SuccessResponse(true, "Point deleted.");
        }
    }
}
