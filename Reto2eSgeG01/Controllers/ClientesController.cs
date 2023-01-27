using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSgeG01.Core.Models;
using Reto2eSgeG01.Data.Context;

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
        

        [HttpGet("{country}")]
        public async Task<IEnumerable<CustomerViewModel>> GetByCountry(string country)
        {
            return await _northwindContext.Customers
                .Where(c => c.Country.ToUpper() == country.ToUpper())
                .OrderBy(c => c.CompanyName)
                .ThenBy(c => c.ContactName)
                .Select(c => new CustomerViewModel
                {
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    ContactTitle = c.ContactTitle,
                    Address = c.Address,
                    City = c.City,
                    Region = c.Region,
                    PostalCode = c.PostalCode,
                    Country = c.Country,
                    Phone = c.Phone,
                    Fax = c.Fax
                })
                .ToListAsync();
        }

        [HttpGet("bycountries")]
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


    }
}


