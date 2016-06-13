using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using team_music_history_api_back_end.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace team_music_history_api_back_end.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SongController : Controller
    {
        private MusicHistoryDbContext _context;

        public SongController(MusicHistoryDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //IQueryable<object> song = from i in _context.Song
            //                           select i;

            IQueryable<object> song = from s in _context.Song
                                      join a in _context.Album
                                      on s.AlbumId equals a.AlbumId
                                      join m in _context.MHUser
                                      on a.MHUserId equals m.MHUserId
                                      select new
                                      {
                                          MHUserId = a.MHUserId,
                                          Username = m.Username,
                                          EmailAddress = m.EmailAddress,
                                          SongId = s.SongId,
                                          SongName = s.Name,
                                          AlbumId = s.AlbumId,
                                          Genre = s.Genre,
                                          Author = s.Author,
                                          Image = s.Image,
                                          Artist = a.Artist,
                                          Year = a.Year,
                                          AlbumName = a.Name

                                      };


            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetSong")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Song song = _context.Song.Single(m => m.SongId == id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Song.Add(song);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SongExists(song.SongId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSong", new { id = song.SongId }, song);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != song.SongId)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Song song = _context.Song.Single(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Song.Remove(song);
            _context.SaveChanges();

            return Ok(song);
        }

        private bool SongExists(int id)
        {
            return _context.Song.Count(e => e.SongId == id) > 0;
        }
    }
}
