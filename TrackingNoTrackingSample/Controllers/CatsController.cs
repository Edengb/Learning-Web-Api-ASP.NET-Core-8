using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingNoTrackingSample.Data;
using TrackingNoTrackingSample.Dtos;
using TrackingNoTrackingSample.Models;

namespace TrackingNoTrackingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController(CatDbContext _context, ILogger<CatsController> logger) : ControllerBase
    {
        
        [HttpGet("tests/tracking/first-find-single")]
        public async Task<ActionResult> GetTestingDefaultBevahiors()
        {
            //Tracking the first entity
            Cat firstCat = await _context.Cats.FirstAsync();

            // The Find will return the tracked entity without making a request to the database
            await _context.Cats.FindAsync(firstCat.Id);

            //The First and Single methods make requests to the database independently.
            await _context.Cats.Where((cat) => cat.Id == firstCat.Id).SingleAsync();

            return Ok("Only two operations against database. Look at logs!");
        }

        [HttpGet("tests/no-tracking/first-find-single")]
        public async Task<ActionResult> GetTestingNoTrackingBehavior()
        {
            Cat firstCat = await _context.Cats.AsNoTracking().FirstAsync();

            // Find will make a request to the database
            await _context.Cats.FindAsync(firstCat.Id);

            // The First and Single methods make requests to the database independently.
            await _context.Cats.Where((cat) => cat.Id == firstCat.Id).SingleAsync();

            return Ok("Three operations against database. Look at logs!");
        }

        [HttpGet("tracking-demo/outdated-cat")]
        public async Task<ActionResult> GetOutdatedCat()
        {
            List<Cat> catsBeforeUpdated = await _context.Cats.ToListAsync();  
            _context.Cats.ExecuteUpdateAsync(setters => setters.SetProperty(cat => cat.Age, 6));
            List<Cat> catsAfterUpdated = await _context.Cats.ToListAsync();
            return Ok(catsAfterUpdated);
        }

        [HttpGet("no-tracking-demo/updated-cat")]
        public async Task<ActionResult> GetUpdatedCat()
        {
            List<Cat> catsBeforeUpdated = await _context.Cats.AsNoTracking().ToListAsync();  
            _context.Cats.ExecuteUpdateAsync(setters => setters.SetProperty(cat => cat.Age, 15));
            List<Cat> catsAfterUpdated = await _context.Cats.ToListAsync();
            return Ok(catsAfterUpdated);
        }

        [HttpGet("tracking-demo/outdated-new-cat")]
        public async Task<ActionResult> GetOutdatedAndNewCat()
        {
            //Tracking the first entity
            Cat firstCat = await _context.Cats.FirstAsync();

            _context.Cats.Where((cat) => cat.Id == firstCat.Id).ExecuteDeleteAsync();

            firstCat.Age = 11;

            _context.Entry(firstCat).State = EntityState.Added;
            
            await _context.SaveChangesAsync();

            return Ok("Look at logs the database operations!");
        }

        // GET: api/cats/states-demo/attached-cat
        [HttpGet("states-demo/attached-cat")]
        public async Task<ActionResult<IEnumerable<Cat>>> GetAttachedCat()
        {
            
            //Detached cat because of AsNoTracking
            Cat cat = await _context.Cats.AsNoTracking().FirstAsync();

            string textFirstState = CatsController.GetActualEntityState(_context.Entry(cat).State);

            logger.LogInformation($"State after calling FirstAsync with AsNoTracking: {textFirstState}");

            await _context.Cats.ExecuteUpdateAsync(setters => setters.SetProperty(cat => cat.Age, 35));

            //Attaches the Cat
            _context.Entry(cat).State = EntityState.Unchanged;

            string textSecondState = CatsController.GetActualEntityState(_context.Entry(cat).State);;

            logger.LogInformation($"State after attaching it: {textSecondState}");
            logger.LogInformation("FindAsync above --- return the tracked entity - outdated cat");
            Cat attachedOutdatedCat = await _context.Cats.FindAsync(cat.Id);

            return Ok(attachedOutdatedCat);
        }

        [HttpGet("states-demo/detached-cat")]
        public async Task<ActionResult<IEnumerable<Cat>>> GetDettachedCat()
        {
            //Attached cat by default
            Cat cat = await _context.Cats.FirstAsync();
            
            string textFirstState = CatsController.GetActualEntityState(_context.Entry(cat).State);

            logger.LogInformation($"State after calling FirstAsync with AsNoTracking: {textFirstState}");

            await _context.Cats.ExecuteUpdateAsync(setters => setters.SetProperty(cat => cat.Age, 22));

            //Detached the Cat
            _context.Entry(cat).State = EntityState.Detached;

            string? textSecondState = CatsController.GetActualEntityState(_context.Entry(cat).State);

            logger.LogInformation($"State after attaching it: {textSecondState}");
            logger.LogInformation("FindAsync above --- request to Database - updated cat");

            Cat updatedCat = await _context.Cats.FindAsync(cat.Id);

            return Ok(updatedCat);
        }

        // GET: api/Cats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> GetCat(Guid id)
        {
            var cat = await _context.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        // PUT: api/Cats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCat(Guid id, PutCatDto cat)
        {
            _context.Entry(new Cat{
                Id = id,
                Nickname = cat.Nickname,
                Age = cat.Age,
                Breed = cat.Breed
            }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(id))
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

        // POST: api/Cats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("states-demo/added-cat")]
        public async Task<ActionResult<Cat>> PostFixedCat()
        {
            Guid id = Guid.NewGuid();

            Cat cat = new Cat{
                Age = 1,
                Breed = Enums.CatBreeds.Abyssinian,
                Id = id,
                
            };
            
            _context.Entry(cat).State = EntityState.Added;

            string textState = CatsController.GetActualEntityState(_context.Entry(cat).State);

            logger.LogInformation($"Adding Manually the state: {textState}");
            
            Cat attachedNewCat = await _context.Cats.FindAsync(id);

            await _context.SaveChangesAsync();

            return Ok(attachedNewCat);
        }

        // DELETE: api/Cats
        [HttpDelete("states-demo/last")]
        public async Task<IActionResult> DeleteCat()
        {
            var cat = await _context.Cats.OrderByDescending(cat => cat.Id).FirstAsync();

            _context.Entry(cat).State = EntityState.Deleted;

            string textState = CatsController.GetActualEntityState(_context.Entry(cat).State);;

            logger.LogInformation($"Handling manually the state: {textState}");

            await _context.SaveChangesAsync();

            return NoContent();
        }

        public static string GetActualEntityState(EntityState entityState)
        {
            Array entityStateValues = Enum.GetValues(typeof(EntityState));
            string? textState = Convert.ToString(entityStateValues.GetValue((int)entityState));
            return textState;
        }
        private bool CatExists(Guid id)
        {
            return _context.Cats.Any(e => e.Id == id);
        }
    }
}
