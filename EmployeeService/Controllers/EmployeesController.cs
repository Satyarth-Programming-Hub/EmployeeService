using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        //GET : api/Employees
        public HttpResponseMessage Get() 
        {
            List<Employee> employees = new List<Employee>();

            using (HRDBContext dbContext = new HRDBContext())
            {
                employees = dbContext.Employees.ToList();
                if (employees.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please try again later");
                }

                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
        }

        //GET : api/Employees/id
        public HttpResponseMessage Get(int id)
        {
            using (HRDBContext dbContext = new HRDBContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in our database");
                }
            }
        }

        //POST : api/Employees/
        public HttpResponseMessage Post(Employee employee) 
        {
            using (HRDBContext dbContext = new HRDBContext())
            {
                if (employee != null)
                {
                    dbContext.Employees.Add(employee);
                    dbContext.SaveChanges(); // this line of code is needed to save the data in the database.
                    return Request.CreateResponse(HttpStatusCode.Created, employee);
                }
                else 
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Please provide the required infomration");
                }
            }
        
        }

        //PUT : api/Employees/id
        public HttpResponseMessage Put(int id, Employee employee) 
        {
            using (HRDBContext dbContext = new HRDBContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    emp.FirstName = employee.FirstName;
                    emp.LastName = employee.LastName;
                    emp.Email = employee.Email;
                    emp.Gender = employee.Gender;
                    emp.City = employee.City;

                    dbContext.SaveChanges(); //this line is need to save data in th database
                    
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in our database");
                }
            }

        }

        //DELETE : api/Employees/id
        public HttpResponseMessage Delete(int id) 
        {
            using (HRDBContext dbContext = new HRDBContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    dbContext.Employees.Remove(emp);
                    dbContext.SaveChanges(); // this line is need to make changes in the database

                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in our database");
                }
            }
        }

    }
}
