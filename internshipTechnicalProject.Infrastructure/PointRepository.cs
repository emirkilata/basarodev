using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Domain.Point;
using internshipTechnicalProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Infrastructure
{
    public class PointRepository : IPointRepository
    {
        private readonly AppDbContext _context;
        public PointRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Point> AddAsync(Point point)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync();
            return point;
        }

        public async Task<List<Point>> GetAllAsync()
        {
            return await _context.Points.ToListAsync();
        }

        public async Task<Point?> GetByIdAsync(int id)
        {
            return await _context.Points.FindAsync(id);
        }

        public async Task<Point?> UpdateAsync(Point point)
        {
            var existing = await _context.Points.FindAsync(point.Id);
            if (existing == null) return null;
            existing.X = point.X;
            existing.Y = point.Y;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point == null) return false;
            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 