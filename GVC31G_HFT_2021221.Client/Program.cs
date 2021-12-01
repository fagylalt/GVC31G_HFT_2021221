using GVC31G_HFT_2021221.Client;
using GVC31G_HFT_2021221.Data;
using GVC31G_HFT_2021221.Logic;
using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using System;

namespace GVC31G_HFT_2021221
{
    class Program
    {
        static void Main(string[] args)
        {
            //RestService restServ = new RestService("http://localhost:51716");
            TrainDbContext db = new TrainDbContext();
            EmployeeRepository emprep = new EmployeeRepository(db);
            EmployeeLogic emplogic = new EmployeeLogic(emprep);
            var result = emplogic.ListAllEmployeesWithTheirManager();
            ;
        }

    }
}
