using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Domain.Point;
using internshipTechnicalProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Infrastructure
{
    public class PointRepository : internshipTechnicalProject.Application.PointerService.IPointRepository
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
        public async Task<Point> GetByIdAsync(int id)
        {
            return await _context.Points.FindAsync(id);
        }
        public async Task<Point> UpdateAsync(Point point)
        {
            _context.Points.Update(point);
            await _context.SaveChangesAsync();
            return point;
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