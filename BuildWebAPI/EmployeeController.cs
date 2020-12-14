using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2_WebAPI.Models;

namespace WebApplication2_WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        Employee[] employees = new Employee[]{
         new Employee { id = 1, name = "Mark",  age = 30 },
         new Employee { id = 2, name = "Allan", age = 35 },
         new Employee { id = 3, name = "Johny", age = 21 }
        };

        /*
        //attribute routing - enabled in WebApiConfig class
        [Route("api/employee/details")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            return employees;
        }
        
        [Route("api/employee/details/{id}")]
        public IHttpActionResult GetEmployee(int id)
        {
            var employee = employees.FirstOrDefault((p) => p.id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //parameter binding - primitive->from uri
        //                  - complex->from body

        // to force opp rules i.e primitive from body & complex from uri
        public Employee Post([FromUri] Employee emp) //if above att. route not given,then this method will 
            //not be executed.Instead,GetEmployee method with id executes even though all 3 params are passed 
            //in query string.If both are get,either should exist.
        {
            return emp;
        }
               
        public Employee Get([FromBody] string name)
        {
            var employee = employees.FirstOrDefault((p) => p.name == name);
            return employee;
        }
        */

        //GET Methods
        [Route("api/employee/details")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            return employees;
        }

        [Route("api/employee/{id}")]
        public IHttpActionResult Get(int id)
        {
            var employee = employees.FirstOrDefault((p) => p.id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [Route("api/employee/details/{name}")]
        public IHttpActionResult Get(string name)
        {
            var employee = employees.FirstOrDefault((p) => p.name == name);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //POST Method
        [Route("api/employee/insert")]
        public IEnumerable<Employee> Post(Employee emp) 
        {
            int len = employees.GetLength(0);            
            try
            {
                employees.SetValue( new Employee { id = emp.id, name = emp.name, age = emp.age } , len );
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message); 
            }
            return GetAllEmployees();
        }

        //PUT Method
        [Route("api/employee/update/{name}")]
        public IEnumerable<Employee> Put(Employee emp , string name)
        {
            var employee = employees.FirstOrDefault((p) => p.name == name);
            employee.id = emp.id;
            employee.name = emp.name;
            employee.age = emp.age;
            return GetAllEmployees();
        }

        //DELETE Method
        [Route("api/employee/delete/{name}")]
        public IEnumerable<Employee> Delete(string name)
        {
            var employee = employees.FirstOrDefault((p) => p.name == name);
            //assume to be deleted , as it's array
            employee.id = 0;
            employee.name = null;
            employee.age = 0;
            return GetAllEmployees();
        }
    }
}
