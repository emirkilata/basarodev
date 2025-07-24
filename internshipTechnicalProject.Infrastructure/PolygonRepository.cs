using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Domain;
using internshipTechnicalProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Infrastructure
{
    public class PolygonRepository : internshipTechnicalProject.Application.PointerService.IPolygonRepository
    {
        private readonly AppDbContext _context;
        public PolygonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Polygon> AddAsync(Polygon polygon)
        {
            _context.Polygons.Add(polygon);
            await _context.SaveChangesAsync();
            return polygon;
        }
        public async Task<List<Polygon>> GetAllAsync()
        {
            return await _context.Polygons.ToListAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var polygon = await _context.Polygons.FindAsync(id);
            if (polygon == null) return false;
            _context.Polygons.Remove(polygon);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 