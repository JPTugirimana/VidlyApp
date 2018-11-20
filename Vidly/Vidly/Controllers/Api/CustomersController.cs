using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers(string querry = null)
        {
            var customersQuerry = _context.Customers
                                          .Include(c => c.MembershipType);

            if(!String.IsNullOrWhiteSpace(querry))
            {
                customersQuerry = customersQuerry.Where(c => c.Name.Contains(querry));
            }
            
            var customers = customersQuerry.ToList();

            // using Linq to map a list of customers to Customer Dtos
            return customers.Select(c => new CustomerDto()
            {
                Id = c.Id,
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsSubscribedToNewsletter = c.IsSubscribedToNewsletter,
                MembershipTypeId = c.MembershipTypeId,
                MembershipType = c.MembershipType.Name
            });
        }

        //Get /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var dto = new CustomerDto
            {
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter,
                MembershipTypeId = customer.MembershipTypeId
            };

            return Ok(dto);
        }

        // POST /api/customers
            
            /* COMMENTED OUT for a better implementation...to better apply RESTFul Api conventions */
            //[HttpPost]
            //public CustomerDto CreateCustomer(CustomerDto dto)
            //{
            //    if (!ModelState.IsValid)
            //        throw new HttpResponseException(HttpStatusCode.BadRequest);

            //    var customer = new Customer
            //    {
            //        Name = dto.Name,
            //        BirthDate = dto.BirthDate,
            //        IsSubscribedToNewsletter = dto.IsSubscribedToNewsletter,
            //        MembershipTypeId = dto.MembershipTypeId
            //    };

            //    _context.Customers.Add(customer);
            //    _context.SaveChanges();

            //    return dto;
            //}

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = new Customer
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                IsSubscribedToNewsletter = dto.IsSubscribedToNewsletter,
                MembershipTypeId = dto.MembershipTypeId
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            dto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), dto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto dto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = dto.Name;
            customerInDb.BirthDate = dto.BirthDate;
            customerInDb.IsSubscribedToNewsletter = dto.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = dto.MembershipTypeId;

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
