using GVC31G_HFT_2021221.Logic;
using GVC31G_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeLogic logic;
        public EmployeeController(IEmployeeLogic employeeLogic)
        {
            logic = employeeLogic;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return logic.Read(id);
        }
        [HttpPost]

        public void Post([FromBody] Employee value)
        {
            logic.Create(value);
        }
        [HttpPut("{id}")]

        public void Put([FromBody] Employee value)
        {
            logic.Update(value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }

    }
}
