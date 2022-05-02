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
    public class ManagerController : ControllerBase
    {
        IManagerLogic logic;
        IHubContext<SignalRHub> hub;
        public ManagerController(IManagerLogic ManagerLogic, IHubContext<SignalRHub> hub)
        {
            logic = ManagerLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Manager> Get()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Manager Get(int id)
        {
            return logic.Read(id);
        }
        [HttpPost]

        public void Post([FromBody] Manager value)
        {
            logic.Create(value);
            this.hub.Clients.All.SendAsync("ManagerCreated", value);
        }
        [HttpPut]

        public void Put([FromBody] Manager value)
        {
            ;
            logic.Update(value);
            this.hub.Clients.All.SendAsync("ManagerUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var managerToDelete = logic.Read(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("ManagerDeleted", managerToDelete);
        }
    }
}
