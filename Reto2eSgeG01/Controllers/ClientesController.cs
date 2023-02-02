using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSgeG01.Core.Models;
using Reto2eSgeG01.Data.Context;
using System.Linq;

namespace Reto2eSgeG01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly NorthwindContext _northwindContext;
        private readonly IMapper _mapper;

        public ClientesController(NorthwindContext northwindContext, IMapper mapper)
        {
            _northwindContext = northwindContext;
            _mapper = mapper;
        }


        [HttpGet("Todos los países")]
        public async Task<IEnumerable<string>> GetAllCountries()
        {
            return await _northwindContext.Customers
                .OrderByDescending(c => c.Country)
                .Select(c => c.Country)
                .Distinct()
                .ToListAsync();
        }



        [HttpGet("Todos los clientes")]
        public async Task<IEnumerable<CustomerViewModel>> GetByCountry(string country)
        {
            return await _northwindContext.Customers
                .Where(c => c.Country.ToUpper() == country.ToUpper())
                .OrderBy(c => c.CompanyName)
                .ThenBy(c => c.ContactName)
                //.Select(c => new CustomerViewModel
                //{
                //    CompanyName = c.CompanyName,
                //    ContactName = c.ContactName,
                //    ContactTitle = c.ContactTitle,
                //    Address = c.Address,
                //    City = c.City,
                //    Region = c.Region,
                //    PostalCode = c.PostalCode,
                //    Country = c.Country,
                //    Phone = c.Phone,
                //    Fax = c.Fax
                //})
                .Select(c => _mapper.Map<CustomerViewModel>(c))
                .ToListAsync();
        }

        [HttpGet("Uno o más países")]
        public async Task<IEnumerable<CustomerViewModel>> GetByCountries([FromQuery] string[] countries)
        {
            var query = _northwindContext.Customers.AsQueryable();

            if (countries != null && countries.Any())
            {
                var countriesUpper = countries.Select(c => c.ToUpper());
                query = query.Where(c => countriesUpper.Contains(c.Country.ToUpper()));
            }

            return await query
                .OrderBy(c => c.CompanyName)
                .ThenBy(c => c.ContactName)
                .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<CustomerPaginationModel>>> GetCustomerPaginated(int page = 1, Paginacion paginacion = Paginacion.a)
        {
            
                var result = await _northwindContext.Customers
                    .Select(c => _mapper.Map<CustomerPaginationModel>(c))
                    // el número de reigstros que me voy a saltar
                    .Skip((int)paginacion * (page - 1))
                    // el número de registros a recuperar
                    .Take((int)paginacion)
                    .ToListAsync();
                return result;
            
                
            
        }


    }
}


