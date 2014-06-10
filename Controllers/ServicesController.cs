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
    [RoutePrefix("Services")]
    public class ServicesController : ApiController
    {
        private OpenGovContext db = new OpenGovContext();

        [Route]
        public IQueryable<Service> GetServices()
        {
            return db.Services;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceExists(int id)
        {
            return db.Services.Count(e => e.Id == id) > 0;
        }
    }
}