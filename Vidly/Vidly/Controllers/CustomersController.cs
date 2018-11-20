using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult CustomerList()
        {
          /*  var customers = _context.Customers
                                    .Include(c => c.MembershipType)
                                    .ToList();
            return View(customers);*/ // commented out because now the list is coming from API

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType)
                                             .SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Create()
        {
            var customerViewModel = new CustomerViewModel
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View(customerViewModel);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerViewModel
            {
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter,
                MembershipType = customer.MembershipTypeId,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerViewModel newViewModel)
        {
            if(!ModelState.IsValid)
            {
                newViewModel.MembershipTypes = _context.MembershipTypes.ToList();

                return View("Edit", newViewModel);
            }

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newViewModel.Id);

            customer.Name = newViewModel.Name;
            customer.BirthDate = newViewModel.BirthDate;
            customer.IsSubscribedToNewsletter = newViewModel.IsSubscribedToNewsletter;
            customer.MembershipTypeId = newViewModel.MembershipType;

            _context.SaveChanges();


            return RedirectToAction("CustomerList","Customers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel newCustomer)
        {
            if(!ModelState.IsValid)
            {
                newCustomer.MembershipTypes = _context.MembershipTypes.ToList();
                return View("Create", newCustomer);        
            }
            
            var customer = new Customer
            {
                Name = newCustomer.Name,
                BirthDate = newCustomer.BirthDate,
                IsSubscribedToNewsletter = newCustomer.IsSubscribedToNewsletter,
                MembershipTypeId = newCustomer.MembershipType
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
        
            return RedirectToAction("CustomerList", "Customers");
        }
    }
}