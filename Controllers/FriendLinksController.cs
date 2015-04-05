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
using Accounts;

namespace Accounts.Controllers
{
    public class FriendLinksController : ApiController
    {
        private AccountsEntities db = new AccountsEntities();

        // GET: api/FriendLinks
        public IQueryable<FriendLink> GetFriendLinks()
        {
            return db.FriendLinks;
        }

        // GET: api/FriendLinks/5
        [ResponseType(typeof(FriendLink))]
        public IHttpActionResult GetFriendLink(int id)
        {
            FriendLink friendLink = db.FriendLinks.Find(id);
            if (friendLink == null)
            {
                return NotFound();
            }

            return Ok(friendLink);
        }

        // PUT: api/FriendLinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFriendLink(int id, FriendLink friendLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friendLink.FriendLinkID)
            {
                return BadRequest();
            }

            db.Entry(friendLink).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendLinkExists(id))
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

        // POST: api/FriendLinks
        [ResponseType(typeof(FriendLink))]
        public IHttpActionResult PostFriendLink(FriendLink friendLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FriendLinks.Add(friendLink);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FriendLinkExists(friendLink.FriendLinkID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = friendLink.FriendLinkID }, friendLink);
        }

        // DELETE: api/FriendLinks/5
        [ResponseType(typeof(FriendLink))]
        public IHttpActionResult DeleteFriendLink(int id)
        {
            FriendLink friendLink = db.FriendLinks.Find(id);
            if (friendLink == null)
            {
                return NotFound();
            }

            db.FriendLinks.Remove(friendLink);
            db.SaveChanges();

            return Ok(friendLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendLinkExists(int id)
        {
            return db.FriendLinks.Count(e => e.FriendLinkID == id) > 0;
        }
    }
}