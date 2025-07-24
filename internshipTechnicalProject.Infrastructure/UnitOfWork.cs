using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Infrastructure.Context;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Infrastructure
{
    public class UnitOfWork : internshipTechnicalProject.Application.PointerService.IIUnitOfWork
    {
        private readonly AppDbContext _context;
        private PointRepository? _pointRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public internshipTechnicalProject.Application.PointerService.IPointRepository PointRepository => _pointRepository ??= new PointRepository(_context);
        public internshipTechnicalProject.Application.PointerService.ILineRepository LineRepository => new LineRepository(_context);
        public internshipTechnicalProject.Application.PointerService.IPolygonRepository PolygonRepository => new PolygonRepository(_context);
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 