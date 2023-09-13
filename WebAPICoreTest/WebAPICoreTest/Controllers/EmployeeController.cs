using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPICoreTest.Model;
using System.Linq;

namespace WebAPICoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }


        //Get:api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEmployee>>> GetTblEmployee()
        {

            var employees = (from e in _context.TblEmployee
                             join d in _context.TblDesignation 
                             on e.DesignationID equals d.Id                             
                             
                             select new TblEmployee
                             {
                                 Id = e.Id,
                                 Name = e.Name,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 Age = e.Age,
                                 DesignationID = e.DesignationID,
                                 Designation = d.Designation,
                                 Doj=e.Doj,
                                 Gender = e.Gender,
                                 IsActive = e.IsActive,
                                 IsMarried = e.IsMarried


                             }
                             ).ToListAsync();

           return await employees;
        }

        //Get By Id:api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblEmployee>> GetTblEmployee(int id)
        {
            var TblEmployee= await _context.TblEmployee.FindAsync(id);
            if (TblEmployee == null)
            {
                return NotFound();
            }
            return TblEmployee;
        }


        //For Update Put:api/Employee/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTblEmployee(int id, TblEmployee oTblEmployee)
        {
            if (id != oTblEmployee.Id)
            {
                return BadRequest();
            }
            _context.Entry(oTblEmployee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var TblEmployees = await _context.TblEmployee.FindAsync(id);
                if (id != TblEmployees.Id)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        //Post:api/Employee
        [HttpPost]
        public async Task<ActionResult<TblEmployee>> PostTblEmployee(TblEmployee oTblEmployee)
        {

            _context.TblEmployee.Add(oTblEmployee);
            await _context.SaveChangesAsync();   
            return CreatedAtAction("GetTblEmployee", new{id= oTblEmployee.Id},oTblEmployee);
        }


        //Delete:api/Employee/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<TblEmployee>> DeleteTblEmployee(int id)
        {
            var TblEmployee = await _context.TblEmployee.FindAsync(id);
            if (TblEmployee == null)
            {
                return NotFound();
            }
            _context.TblEmployee.Remove(TblEmployee);
            await _context.SaveChangesAsync();

            return TblEmployee;
        }



    }
}
