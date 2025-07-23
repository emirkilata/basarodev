using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface IIUnitOfWork
    {
        IPointRepository PointRepository { get; }
        Task<int> SaveChangesAsync();
    }
} 