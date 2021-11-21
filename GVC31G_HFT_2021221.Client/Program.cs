using GVC31G_HFT_2021221.Data;
using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using System;

namespace GVC31G_HFT_2021221
{
    class Program
    {
        static void Main(string[] args)
        {
            RestService restService = new RestService("http://localhost:51716");
            restService.Post<Employee>(new Employee()
            {
                Name = "Jóska Pista",
                ManagerId = 1
            },"employee");
            var employees = restService.Get<Employee>("employee");
            var managers = restService.Get<Manager>("managers");
        }
    }
}
