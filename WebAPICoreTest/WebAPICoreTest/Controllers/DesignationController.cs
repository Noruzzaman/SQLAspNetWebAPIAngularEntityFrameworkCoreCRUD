using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPICoreTest.Model;

namespace WebAPICoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public DesignationController(EmployeeContext context)
        {
            _context = context;
        }


        //Get:api/Designation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDesignation>>> GetTblDesignation()
        {
            return await _context.TblDesignation.ToListAsync();
        }

        //Get By Id:api/Designation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDesignation>> GetTblDesignation(int id)
        {
            var TblDesignation = await _context.TblDesignation.FindAsync(id);
            if (TblDesignation == null)
            {
                return NotFound();
            }
            return TblDesignation;
        }


        //For Update Put:api/Designation/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTblDesignation(int id, TblDesignation oTblDesignation)
        {
            if (id != oTblDesignation.Id)
            {
                return BadRequest();
            }
            _context.Entry(oTblDesignation).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var TblDesignation = await _context.TblDesignation.FindAsync(id);
                if (id != TblDesignation.Id)
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


        //Post:api/Designation
        [HttpPost]
        public async Task<ActionResult<TblDesignation>> PostTblDesignation(TblDesignation oTblDesignation)
        {

            _context.TblDesignation.Add(oTblDesignation);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTblDesignation", new { id = oTblDesignation.Id }, oTblDesignation);
        }


        //Delete:api/Designation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblDesignation>> DeleteTblDesignation(int id)
        {
            var TblDesignation = await _context.TblDesignation.FindAsync(id);
            if (TblDesignation == null)
            {
                return NotFound();
            }
            _context.TblDesignation.Remove(TblDesignation);
            await _context.SaveChangesAsync();

            return TblDesignation;
        }

    }
}
