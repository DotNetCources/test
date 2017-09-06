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
    public class CategoriesController : ApiController
    {
        private ICategoryService service;

        public CategoriesController(ICategoryService _service)
        {
            service = _service;
        }

        // GET api/categories
        [HttpGet]
        public async Task<List<CategoryModel>> GetAll()
        {
            List<CategoryDTO> categoriesDtos = await service.GetAllAsync();
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>());
            var list = Mapper.Map<List<CategoryDTO>, List<CategoryModel>>(categoriesDtos);
            return list;
        }

        // POST api/categories/?name=categoryName

        [HttpPost]
        public async Task<HttpResponseMessage> Add(string name)
        {
            await service.AddAsync(new CategoryDTO() { Name = name, DateCreated = DateTime.Now });
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
        }


        [HttpPut]
        public async Task<HttpResponseMessage> Update([FromBody]CategoryDTO category_dto)
        {
            try
            {
                await service.Update(category_dto);
                return Request.CreateResponse(HttpStatusCode.OK, category_dto);
            }
            catch
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }
            
        }
        [HttpGet]
        public async Task<CategoryModel> Get(int id)
        {
            CategoryDTO dto = await service.GetByIdAsync(id);
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>());
            var res = Mapper.Map<CategoryDTO, CategoryModel>(dto);
            return res;
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(int id, string name, string author, string isbn)
        {
            try
            {
                var cat_old = await service.GetByIdAsync(id);
                var category_dto = new CategoryDTO()
                {
                    Id = id,
                    Name = name,
                    DateCreated = cat_old.DateCreated
                };
                await service.Update(category_dto);
                return Request.CreateResponse(HttpStatusCode.OK, category_dto);
            }
            catch
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }

        }
        //DELETE api/categories/id
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
