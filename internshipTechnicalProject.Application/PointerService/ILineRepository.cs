using internshipTechnicalProject.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface ILineRepository
    {
        Task<Line> AddAsync(Line line);
        Task<List<Line>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
} 