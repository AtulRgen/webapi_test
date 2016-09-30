using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCoreWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;


namespace TestCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    
    public class ValuesController : Controller
    {
        private Employee _context;
        public ValuesController(Employee context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "Welcome..!!";
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public List<EmployeeModel> Get(int id)
        {

            List<EmployeeModel> employees1 = _context.empset.FromSql("exec dbo.empdal @Mode={0}", id).ToList();

            return employees1;
        }
        public void Post([FromBody]string value)
        {
        }
        // POST api/values
        [HttpPost, Route("ImportEmployee")]
        public string ImportEmployee([FromBody]EmployeeModel emps)
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
            //dr.IsNull("s_In_File") ? null : (bool?)(bool)dr["s_In_File"],

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


            return Convert.ToString(ReturnVal.Value);

        }

        // PUT api/values/5                                             		
        [HttpPut, Route("UpdateEmployee")]
        public string UpdateEmployee([FromBody]EmployeeModel emps)
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


            return Convert.ToString(ReturnVal.Value);
        }

        // DELETE api/values/5
        [HttpDelete, Route("DeleteEmployee")]
        //[HttpPost("DeleteEmployee"), Route("DeleteEmployee")]
        public string DeleteEmployee(int id)
        {

            SqlParameter Mode = new SqlParameter("@mode", SqlDbType.Int);
            Mode.Value = 3; Mode.Direction = ParameterDirection.Input;

            SqlParameter Ids = new SqlParameter("@id", SqlDbType.Int);
            Ids.Value = id;

            SqlParameter ReturnVal = new SqlParameter("@ReturnVal", SqlDbType.NVarChar, 500);
            ReturnVal.Direction = ParameterDirection.Output;
            ReturnVal.Value = 0;
            var parameters = new List<object>();
            parameters.Add(Mode);
            parameters.Add(Ids);
            parameters.Add(ReturnVal);

            var resp = _context.Database.ExecuteSqlCommand("exec dbo.empdal  @mode=@mode,@id=@id,@ReturnVal=@ReturnVal output", parameters.ToArray());




            return Convert.ToString(ReturnVal.Value);
        }

        [HttpGet, Route("Test")]
        public string Test()
        {
            SqlParameter operatorID = new SqlParameter("@x1", SqlDbType.VarChar, 50);
            operatorID.Direction = ParameterDirection.Output;
            SqlParameter operatorCode = new SqlParameter("@x2", SqlDbType.Int);
            operatorCode.Value = 2;
            operatorCode.Direction = ParameterDirection.Input;

            var parameters = new List<object>();
            parameters.Add(operatorID);
            parameters.Add(operatorCode);

            var resp = _context.Database.ExecuteSqlCommand("exec dbo.test @x1 output,@x2", parameters.ToArray());


            return "hurrey" + operatorID.Value;
        }


    }
}



//Limitations


//Paramter
//is their is null row  then result will not show
