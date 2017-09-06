using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllAsync();
        Task<BookDTO> GetByIdAsync(int? id);
        Task AddAsync(BookDTO book_dto);
        Task Update(BookDTO book_dto);
        Task DeleteAsync(int id);
        void Dispose();
    }
}
