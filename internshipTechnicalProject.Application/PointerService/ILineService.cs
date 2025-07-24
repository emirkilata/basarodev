using internshipTechnicalProject.Application.Common;
using internshipTechnicalProject.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internshipTechnicalProject.Application.PointerService
{
    public interface ILineService
    {
        Task<Response<Line>> CreateAsync(LineDto dto);
        Task<Response<List<Line>>> GetAllAsync();
        Task<Response<bool>> DeleteAsync(int id);
    }
} 