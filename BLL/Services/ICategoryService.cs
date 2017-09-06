using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int? id);
        Task AddAsync(CategoryDTO category_dto);
        Task Update(CategoryDTO category_dto);
        Task DeleteAsync(int id);
        void Dispose();
    }
}
