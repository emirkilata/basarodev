using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface IIUnitOfWork
    {
        IPointRepository PointRepository { get; }
        ILineRepository LineRepository { get; }
        IPolygonRepository PolygonRepository { get; }
        Task<int> SaveChangesAsync();
    }
} 