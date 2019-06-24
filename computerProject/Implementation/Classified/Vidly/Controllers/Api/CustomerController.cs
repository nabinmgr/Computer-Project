using AutoMapper;
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
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        // Get /api/customers
        [AllowAnonymous]
        public IHttpActionResult GetCustomers()
        {
            var customerDtos=_context.Customer.
                Include(c=>c.MembershipType).
                ToList().Select(Mapper.Map<Customer,CustomerDto>);
            return Ok(customerDtos);
        }
        //filter results
        //public IHttpActionResult GetCustomers(string query=null)
        //{
        //    var customerQuery = _context.Customer.
        //         Include(c => c.MembershipType);

        //    if (!String.IsNullOrWhiteSpace(query))
        //    {
        //       customerQuery = _context.Customer.
        //      Include(c => c.MembershipType).Where(c => c.Name.Contains(query));
        //    }

        //    var customerDtos = customerQuery.
        //        ToList().Select(Mapper.Map<Customer, CustomerDto>);
        //    return Ok(customerDtos);
        //}
        public IHttpActionResult GetCustomers(string query)
        {
            var customerQuery = _context.Customer.
                Include(c => c.MembershipType).Where(c => c.Name.Contains(query));

             var customerDtos = customerQuery.
                ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }
        
        //POST /api/customers
        //[HttpPost]
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadGateway);
        //    var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
        //    _context.Customer.Add(customer);
        //    _context.SaveChanges();

        //    customerDto.Id = customer.Id;
        //    return customerDto;
        //}

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customer.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri+"/"+customer.Id),customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadGateway);

            var customerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
            return Ok();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customer.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customer.Remove(customer);
            _context.SaveChanges();

            return Ok();
        }
    }
}
