using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAPI.Data;
using PatientAPI.Models;
using System.Diagnostics;

namespace PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController(PatientDbContext context) : ControllerBase
    {

        private readonly PatientDbContext _context = context;

        [HttpPost]
        public ActionResult<Patient>AddPatient(Patient newPatient)
        {
            if (newPatient == null)
                return BadRequest("Var vänlig och fyll i patientdata");
            // Kolla om personnumret redan finns (för att undvika dubletter)
            var existingPatient = _context.Patients.FirstOrDefault(p => p.Personnummer == newPatient.Personnummer);
            if (existingPatient != null)
            {
                return Conflict("Det existerar redan en patient med detta personnummer.");
            }

            _context.Patients.Add(newPatient);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPatientByPersonalNumber), new { personnummer = newPatient.Personnummer }, newPatient);

        }


        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            return Ok(await _context.Patients.ToListAsync());
        }

        [HttpGet("{personnummer}")]
        public async Task<ActionResult<Patient>> GetPatientByPersonalNumber(string personnummer)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Personnummer == personnummer);

            if (patient is null)
                return NotFound($"Inga patienter med personnummer {personnummer} hittades.");

            return Ok(patient);
        }

        [HttpPut("{personnummer}")]
        public async Task<IActionResult> UpdatePatient(string personnummer, Patient updatesPatient)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Personnummer == personnummer);

            if (patient is null)
                return NotFound($"Inga patienter med personnummer {personnummer} hittades.");

            // Uppdatera patientens fält
            patient.Fornamn = updatesPatient.Fornamn;
            patient.Efternamn = updatesPatient.Efternamn;

            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content = Uppdatering lyckades
        }

        [HttpDelete("{personnummer}")]
        public async Task<IActionResult> DeletePatient(string personnummer)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Personnummer == personnummer);

            if (patient is null)
                return NotFound($"No patient found with personal number {personnummer}.");

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
