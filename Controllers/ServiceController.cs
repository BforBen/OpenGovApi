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
using System.ServiceModel.Syndication;

namespace OpenGovApi.Controllers
{
    [RoutePrefix("Service")]
    public class ServiceController : ApiController
    {
        private OpenGovContext db = new OpenGovContext();

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(Atom10FeedFormatter))]
        public async Task<IHttpActionResult> GetService(string id)
        {
            Service service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            var ServiceFeed = new SyndicationFeed();
            ServiceFeed.Id = service.Id;
            ServiceFeed.LastUpdatedTime = service.Updated;
            ServiceFeed.Title = new TextSyndicationContent(service.Title);
            ServiceFeed.Description = new TextSyndicationContent(service.Summary);

            ServiceFeed.Categories.Add(new SyndicationCategory(service.ServiceCategoryId.ToString(), null, service.Category.Name));

            var SelfLink = new SyndicationLink();
            SelfLink.RelationshipType = "self";
            SelfLink.Uri = new Uri(Url.Content("~/Service/" + service.Id));
            SelfLink.MediaType = "application/atom+xml";
            ServiceFeed.Links.Add(SelfLink);

            return Ok(ServiceFeed.GetAtom10Formatter());
        }

        [HttpPost]
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutService(string id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            db.Entry(service).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Services.Add(service);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = service.Id }, service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceExists(string id)
        {
            return db.Services.Count(e => e.Id == id) > 0;
        }
    }
}