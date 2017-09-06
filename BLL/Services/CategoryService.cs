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
    public class CategoryService: ICategoryService
    {
        IUnitOfWork Database { get; set; }

        public CategoryService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public async Task AddAsync(CategoryDTO catDto)
        {
            Category model = new Category()
            {
                Id = catDto.Id,
                Name = catDto.Name,
                DateCreated = catDto.DateCreated
            };
            await Database.Categories.CreateAsync(model);
            await Database.SaveAsync();

        }
        public async Task<CategoryDTO> GetByIdAsync(int? id)
        {
            try
            {
                var catDto = Database.Categories.GetByIdAsync(id.Value);
                Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
                return Mapper.Map<Category, CategoryDTO>(await catDto);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Not found", "");
            }
        }
        public async Task<List<CategoryDTO>> GetAllAsync()
        {

            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
            return Mapper.Map<List<Category>, List<CategoryDTO>>(await Database.Categories.GetAllAsync());
        }

        public async Task Update(CategoryDTO cat_dto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>());
            Database.Categories.Update(Mapper.Map<CategoryDTO, Category>(cat_dto));
            await Database.SaveAsync();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
        public async Task DeleteAsync(int id)
        {
            await Database.Categories.DeleteAsync(id);
            await Database.SaveAsync();
        }
    }
}
