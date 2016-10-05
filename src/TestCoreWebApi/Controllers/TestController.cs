using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCoreWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCoreWebApi.Controllers
{
    [Route("api/[controller]")]

    [Produces("application/json")]
    public class TestController : Controller
    {
        private Employee _context;
        public TestController(Employee context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpGet, Route("Getdata")]
       
        public IActionResult GetData()
        {
            int id = 0;
            List<EmployeeModel> employees1 = _context.empset.FromSql("exec dbo.empdal @Mode={0}", id).ToList();
            
            return Json(employees1);

        }

        [HttpGet, Route("Getdata1")]
        
        public JsonResult GetData1()
        {
            int id = 0;
            List<EmployeeModel> employees1 = _context.empset.FromSql("exec dbo.empdal @Mode={0}", id).ToList();

            return Json(employees1);

        }


        [HttpPost, Route("Insert")]

        public IActionResult Insert([FromBody] EmployeeModel emps)
        {
            var ReturnVal = new SqlParameter("ReturnVal", SqlDbType.NVarChar, 5000) { Direction = ParameterDirection.Output };
            SqlParameter Mode = new SqlParameter("@mode", SqlDbType.Int);
            Mode.Value = 1; Mode.Direction = ParameterDirection.Input;
            SqlParameter Ids = new SqlParameter("@id", SqlDbType.Int);
            Ids.Value = emps.Id;
            SqlParameter EmpName = new SqlParameter("@EmpName", SqlDbType.VarChar, 50);
            EmpName.Value = emps.EmpName;
            SqlParameter EmpLastName = new SqlParameter("@EmpLastName", SqlDbType.VarChar, 50);
            EmpLastName.Value = emps.EmpLastName;
            SqlParameter Gender = new SqlParameter("@Gender", SqlDbType.Bit);
            Gender.Value = emps.Gender;
            SqlParameter City = new SqlParameter("@City", SqlDbType.VarChar, 50);
            City.Value = emps.City;
            SqlParameter Designation = new SqlParameter("@Designation", SqlDbType.VarChar, 50);
            Designation.Value = emps.Designation;
            SqlParameter Salary = new SqlParameter("@Salary", SqlDbType.Money);
            Salary.Value = (emps.Salary == null ? null : (decimal?)(decimal)emps.Salary);

            var parameters = new List<object>();
            parameters.Add(Mode);
            parameters.Add(Ids);
            parameters.Add(EmpName);
            parameters.Add(EmpLastName);
            parameters.Add(Gender);
            parameters.Add(City);
            parameters.Add(Designation);
            parameters.Add(Salary);
            parameters.Add(ReturnVal);
            var resp = _context.Database.ExecuteSqlCommand("exec dbo.empdal  @mode=@mode,@id=@id,@ReturnVal=@ReturnVal output,@EmpName=@EmpName,@EmpLastName=@EmpLastName,@Gender=@Gender,@City=@City,@Designation=@Designation,@Salary=@Salary", parameters.ToArray());
            return Json(ReturnVal.Value);
        }

        [HttpPut, Route("Update")]
    
        public IActionResult Update([FromBody] EmployeeModel emps)
        {

            var ReturnVal = new SqlParameter("ReturnVal", SqlDbType.NVarChar, 5000) { Direction = ParameterDirection.Output };
            SqlParameter Mode = new SqlParameter("@mode", SqlDbType.Int);
            Mode.Value = 2; Mode.Direction = ParameterDirection.Input;

            SqlParameter Ids = new SqlParameter("@id", SqlDbType.Int);
            Ids.Value = emps.Id;
            SqlParameter EmpName = new SqlParameter("@EmpName", SqlDbType.VarChar, 50);
            EmpName.Value = emps.EmpName;
            SqlParameter EmpLastName = new SqlParameter("@EmpLastName", SqlDbType.VarChar, 50);
            EmpLastName.Value = emps.EmpLastName;
            SqlParameter Gender = new SqlParameter("@Gender", SqlDbType.Bit);
            Gender.Value = emps.Gender;
            SqlParameter City = new SqlParameter("@City", SqlDbType.VarChar, 50);
            City.Value = emps.City;
            SqlParameter Designation = new SqlParameter("@Designation", SqlDbType.VarChar, 50);
            Designation.Value = emps.Designation;
            SqlParameter Salary = new SqlParameter("@Salary", SqlDbType.Money);
            Salary.Value = emps.Salary;

            var parameters = new List<object>();
            parameters.Add(Mode);
            parameters.Add(Ids);
            parameters.Add(EmpName);
            parameters.Add(EmpLastName);
            parameters.Add(Gender);
            parameters.Add(City);
            parameters.Add(Designation);
            parameters.Add(Salary);
            parameters.Add(ReturnVal);
            var resp = _context.Database.ExecuteSqlCommand("exec dbo.empdal  @mode=@mode,@id=@id,@ReturnVal=@ReturnVal output,@EmpName=@EmpName,@EmpLastName=@EmpLastName,@Gender=@Gender,@City=@City,@Designation=@Designation,@Salary=@Salary", parameters.ToArray());


            return Json(ReturnVal.Value);
        }

        [HttpDelete, Route("Delete")]
   
        public IActionResult Delete([FromBody] EmployeeModel emps)
        {
            SqlParameter Mode = new SqlParameter("@mode", SqlDbType.Int);
            Mode.Value = 3; Mode.Direction = ParameterDirection.Input;

            SqlParameter Ids = new SqlParameter("@id", SqlDbType.Int);
            Ids.Value = emps.Id;

            SqlParameter ReturnVal = new SqlParameter("@ReturnVal", SqlDbType.NVarChar, 500);
            ReturnVal.Direction = ParameterDirection.Output;
            ReturnVal.Value = 0;
            var parameters = new List<object>();
            parameters.Add(Mode);
            parameters.Add(Ids);
            parameters.Add(ReturnVal);

            var resp = _context.Database.ExecuteSqlCommand("exec dbo.empdal  @mode=@mode,@id=@id,@ReturnVal=@ReturnVal output", parameters.ToArray());
            return Json(ReturnVal.Value);
        }
    }
}
