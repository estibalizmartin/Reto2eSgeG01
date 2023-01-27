using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Reto2eSgeG01.Core.Entities;
using Reto2eSgeG01.Core.Models;
using Reto2eSgeG01.Data.Context;

namespace Reto2eSgeG01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly NorthwindContext _northwindContext;
        private readonly IMapper _mapper;

        public UsuariosController(NorthwindContext northwindContext, IMapper mapper)
        {
            _northwindContext = northwindContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _northwindContext.Employees
                .ProjectTo<Employee>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
