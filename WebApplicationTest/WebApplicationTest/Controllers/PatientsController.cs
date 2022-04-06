#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTest.Entities;
using WebApplicationTest.Views;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PatientsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("get/{patientId}")]
        public async Task<ActionResult<Patient>> Get(int patientId)
        {
            if (!PatientExists(patientId))
            {
                return BadRequest();
            }
            return Ok(await _context.Patients
                .Include(x => x.Districts)
                .FirstOrDefaultAsync(m => m.Id == patientId));
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<List<PatientView>>> GetAll(GetRequest request)
        {
            var result = await _context.Patients.Include(x => x.Districts).ToListAsync();

            if (request != null)
            {
                if (request.SortOption != null)
                {
                    if (request.SortOption == SortOptions.Acsending)
                    {
                        result = result.OrderBy(x => x.LastName).ToList();
                    }
                    else if (request.SortOption == SortOptions.Decsending)
                    {
                        result = result.OrderByDescending(x => x.LastName).ToList();
                    }
                }

                if (request.PageSize != null && request.PageIndex != null)
                {
                    result = result.Skip((int)request.PageSize * (int)request.PageIndex).Take((int)request.PageSize).ToList();
                }
            }

            return Ok(PatientView.MapList(result));
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(Patient patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }

            _context.Add(patient);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete/{patientId}")]
        public async Task<ActionResult> Delete(int patientId)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
            {
                return BadRequest();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Edit(Patient patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(patient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
