using JournalAPI.Data;
using JournalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly JournalDbContext _context;

        public JournalController(JournalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Journal>> AddJournal([FromBody] Journal newJournal)
        {
            if (newJournal == null || string.IsNullOrEmpty(newJournal.Anteckning) || string.IsNullOrEmpty(newJournal.Personnummer))
                return BadRequest("Var vänlig och fyll i besöksorsaken.");

            // Här sparar vi journalen med personnummer och besöksorsak
            var journalToSave = new Journal
            {
                Personnummer = newJournal.Personnummer,  // Personnummer
                Anteckning = newJournal.Anteckning  // ReasonForVisit
            };

            // Lägg till journalen i databasen
            _context.Journaler.Add(journalToSave);
            await _context.SaveChangesAsync();

            // Skicka tillbaka den sparade journalen som svar
            return CreatedAtAction(nameof(GetJournals), new { id = journalToSave.JournalId }, journalToSave);
        }

        [HttpGet]
        public async Task<ActionResult<List<Journal>>> GetJournals()
        {
            return Ok(await _context.Journaler.ToListAsync());
        }

        [HttpGet("{personnummer}")]
        public async Task<ActionResult<List<Journal>>> GetJournalByPersonalNumber(string personnummer)
        {
            var journaler = await _context.Journaler
                .Where(j => j.Personnummer == personnummer)
                .ToListAsync();

            if (journaler.Count == 0)
                return NotFound($"Inga journalanteckningar hittades för personnummer {personnummer}.");

            return Ok(journaler);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJournal(int id, Journal updatesJournal)
        {
            var journal = await _context.Journaler.FirstOrDefaultAsync(j => j.JournalId == id);

            if (journal is null)
                return NotFound($"Inga journaler hittades.");


           
            // Uppdatera patientens fält
            journal.Anteckning = updatesJournal.Anteckning;
            journal.Personnummer = updatesJournal.Personnummer;
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content = Uppdatering lyckades
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteJournal(int id)
        {
            var journal = await _context.Journaler.FirstOrDefaultAsync(j=> j.JournalId == id);

            if (journal is null)
                return NotFound();

            _context.Journaler.Remove(journal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

