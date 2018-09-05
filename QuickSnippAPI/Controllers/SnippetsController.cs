using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuickSnippAPI.Models;

namespace QuickSnippAPI.Controllers
{
    [Authorize]
    public class SnippetsController : ApiController
    {
        private SnippetDBContext db = new SnippetDBContext();

        // GET: api/Snippets
        public IQueryable<Snippet> GetSnippet()
        {
            return db.Snippet;
        }

        // GET: api/Snippets/5
        [ResponseType(typeof(Snippet))]
        public IHttpActionResult GetSnippet(int id)
        {
            Snippet snippet = db.Snippet.Find(id);
            if (snippet == null)
            {
                return NotFound();
            }

            return Ok(snippet);
        }

        // PUT: api/Snippets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSnippet(int id, Snippet snippet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != snippet.ID)
            {
                return BadRequest();
            }

            db.Entry(snippet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SnippetExists(id))
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

        // POST: api/Snippets
        [ResponseType(typeof(Snippet))]
        public IHttpActionResult PostSnippet(Snippet snippet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Snippet.Add(snippet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = snippet.ID }, snippet);
        }

        // DELETE: api/Snippets/5
        [ResponseType(typeof(Snippet))]
        public IHttpActionResult DeleteSnippet(int id)
        {
            Snippet snippet = db.Snippet.Find(id);
            if (snippet == null)
            {
                return NotFound();
            }

            db.Snippet.Remove(snippet);
            db.SaveChanges();

            return Ok(snippet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SnippetExists(int id)
        {
            return db.Snippet.Count(e => e.ID == id) > 0;
        }
    }
}