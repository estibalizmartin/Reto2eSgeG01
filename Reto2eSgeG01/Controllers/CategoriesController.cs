using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSgeG01.Core.Entities;
using Reto2eSgeG01.Core.Models;
using Reto2eSgeG01.Data.Context;
using System.Collections.ObjectModel;

namespace Reto2eSgeG01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            return await _context.Categories
                .Select(a => _mapper.Map<CategoryViewModel>(a))
                .ToListAsync();
        }
        [HttpPost("createCategory")]
        public async Task<ActionResult> Post()
        {
            var category = new Category()
            {
                CategoryName = "Lounge",
                Description = "A great hall, ideal for parties and reunions",
                Picture = null,
                Products = null
                /*
                Products = new Collection<Product>
                {
                    new Product()
                    {
                        ProductName = "Dinner",
                        SupplierId = 3,
                        CategoryId = 1,
                        QuantityPerUnit = "50 meters",
                        UnitPrice = 18,
                        UnitsInStock = 1,
                        UnitsOnOrder = 1,
                        ReorderLevel = 1,
                        Discontinued = true
                    },
                    new Product()
                    {
                        ProductName = "Dinner2",
                        SupplierId = 2,
                        CategoryId = 2,
                        QuantityPerUnit = "500 jars",
                        UnitPrice = 50,
                        UnitsInStock = 1,
                        UnitsOnOrder = 1,
                        ReorderLevel = 1,
                        Discontinued = true
                    }
                }*/
            };
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryElim = await _context.Categories
                .AsTracking()
                .FirstOrDefaultAsync(category => category.CategoryId == id);
            if (categoryElim == null)
            {
                return NotFound();
            }
            else
            {
                //generoElim.EstaBorrado = true;
                _context.Categories.Remove(categoryElim);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CategoryUpdateModel model)
        {
            var categoryModif = await _context.Categories
               .AsTracking()
               .FirstOrDefaultAsync(category => category.CategoryId == id);
            if (categoryModif == null)
            {
                return NotFound();
            }
            else
            {
                //Con automapper no deja hacerlo
                //categoryModif = _mapper.Map<Category>(model);

                categoryModif.CategoryName = model.CategoryName;
                categoryModif.Description = model.Description;
                categoryModif.Picture = model.Picture;

                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
