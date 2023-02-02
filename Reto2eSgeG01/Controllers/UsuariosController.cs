using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSgeG01.Core.Entities;
using Reto2eSgeG01.Core.Models;
using Reto2eSgeG01.Data.Context;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;

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

        [HttpPost("Get employee by first name and password")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployeeByFirstNameAndPassword(UserPostModel userPostModel)
        {
            var entity = await _northwindContext.Employees
                .Where(x => x.FirstName.Equals(userPostModel.FirstName) && x.Password.Equals(userPostModel.Password))
                .ProjectTo<EmployeeViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (entity is null)
            {
                return NotFound();
            }
            else
            {
                return entity;
            }
        }

        [HttpPut("Encrypt password")]
        public async Task<ActionResult> EncryptPassword(int employeeId, string password)
        {
            var employee = await _northwindContext.Employees
                .AsTracking()
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee is null)
            {
                return NotFound();
            }
            else
            {
                var encryptedPassword = Encoding.Default.GetString(CalculateSHA256(password));
                employee.Password = encryptedPassword;
                await _northwindContext.SaveChangesAsync();
                return Ok();
            }
        }

        private byte[] CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));

            return hashValue;
        }
    }
}
