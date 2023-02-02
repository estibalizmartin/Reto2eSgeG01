using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSgeG01.Core.Entities;
using Reto2eSgeG01.Core.Models;
using Reto2eSgeG01.Data.Context;
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

        //[HttpGet]
        //public async Task<IEnumerable<EmployeeViewModel>> GetEmployeeByFirstNameAndPassword(string firstName, int password)
        //{
        //    var result = await _northwindContext.Employees.FirstOrDefaultAsync(x => x.FirstName == firstName && x.Password.Equals(password));

        //    return result;
        //}

        [HttpPost("encryptpassword")]
        public async Task<ActionResult> EncryptPassword(int employeeId, string password)
        {
            var employee = await _northwindContext.Employees
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

        [HttpGet("Por fecha")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetByDate(DateTime date)
        {
            var employees = await _northwindContext.Employees
                .Where(employee => employee.BirthDate > date)
                .Select(employee => _mapper.Map<EmployeeViewModel>(employee))
                .ToArrayAsync();

            if (employees.Length == 0)
            {
                return NotFound();
            }
            
            return Ok(employees);
            
        }
    }
}
