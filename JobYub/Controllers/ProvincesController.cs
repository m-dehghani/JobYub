using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobYub.Data;
using JobYub.Models;

namespace JobYub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProvincesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Provinces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvince()
        {
            return await _context.Province.ToListAsync();
        }
		
        // GET: api/Provinces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
            var province = await _context.Province.FindAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }

        // PUT: api/Provinces/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvince(int id, Province province)
        {
            if (id != province.ID)
            {
                return BadRequest();
            }

            _context.Entry(province).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinceExists(id))
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

        // POST: api/Provinces
        [HttpPost]
        public async Task<ActionResult<Province>> PostProvince(Province province)
        {
            _context.Province.Add(province);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvince", new { id = province.ID }, province);
        }

        // DELETE: api/Provinces/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Province>> DeleteProvince(int id)
        {
            var province = await _context.Province.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            _context.Province.Remove(province);
            await _context.SaveChangesAsync();

            return province;
        }

        private bool ProvinceExists(int id)
        {
            return _context.Province.Any(e => e.ID == id);
        }
    }
}
