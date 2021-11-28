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
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IEmployeeLogic elogic;
        IAssignmentLogic alogic;
        
        public StatController(IEmployeeLogic elog, IAssignmentLogic alog)
        {
            elogic = elog;
            alogic = alog;
        }
        [HttpGet]
        // stat/latestassignment
        public Employee LatestAssignment()
        {
            return alogic.getLatestAssignment();
        }

        // stat/longestassignment
        [HttpGet]
        public Employee LongestAssignment()
        {
            return alogic.getLongestAssignment();
        }

        [HttpGet]
        // stat/listallemployees
        public IEnumerable<SelectAllEmp> ListAllEmployees()
        {
            return elogic.ListAllEmployees();
        }

        [HttpGet]
        // stat/employeesbymanagers
        public IEnumerable<SelectEmpCount> EmployeesByManagers()
        {
            return elogic.EmployeesbyManagers();
        }

        [HttpGet]
        // stat/mostassignments
        public string MostAssignments()
        {
            return elogic.whoHasMaxAssignment();
        }
    }
}
