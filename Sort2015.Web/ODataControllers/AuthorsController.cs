using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Sort2015.Data;
using Sort2015.Data.Models;

namespace Sort2015.Web.ODataControllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Sort2015.Data.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Author>("Authors");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AuthorsController : ODataController
    {
        private GLFeedContext db = new GLFeedContext();

        // GET: odata/Authors
        [EnableQuery]
        public IQueryable<Author> GetAuthors()
        {
            return db.Authors;
        }

        // GET: odata/Authors(5)
        [EnableQuery]
        public SingleResult<Author> GetAuthor([FromODataUri] int key)
        {
            return SingleResult.Create(db.Authors.Where(author => author.Id == key));
        }

        // PUT: odata/Authors(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Author> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author author = db.Authors.Find(key);
            if (author == null)
            {
                return NotFound();
            }

            patch.Put(author);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(author);
        }

        // POST: odata/Authors
        public IHttpActionResult Post(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(author);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AuthorExists(author.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(author);
        }

        // PATCH: odata/Authors(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Author> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Author author = db.Authors.Find(key);
            if (author == null)
            {
                return NotFound();
            }

            patch.Patch(author);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(author);
        }

        // DELETE: odata/Authors(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Author author = db.Authors.Find(key);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int key)
        {
            return db.Authors.Count(e => e.Id == key) > 0;
        }
    }
}
