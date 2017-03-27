﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC01.DataAccessLayer;

namespace MVC01.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }

        //public bool IsValidUser(UserDetails u)
        //{
        //    if (u.UserName == "Admin" && u.Password == "Admin")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "Aries" && u.Password == "Aries")
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }

		public void UploadEmployees(List<Employee> employees)
		{
			SalesERPDAL salesDal = new SalesERPDAL();
			salesDal.Employees.AddRange(employees);
			salesDal.SaveChanges();
		}
	}
}