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
        // stat/EmployeeWithLatestAssignment
        public string EmployeeWithLatestAssignment()
        {
            return alogic.getEmployeeWithLatestAssignment();
        }

        // stat/getEmployeeLongestAssignmentDescription
        [HttpGet]
        public string getEmployeeLongestAssignmentDescription()
        {
            return alogic.getEmployeeWithLongestAssignmentDescription();
        }

        [HttpGet]
        // stat/ListAllEmployeesWithTheirManagers
        public IEnumerable<SelectAllEmp> ListAllEmployeesWithTheirManagers()
        {
            return elogic.ListAllEmployeesWithTheirManager();
        }

        [HttpGet]
        // stat/employeesbymanagers
        public IEnumerable<SelectEmpCount> EmployeesMergedByManagers()
        {
            return elogic.EmployeesMergedByManagers();
        }

        [HttpGet]
        // stat/mostassignments
        public string whoHasTheMostAssignments()
        {
            return elogic.whoHasTheMostAssignments();
        }
    }
}
