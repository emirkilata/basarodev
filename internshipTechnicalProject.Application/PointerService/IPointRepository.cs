using internshipTechnicalProject.Domain.Point;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface IPointRepository
    {
        Task<Point> AddAsync(Point point);
        Task<List<Point>> GetAllAsync();
        Task<Point?> GetByIdAsync(int id);
        Task<Point?> UpdateAsync(Point point);
        Task<bool> DeleteAsync(int id);
    }
} 