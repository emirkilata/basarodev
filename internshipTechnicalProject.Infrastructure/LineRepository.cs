using internshipTechnicalProject.Application.PointerService;
using internshipTechnicalProject.Domain;
using internshipTechnicalProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Infrastructure
{
    public class LineRepository : internshipTechnicalProject.Application.PointerService.ILineRepository
    {
        private readonly AppDbContext _context;
        public LineRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Line> AddAsync(Line line)
        {
            _context.Lines.Add(line);
            await _context.SaveChangesAsync();
            return line;
        }
        public async Task<List<Line>> GetAllAsync()
        {
            return await _context.Lines.ToListAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var line = await _context.Lines.FindAsync(id);
            if (line == null) return false;
            _context.Lines.Remove(line);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 