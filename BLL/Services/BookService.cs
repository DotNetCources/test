using AutoMapper;
using BLL.DTOs;
using BLL.Infrastructure;
using DAL.Entities;
using DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService:IBookService
    {
        IUnitOfWork Database { get; set; }

        public BookService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public async Task AddAsync(BookDTO bookDto)
        {
            Book model = new Book()
            {
                Id = bookDto.Id,
                Name = bookDto.Name,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN
            };
            await Database.Books.CreateAsync(model);
            await Database.SaveAsync();

        }
        public async Task<BookDTO> GetByIdAsync(int? id)
        {
            try
            {
                var cityDto = Database.Books.GetByIdAsync(id.Value);
                Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());
                return Mapper.Map<Book, BookDTO>(await cityDto);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Not found", "");
            }
        }
        public async Task<List<BookDTO>> GetAllAsync()
        {

            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());
            return Mapper.Map<List<Book>, List<BookDTO>>(await Database.Books.GetAllAsync());
        }

        public async Task Update(BookDTO book_dto)
        {
            var book = new Book()
            {
                Id = book_dto.Id,
                Name = book_dto.Name,
                Author = book_dto.Author,
                ISBN = book_dto.ISBN,
                Categories = new List<Category>()
            };
            Database.Books.Update(book);
            await Database.SaveAsync();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
        public async Task DeleteAsync(int id)
        {
            await Database.Books.DeleteAsync(id);
            await Database.SaveAsync();
        }
    }
}
