using internshipTechnicalProject.Application.Common;
using internshipTechnicalProject.Domain.Point;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface IPointService
    {
        Task<Response<Point>> CreateAsync(Point point);
        Task<Response<List<Point>>> GetAllAsync();
        Task<Response<Point>> GetByIdAsync(int id);
        Task<Response<Point>> UpdateAsync(Point point);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
