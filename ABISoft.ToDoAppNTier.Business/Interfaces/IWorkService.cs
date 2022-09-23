using ABISoft.ToDoAppNTier.Common.ResponseObjects;
using ABISoft.ToDoAppNTier.Dtos.WorkDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.Business.Interfaces
{
    public interface IWorkService
    {
        Task<IResponse<List<WorkShowDto>>> GetAllAsync();
        Task<IResponse<WorkCreateDto>> AddAsync(WorkCreateDto dto);
        Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IResponse> Remove(int id);
    }
}
