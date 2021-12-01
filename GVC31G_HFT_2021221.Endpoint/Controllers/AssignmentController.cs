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
    public class AssignmentController : ControllerBase
    {
        IAssignmentLogic logic;
        public AssignmentController(IAssignmentLogic AssignmentLogic)
        {
            logic = AssignmentLogic;
        }
        // GET: /assignment
        [HttpGet]
        public IEnumerable<Assignment> Get()
        {
            return logic.ReadAll();
        }
        // GET: /assignment/5
        [HttpGet("{id}")]
        public Assignment Get(int id)
        {
            return logic.Read(id);
        }
        // POST /assignment
        [HttpPost]
        public void Post([FromBody] Assignment value)
        {
            logic.Create(value);
        }
        // PUT /assignment
        [HttpPut]
        public void Put([FromBody] Assignment value)
        {
            logic.Update(value);
        }
        //DELETE assignment/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
