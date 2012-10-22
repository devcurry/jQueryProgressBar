using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MVC_JQuery_UI.Models;
using System.Diagnostics;

namespace MVC_JQuery_UI.Controllers
{
    public class OrdersController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET api/Orders
        public IEnumerable<Order> GetOrders()
        {
            Debug.Print(db.Orders.Count<Order>().ToString());
            return db.Orders.AsEnumerable();
        }

        // GET api/Orders/5
        public Order GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return order;
        }

        // PUT api/Orders/5
        public HttpResponseMessage PutOrder(int id, Order order)
        {
            if (ModelState.IsValid && id == order.OrderID)
            {
                db.Entry(order).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Orders
        public HttpResponseMessage PostOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = order.OrderID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Orders/5
        public HttpResponseMessage DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Orders.Remove(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, order);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}