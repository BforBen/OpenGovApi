using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OpenGovApi.Models;

namespace OpenGovApi.Controllers
{
    /// <summary>
    /// Person controller
    /// </summary>
    [RoutePrefix("Person")]
    public class PersonController : ApiController
    {
        private OpenGovContext db = new OpenGovContext();

        [Route]
        [HttpGet]
        public IQueryable<Person> GetPeople()
        {
            return db.People;
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> GetPerson(string id)
        {
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerson(string id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            db.Entry(person).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
        }

        /// <summary>
        /// Remove a person record
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> DeletePerson(string id)
        {
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(string id)
        {
            return db.People.Count(e => e.Id == id) > 0;
        }
    }
}