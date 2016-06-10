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
    public class MHUserController : Controller
    {
        private MusicHistoryDbContext _context;

        public MHUserController(MusicHistoryDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] string username)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable<MHUser> mhusers = from mhuser in _context.MHUser
                                       select new MHUser
                                       {
                                           MHUserId = mhuser.MHUserId,
                                           Username = mhuser.Username,
                                           EmailAddress = mhuser.EmailAddress
                                           //Albums = String.Format("/api/Album?MHUserId={0}", mhuser.MHUserId)
                                       };
            if (username != null)
            {
                mhusers = mhusers.Where(u => u.Username == username);
            }

            if (mhusers == null)
            {
                return NotFound();
            }

            return Ok(mhusers);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetMHUser")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MHUser mhuser = _context.MHUser.Single(m => m.MHUserId == id);

            if (mhuser == null)
            {
                return NotFound();
            }

            return Ok(mhuser);
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]MHUser mhuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = from g in _context.MHUser
                               where g.Username == mhuser.Username
                               select g;

            if (existingUser.Count<MHUser>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.MHUser.Add(mhuser);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MHUserExists(mhuser.MHUserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetMHUser", new { id = mhuser.MHUserId }, mhuser);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MHUser mhuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mhuser.MHUserId)
            {
                return BadRequest();
            }

            _context.Entry(mhuser).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MHUserExists(mhuser.MHUserId))
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

            MHUser mhuser = _context.MHUser.Single(m => m.MHUserId == id);
            if (mhuser == null)
            {
                return NotFound();
            }

            _context.MHUser.Remove(mhuser);
            _context.SaveChanges();

            return Ok(mhuser);
        }

        private bool MHUserExists(int id)
        {
            return _context.MHUser.Count(e => e.MHUserId == id) > 0;
        }
    }
}
