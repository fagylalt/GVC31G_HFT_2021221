using GVC31G_HFT_2021221.Data;
using GVC31G_HFT_2021221.Repository;
using System;

namespace GVC31G_HFT_2021221
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new TrainDbContext();
            ;
            Console.WriteLine("Hello World!");
            EmployeeRepository ep = new EmployeeRepository(ctx);
            ManagerRepository rp = new ManagerRepository(ctx);
            var readall = rp.ReadAll();
            var readall2 = ep.ReadAll();
            ;
        }
    }
}
