using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicEfCoreDemo.Data;
using BasicEfCoreDemo.Models;
using BasicEfCoreDemo.Mapping;

namespace BasicEfCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController(InvoiceDbContext context) : ControllerBase
    {

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> GetActor()
        {
            return await context.Actor
                            .Include(x => x.Movies)
                            .Select(a => new ActorDto(
                                a.Id,
                                a.Name,
                                a.Movies.Select(m => new MovieDto(
                                    m.Id,
                                    m.Title,
                                    m.Description,
                                    m.ReleaseYear
                                )).ToList()
                            ))
                            .ToListAsync();
            // return (await context.Actor
            //         .Include(x => x.Movies)
            //         .ToListAsync())
            //         .ToDto()
            //         .ToList();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(Guid id)
        {
            var actor = await context.Actor.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(Guid id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            context.Entry(actor).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            context.Actor.Add(actor);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }


        [HttpPost("{id}/movies/{moviesId}")]
        public async Task<IActionResult> AddMovie(Guid id, Guid movieId)
        {
            if (context.Actor == null)
            {
                return NotFound("Actors is null.");
            }

            var actor = await context.Actor
                .Include(x => x.Movies)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (actor == null)
            {
                return NotFound($"Actor with id {id} not found.");
            }

            var movie = await context.Movie.FindAsync(movieId);
            if (movie == null)
            {
                return NotFound($"Movie with id {movieId} not found.");
            }

            actor.Movies.Add(movie);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetMovies(Guid id)
        {
            if (context.Actor == null)
            {
                return NotFound("Actors is null.");
            }

            var actor = await context.Actor
                .Include(x => x.Movies)
                .Where(x => x.Id == id) 
                .Select(a => new ActorDto(
                                a.Id,
                                a.Name,
                                a.Movies.Select(m => new MovieDto(
                                    m.Id,
                                    m.Title,
                                    m.Description,
                                    m.ReleaseYear
                                )).ToList()
                            ))
                .SingleOrDefaultAsync();

            if (actor == null)
            {
                return NotFound($"Actor with id {id} not found.");
            }

            return Ok(actor.Movies);
        }
        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(Guid id)
        {
            var actor = await context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            context.Actor.Remove(actor);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}/movies/{movieId}")]
        public async Task<IActionResult> DeleteMovie(Guid id, Guid movieId)
        {
            if (context.Actor == null)
            {
                return NotFound("Actors is null.");
            }

            var actor = await context.Actor.Include(x => x.Movies).SingleOrDefaultAsync(a => a.Id == id);
            if (actor == null)
            {
                return NotFound($"Actor with id {id} not found.");
            }

            var movie = await context.Movie.FindAsync(movieId);
            if (movie == null)
            {
                return NotFound($"Movie with id {movieId} not found.");
            }

            actor.Movies.Remove(movie);
            await context.SaveChangesAsync();

            return NoContent();
        }
        private bool ActorExists(Guid id)
        {
            return context.Actor.Any(e => e.Id == id);
        }
    }
}
