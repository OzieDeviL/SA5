using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA5.WebApiDomainDal.Models.DomainModels;
using SA5.WebApiDomainDal.Models.RequestModels;

namespace SA5.WebApiDomainDal.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
#warning Refactor Controllers with repositories and seperate services
        private SA5DbContext _dbContext;
        public EmployeeController(SA5DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            Employee dbRepresentation = await _dbContext.FindAsync<Employee>(id);
            return Ok(dbRepresentation);
        }
        
        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmployeeInsertRequestModel clientRepresentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee domainEmployee = (Employee)clientRepresentation;
            _dbContext.Add<Employee>(domainEmployee);
            await _dbContext.SaveChangesAsync();
            Employee dbRepresentation = domainEmployee;
            return Ok(dbRepresentation.Id);
        }
        
        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EmployeeUpdateRequestModel clientRepresentation)
        {
            if (id != clientRepresentation.Id)
            {
                return BadRequest("Resource URI does not match clientRepresentation id");
            }
            Employee domainEmployee = (Employee)clientRepresentation;
            _dbContext.Entry(domainEmployee).State = EntityState.Unchanged;
            //_dbContext.Employees.Attach(domainEmployee);
            _dbContext.Entry(domainEmployee).Property("Name").IsModified = true;
            _dbContext.Entry(domainEmployee).Property("Office").IsModified = true;
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return Ok(rowsAffected);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = new Employee() { Id = id };
            _dbContext.Employees.Attach(employee);
            _dbContext.Employees.Remove(employee);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return Ok(rowsAffected);
        }
    }
}
