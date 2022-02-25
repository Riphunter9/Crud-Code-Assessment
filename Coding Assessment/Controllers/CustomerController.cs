using AspNetCoreHero.ToastNotification.Abstractions;
using Coding_Assessment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Coding_Assessment.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerDbContext _customerDbContext;
        private readonly INotyfService _notyf;

        public CustomerController(ILogger<CustomerController> logger,CustomerDbContext db,INotyfService notyf)
        {
            _logger = logger;
            _customerDbContext = db;
            _notyf = notyf;

        }
        
        public IActionResult Index()
        {
            var Customer = _customerDbContext.customers.OrderBy(n => n.CustomerName).ToList();
            return View(Customer);
        }
        [HttpGet] //start of customer add
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit",new Customer());
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var customers = _customerDbContext.customers.Find(id);
            return View(customers);
        }
        //got the views using id now editing begins.
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    _customerDbContext.customers.Add(customer);
                    _notyf.Success("Customer Added");
                }
                else if(customer.CustomerID==customer.CustomerID && customer.CustomerID>0)
                {
                    _customerDbContext.customers.Update(customer);
                     
                    _notyf.Success("Customer Updated");
                }
               _customerDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                return View(customer);
            }
        }
        //editing ends
        //delete begin
        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{

        //}
     
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerDbContext.customers.FindAsync(id);
            if (customer==null)
            {
                ViewBag.ErrorMessage = "Sorry that customer does not exist";
                return View("Index");
            }
            else
            {
                try
                {
                    _customerDbContext.customers.Remove(customer);
                    await _customerDbContext.SaveChangesAsync();

                }
                catch
                {
                    return View("Index");
                }
            }
           
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
