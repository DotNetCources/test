using API.Models;
using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class BooksController : ApiController
    {
        private IBookService service;
        private ICategoryService categoryService;

        public BooksController(IBookService _service, ICategoryService _categoryService)
        {
            service = _service;
            categoryService = _categoryService;
        }

        // GET api/books
        [HttpGet]
        public async Task<List<BookModel>> GetAll()
        {
            List<BookDTO> booksDtos = await service.GetAllAsync();
            Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, BookModel>());
            var list = Mapper.Map<List<BookDTO>, List<BookModel>>(booksDtos);
            return list;
        }

        [HttpGet]
        public async Task<BookModel> Get(int id)
        {
            BookDTO booksDtos = await service.GetByIdAsync(id);
            Mapper.Initialize(cfg => cfg.CreateMap<BookDTO, BookModel>());
            var res = Mapper.Map<BookDTO,BookModel>(booksDtos);
            return res;
        }
        [HttpPost]
        public async Task<HttpResponseMessage> AddCategory(int bookId, int categoryId)
        {
            try
            {
                var category = await categoryService.GetByIdAsync(categoryId);
                var book = await service.GetByIdAsync(bookId);
                if (book.Categories == null)
                    book.Categories = new List<CategoryDTO>();

                book.Categories.Add(category);
                await service.Update(book);
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            } catch(Exception ex)
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }

        }
        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id, string name, string author, string isbn)
        {
            try
            {
                var book_old = await service.GetByIdAsync(id);
                var book_dto = new BookDTO()
                {
                    Id = id,
                    Name = name,
                    Author = author,
                    ISBN = isbn,
                    Categories = book_old.Categories
                };
                await service.Update(book_dto);
                return Request.CreateResponse(HttpStatusCode.OK, book_dto);
            }
            catch
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }

        }

        [HttpPost]
        public async Task<HttpResponseMessage> Add(string name, string author, string isbn)
        {
            await service.AddAsync(new BookDTO() { Name = name, Author = author, ISBN = isbn });
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
        }

        //DELETE api/books/id
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            if (service.GetByIdAsync(id) != null)
            {
                await service.DeleteAsync(id);
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            }
            else
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };

        }


    }
}
