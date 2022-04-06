#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationTest;
using WebApplicationTest.Entities;
using WebApplicationTest.Views;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DoctorsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("get/{doctorId}")]
        public async Task<ActionResult<Doctor>> Get(int doctorId)
        {
            if (!DoctorExists(doctorId))
            {
                return BadRequest();
            }
            return Ok(await _context.Doctors
                .Include(x => x.Office)
                .Include(x => x.Specialization)
                .Include(x => x.District)
                .FirstOrDefaultAsync(m => m.Id == doctorId));
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<List<DoctorView>>> GetAll(GetRequest request)  
        {
            var result = await _context.Doctors.Include(x => x.Office).Include(x => x.Specialization).Include(x => x.District).ToListAsync();

            if (request != null)
            {
                if (request.SortOption != null)
                {
                    if (request.SortOption == SortOptions.Acsending)
                    {
                        result = result.OrderBy(x => x.FullName).ToList();
                    }
                    else if (request.SortOption == SortOptions.Decsending)
                    {
                        result = result.OrderByDescending(x => x.FullName).ToList();
                    }
                }

                if (request.PageSize != null && request.PageIndex != null)
                {
                    result = result.Skip((int)request.PageSize * (int)request.PageIndex).Take((int)request.PageSize).ToList();
                }
            }

            var doctorViews = DoctorView.MapList(result);

            return Ok(doctorViews);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(Doctor doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }

            _context.Add(doctor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete/{doctorId}")]
        public async Task<ActionResult> Delete(int doctorId)
        {
            var doctor = await _context.Doctors
           .FirstOrDefaultAsync(m => m.Id == doctorId);
            if (doctor == null || doctor.FullName == null)
            {
                return BadRequest();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Edit(Doctor doctor)
        {
            if (doctor == null || doctor.FullName == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(doctor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
