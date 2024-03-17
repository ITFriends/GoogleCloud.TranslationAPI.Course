using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Translator.WebService.Data;
using Translator.WebService.Models;

namespace Translator.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TranslationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Translations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Translation>>> GetTranslations()
        {
          if (_context.Translations == null)
          {
              return NotFound();
          }
            return await _context.Translations.ToListAsync();
        }

        // GET: api/Translations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Translation>> GetTranslation(string id)
        {
          if (_context.Translations == null)
          {
              return NotFound();
          }
            var translation = await _context.Translations.FindAsync(id);

            if (translation == null)
            {
                return NotFound();
            }

            return translation;
        }

        // PUT: api/Translations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranslation(string id, Translation translation)
        {
            if (id != translation.Id)
            {
                return BadRequest();
            }

            _context.Entry(translation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationExists(id))
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

        // DELETE: api/Translations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslation(string id)
        {
            if (_context.Translations == null)
            {
                return NotFound();
            }
            var translation = await _context.Translations.FindAsync(id);
            if (translation == null)
            {
                return NotFound();
            }

            _context.Translations.Remove(translation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TranslationExists(string id)
        {
            return (_context.Translations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
