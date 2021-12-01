using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Client
{
    class Menu
    {
        static RestService restserv;
        public Menu(RestService rest)
        {
            restserv = rest;
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option");
            Console.WriteLine("1) Register a manager, employee or assignment");
            Console.WriteLine("2) Read from manager, employee or assignment tables");
            Console.WriteLine("3) Update an existing employee, manager or assignment");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Create();
                    return true;
                case "2":
                    Read();
                    return true;
                case "3":
                    return false;
                default:
                    MainMenu();
                    return true;
            }

        }
        private static void Create()
        {
            Console.Clear();
            Console.WriteLine("Which table do you want to insert to? (manager, employee, assignment)");
            string table = Console.ReadLine();
            if(table =="manager" || table == "employee" || table == "assignment")
            {
                switch (table)
                {
                    case "manager":
                        Console.WriteLine("Name of the new manager:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Department of the manager:");
                        string department = Console.ReadLine();
                        restserv.Post(new Manager()
                        {
                            Name = name,
                            DepartmentName = department
                        },"manager");
                        Console.WriteLine("Item succesfully added");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "employee":
                        Console.WriteLine("Name of the new employee:");
                        string empname = Console.ReadLine();
                        Console.WriteLine("ID of the manager");
                        int managerid = Convert.ToInt32(Console.ReadLine());
                        restserv.Post(new Employee()
                        {
                            Name = empname,
                            ManagerId = managerid
                        }, "employee");
                        Console.WriteLine("Item succesfully added");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "assignment":
                        Console.WriteLine("Description of the assignment:");
                        string desc = Console.ReadLine();
                        Console.WriteLine("Due date of assignment (format: \"2020.01.01\"");
                        DateTime duedate = Convert.ToDateTime(Console.ReadLine());
                        restserv.Post(new Assignment()
                        {
                            Description = desc,
                            dueDate= duedate
                        }, "assignment");
                        Console.WriteLine("Item succesfully added");
                        Console.ReadKey();
                        MainMenu();
                        break;
                }
            }
        }
        private static void Read()
        {
            Console.Clear();
            Console.WriteLine("Which table should I display ? (manager,employee,assignment)");
            string table = Console.ReadLine();
            if (table == "employee" || table == "manager" || table == "assignment")
            {
                switch (table)
                {
                    case "employee":
                        var tempEmp = restserv.Get<Employee>("employee");
                        foreach (var item in tempEmp)
                        {
                            Console.WriteLine("ID: {0}, Name: {1} Manager's name: {2} ", item.Id, item.Name, item.Manager.Name);
                        }
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "manager":
                        var tempManager = restserv.Get<Manager>("manager");
                        foreach (var item in tempManager)
                        {
                            Console.WriteLine("ID: {0}, Name {1}, Department {2}", item.Id, item.Name, item.DepartmentName);
                        }
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "assignment":
                        var tempAssignment = restserv.Get<Assignment>("assignment");
                        foreach (var item in tempAssignment)
                        {
                            Console.WriteLine("ID: {0}, Description {1}, Due date: {2}", item.Id, item.Description, item.dueDate.ToString());
                        }
                        Console.ReadKey();
                        MainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect table name!");
                Console.ReadKey();
                MainMenu();
            }
        }
    }
}
