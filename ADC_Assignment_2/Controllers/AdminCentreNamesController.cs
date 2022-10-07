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
using ADC_Assignment_2.Models;

namespace ADC_Assignment_2.Controllers
{
    public class AdminCentreNamesController : ApiController
    {
        private centreBookingsDBEntities4 db = new centreBookingsDBEntities4();

        // GET: api/AdminCentreNames
        public IQueryable<AdminCentreName> GetAdminCentreNames()
        {
            return db.AdminCentreNames;
        }

        // GET: api/AdminCentreNames/5
        [ResponseType(typeof(AdminCentreName))]
        public IHttpActionResult GetAdminCentreName(int id)
        {
            AdminCentreName adminCentreName = db.AdminCentreNames.Find(id);
            if (adminCentreName == null)
            {
                return NotFound();
            }

            return Ok(adminCentreName);
        }

        // PUT: api/AdminCentreNames/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdminCentreName(int id, AdminCentreName adminCentreName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminCentreName.Id)
            {
                return BadRequest();
            }

            db.Entry(adminCentreName).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminCentreNameExists(id))
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

        // POST: api/AdminCentreNames
        [ResponseType(typeof(AdminCentreName))]
        public IHttpActionResult PostAdminCentreName(AdminCentreName adminCentreName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdminCentreNames.Add(adminCentreName);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdminCentreNameExists(adminCentreName.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = adminCentreName.Id }, adminCentreName);
        }

        // DELETE: api/AdminCentreNames/5
        [ResponseType(typeof(AdminCentreName))]
        public IHttpActionResult DeleteAdminCentreName(int id)
        {
            AdminCentreName adminCentreName = db.AdminCentreNames.Find(id);
            if (adminCentreName == null)
            {
                return NotFound();
            }

            db.AdminCentreNames.Remove(adminCentreName);
            db.SaveChanges();

            return Ok(adminCentreName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminCentreNameExists(int id)
        {
            return db.AdminCentreNames.Count(e => e.Id == id) > 0;
        }
    }
}