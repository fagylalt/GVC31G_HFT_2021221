using GVC31G_HFT_2021221.Endpoint.Services;
using GVC31G_HFT_2021221.Logic;
using GVC31G_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        IHubContext<SignalRHub> hub;
        public EmployeeController(IEmployeeLogic employeeLogic , IHubContext<SignalRHub> hub)
        {
            logic = employeeLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("EmployeeCreated", value);
        }
        [HttpPut]

        public void Put([FromBody] Employee value)
        {
            logic.Update(value);
            this.hub.Clients.All.SendAsync("EmployeeUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var employeeToDelete = logic.Read(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("EmployeeDeleted", employeeToDelete);
        }

    }
}
