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
    public class BookingItemsController : ApiController
    {
        private centreBookingsDBEntities4 db = new centreBookingsDBEntities4();

        // GET: api/BookingItems
        public IQueryable<BookingItem> GetBookingItems()
        {
            return db.BookingItems;
        }

        // GET: api/BookingItems/5
        [ResponseType(typeof(BookingItem))]
        public IHttpActionResult GetBookingItem(int id)
        {
            BookingItem bookingItem = db.BookingItems.Find(id);
            if (bookingItem == null)
            {
                return NotFound();
            }

            return Ok(bookingItem);
        }

        // PUT: api/BookingItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookingItem(int id, BookingItem bookingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookingItem.Id)
            {
                return BadRequest();
            }

            db.Entry(bookingItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingItemExists(id))
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

        // POST: api/BookingItems
        [ResponseType(typeof(BookingItem))]
        public IHttpActionResult PostBookingItem(BookingItem bookingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookingItems.Add(bookingItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BookingItemExists(bookingItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bookingItem.Id }, bookingItem);
        }

        // DELETE: api/BookingItems/5
        [ResponseType(typeof(BookingItem))]
        public IHttpActionResult DeleteBookingItem(int id)
        {
            BookingItem bookingItem = db.BookingItems.Find(id);
            if (bookingItem == null)
            {
                return NotFound();
            }

            db.BookingItems.Remove(bookingItem);
            db.SaveChanges();

            return Ok(bookingItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingItemExists(int id)
        {
            return db.BookingItems.Count(e => e.Id == id) > 0;
        }
    }
}