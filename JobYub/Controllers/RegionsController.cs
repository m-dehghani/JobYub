﻿using System;
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
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Regions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegion()
        {
            return await _context.Region.ToListAsync();
        }

        // GET: api/Regions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
            var region = await _context.Region.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        // PUT: api/Regions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegion(int id, Region region)
        {
            if (id != region.ID)
            {
                return BadRequest();
            }

            _context.Entry(region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/Regions
        [HttpPost]
        public async Task<ActionResult<Region>> PostRegion(Region region)
        {
            _context.Region.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.ID }, region);
        }

        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Region>> DeleteRegion(int id)
        {
            var region = await _context.Region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Region.Remove(region);
            await _context.SaveChangesAsync();

            return region;
        }

        private bool RegionExists(int id)
        {
            return _context.Region.Any(e => e.ID == id);
        }
    }
}
