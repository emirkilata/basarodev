using internshipTechnicalProject.Application.PointerService;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Infrastructure
{
    public class UnitOfWork : IIUnitOfWork
    {
        private readonly AppDbContext _context;
        private IPointRepository? _pointRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IPointRepository PointRepository => _pointRepository ??= new PointRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 