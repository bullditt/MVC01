using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC01.Models;
using System.Web.Mvc.Ajax;
using MVC01.ViewModels;
using MVC01.Filters;

namespace MVC01.Controllers
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return this.CustomerName + "|" + this.Address;
        }
    }

    public class EmployeeController : Controller
    {
        public string GetString()
        {
            return "Hello World is old now. It&rsquo;s time for wassup bro ;)";
        }

        public Customer GetCustomer()
        {
            Customer c = new Customer();
            c.CustomerName = "Customer 1";
            c.Address = "Address1";
            return c;
        }

        [Authorize]
		[HeaderFooterFilter]
		public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();

            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel vmEmp = new EmployeeViewModel();
                vmEmp.EmployeeName = emp.FirstName + " " + emp.LastName;
                vmEmp.Salary = emp.Salary.ToString();//.ToString("C");
                if (emp.Salary > 15000)
                {
                    vmEmp.SalaryColor = "yellow";
                }
                else
                {
                    vmEmp.SalaryColor = "green";
                }
                empViewModels.Add(vmEmp);
            }
            employeeListViewModel.Employees = empViewModels;
            //employeeListViewModel.UserName = "Admin";-->Remove this line -->Change1

            return View("Index", employeeListViewModel);//-->Change View Name -->Change 2
        }

        [AdminFilter]
		[HeaderFooterFilter]
		public ActionResult AddNew()
        {
			CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
			return View("CreateEmployee", employeeListViewModel);
		}

        [AdminFilter]
		[HeaderFooterFilter]
		public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        if (e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }
						return View("CreateEmployee", vm); // Day 4 Change - Passing e here
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}