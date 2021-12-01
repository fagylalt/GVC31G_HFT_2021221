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
    public class ManagerController : ControllerBase
    {
        IManagerLogic logic;
        public ManagerController(IManagerLogic ManagerLogic)
        {
            logic = ManagerLogic;
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
        }
        [HttpPut]

        public void Put([FromBody] Manager value)
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
