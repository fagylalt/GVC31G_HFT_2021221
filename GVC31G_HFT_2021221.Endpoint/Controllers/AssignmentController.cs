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
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        IAssignmentLogic logic;
        public AssignmentController(IAssignmentLogic AssignmentLogic)
        {
            logic = AssignmentLogic;
        }

        [HttpGet]
        public IEnumerable<Assignment> Get()
        {
            return logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Assignment Get(int id)
        {
            return logic.Read(id);
        }
        [HttpPost]

        public void Post([FromBody] Assignment value)
        {
            logic.Create(value);
        }
        [HttpPut("{id}")]

        public void Put([FromBody] Assignment value)
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
