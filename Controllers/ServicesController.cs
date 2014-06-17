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
    [RoutePrefix("Services")]
    public class ServicesController : ApiController
    {
        private OpenGovContext db = new OpenGovContext();

        /// <summary>
        /// An Atom feed listing services the organisation offers.
        /// </summary>
        /// <returns>An Atom feed.</returns>
        [Route]
        [ResponseType(typeof(Atom10FeedFormatter))]
        public async Task<IHttpActionResult> GetServices()
        {
            var Services = await db.Services.ToListAsync();

            var ServiceItemList = new List<SyndicationItem>();
            Services.ForEach(i =>
                {
                    var Entry = new SyndicationItem();
                    Entry.Id = i.Id;

                    Entry.Authors.Add(new SyndicationPerson(null, "Cleansing Services", "http://www.surreyhills.gov.uk/cleansing"));
                    Entry.Categories.Add(new SyndicationCategory(i.ServiceCategoryId.ToString(), null, i.Category.Name));
                    Entry.Content = SyndicationContent.CreatePlaintextContent(i.Content);
                    Entry.LastUpdatedTime = i.Updated;
                    Entry.Summary = new TextSyndicationContent(i.Summary);
                    Entry.Title = new TextSyndicationContent(i.Title);                    

                    var AtomLink = new SyndicationLink();
                    AtomLink.RelationshipType = "self";
                    AtomLink.Uri = new Uri(Url.Content("~/Service/" + i.Id));
                    AtomLink.MediaType = "application/atom+xml";
                    Entry.Links.Add(AtomLink);

                    var HtmlLink = new SyndicationLink();
                    HtmlLink.RelationshipType = "self";
                    HtmlLink.Uri = new Uri("http://surreyhillsdc.azurewebsites.net/" + i.Title.ToLower().Replace(" a ", " ").Replace(" the ", " ").Replace(" ", "-"));
                    HtmlLink.MediaType = "text/html";
                    Entry.Links.Add(HtmlLink);

                    ServiceItemList.Add(Entry);
                });

            var ServiceFeed = new SyndicationFeed();
            ServiceFeed.Items = ServiceItemList;
            ServiceFeed.Id = "43UZ";
            ServiceFeed.LastUpdatedTime = ServiceItemList.Max(i => i.LastUpdatedTime);
            ServiceFeed.Title = new TextSyndicationContent("Surrey Hills District Council Service List");
            ServiceFeed.Description = new TextSyndicationContent("Service list for Surrey Hills District Council");
            ServiceFeed.Authors.Add(new SyndicationPerson(null, "Surrey Hills District Council", "http://www.surreyhills.gov.uk/"));

            //foreach (var c in ServiceItemList.SelectMany(i => i.Categories).Distinct())
            //{
            //    ServiceFeed.Categories.Add(c);
            //}

            var SelfLink = new SyndicationLink();
            SelfLink.RelationshipType = "self";
            SelfLink.Uri = new Uri(Url.Content("~/Services"));
            SelfLink.MediaType = "application/atom+xml";
            ServiceFeed.Links.Add(SelfLink);

            return Ok(ServiceFeed.GetAtom10Formatter());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}