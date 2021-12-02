﻿using ConsoleTools;
using GVC31G_HFT_2021221.Data;
using GVC31G_HFT_2021221.Logic;
using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using System;

namespace GVC31G_HFT_2021221
{
    class Program
    {
        static RestService restServ = new RestService("http://localhost:51716");
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(10000);
            var assignmentMenu = new ConsoleMenu(args, level: 1)
                .Add("Add assignment", () => AddAssignment())
                .Add("Read a single assignment", () => ReadSingleAssignment())
                .Add("Read all assignments", () => ReadAllAssignment())
                .Add("Update assignment", () => UpdateAssignment())
                .Add("Delete assignment", () => DeleteAssignment())
                .Add("Return to main menu", ())
            var managerMenu = new ConsoleMenu(args, level: 1)
                .Add("Add manager", () => AddManager())
                .Add("Read a single Manager", () => ReadSingleManager())
                .Add("Read all Managers", () => ReadAllManagers())
                .Add("Update Manager", () => UpdateManager())
                .Add("Delete Manager", () => DeleteManager());
            var employeMenu = new ConsoleMenu(args, level: 1)
                .Add("Add Employee", () => AddEmployee())
                .Add("Read a single Employee", () => ReadSingleEmployee())
                .Add("Read all Employees", () => ReadAllEmployees())
                .Add("Update Employee", () => UpdateEmployee())
                .Add("Delete Employee", () => DeleteEmployee());

            var statMenu = new ConsoleMenu(args, level: 1);
            ConsoleMenu mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Assignment related tasks: ", () => assignmentMenu.Show())
                .Add("Manager related tasks: ", () => managerMenu.Show())
                .Add("Employee related tasks: ", () => employeMenu.Show())
                .Add("Statistics menu", () => statMenu.Show());
            mainMenu.Show();

        }
        static void AddAssignment()
        {
            Console.WriteLine("The current assignments");
            var getAssignments = restServ.Get<Assignment>("assignments");
            getAssignments.ForEach(x => Console.WriteLine($"Assignment description: {x.Description} , assignment due date: {x.dueDate}, employee responsible: {x.Employee.Name} "));
            Assignment newAssignment = new Assignment();
            Console.WriteLine("Please provide aassignment description");
            newAssignment.Description = Console.ReadLine();
            Console.WriteLine("Please provide assignment due dat, format:\"2021.01.01\"");
            newAssignment.dueDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Please provide an employee number: ");
            newAssignment.EmployeeId = int.Parse(Console.ReadLine());
            restServ.Post<Assignment>(newAssignment, "assignment");
            Console.WriteLine("Successfully added");
            Console.ReadKey();
        }
        static void ReadAllAssignment()
        {
            var result = restServ.Get<Assignment>("assignment");
            result.ForEach(x => Console.WriteLine($"Assignment desc: {x.Description}, assigment duedate {x.dueDate}, employee responsible {x.Employee.Name} "));
            Console.ReadKey();

        }
        static void ReadSingleAssignment()
        {
            Console.WriteLine("Id of wanted task");
            var result = restServ.Get<Assignment>(Convert.ToInt32(Console.ReadLine()), "assignment");
            Console.WriteLine($"Assignment desc: {result.Description}, assigment duedate {result.dueDate}, employee responsible {result.Employee.Name} ");
            Console.ReadKey();
        }
        static void UpdateAssignment()
        {
            Console.WriteLine("Id of the task you'd want to update");
            int id = int.Parse(Console.ReadLine());
            Assignment newAssignment = new Assignment();
            Console.WriteLine("New description");
            newAssignment.Description = Console.ReadLine();
            Console.WriteLine("New deadline: format:\"2021.01.01\"");
            newAssignment.dueDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("New employee id");
            newAssignment.EmployeeId = int.Parse(Console.ReadLine());
            restServ.Put<Assignment>(newAssignment, "assignment");
            Console.WriteLine($"Successfuly updated {id}");
            Console.ReadKey();
        }
        static void DeleteAssignment()
        {
            Console.WriteLine("Id of the soon to be deleted task");
            int id = int.Parse(Console.ReadLine());
            restServ.Delete(id, "assignment");
            Console.WriteLine("Successfuly deleted the item");
            Console.ReadKey();
        }
        static void AddManager()
        {
            Console.WriteLine("The current managers");
            var getAssignments = restServ.Get<Manager>("managers");
            getAssignments.ForEach(x => Console.WriteLine($"Manager name: {x.Name} , department name: {x.DepartmentName}, employees under control: {x.Employees.Count} "));
            Manager newManager = new Manager();
            Console.WriteLine("Please provide manager name");
            newManager.Name = Console.ReadLine();
            Console.WriteLine("Please provide department name");
            newManager.DepartmentName = Console.ReadLine();
            restServ.Post<Manager>(newManager, "managers");
            Console.WriteLine("Successfully added");
            Console.ReadKey();
        }
        static void ReadAllManagers()
        {
            var result = restServ.Get<Manager>("managers");
            result.ForEach(x => Console.WriteLine($"Manager name: {x.Name}, department {x.DepartmentName}, employees controlled {x.Employees.Count} "));
            Console.ReadKey();
        }
        static void ReadSingleManager()
        {
            Console.WriteLine("Id of wanted manager");
            var result = restServ.Get<Manager>(Convert.ToInt32(Console.ReadLine()), "managers");
            Console.WriteLine($"Manager name: {result.Name}, department {result.DepartmentName}, employees controlled {result.Employees.Count} ");
            Console.ReadKey();
        }
        static void UpdateManager()
        {
            Console.WriteLine("Id of the manager you'd want to update");
            int id = int.Parse(Console.ReadLine());
            Manager newManager = new Manager();
            Console.WriteLine("New name");
            newManager.Name = Console.ReadLine();
            Console.WriteLine("New department name");
            newManager.DepartmentName = Console.ReadLine();
            restServ.Put<Manager>(newManager, "managers");
            Console.WriteLine($"Successfuly updated {id}");
            Console.ReadKey();
        }
        static void DeleteManager()
        {
            Console.WriteLine("Id of the soon to be deleted manager");
            int id = int.Parse(Console.ReadLine());
            restServ.Delete(id, "managers");
            Console.WriteLine("Successfuly deleted the item");
            Console.ReadKey();
        }
        static void AddEmployee()
        {
            Console.WriteLine("The current employees");
            var getEmployees = restServ.Get<Employee>("employees");
            getEmployees.ForEach(x => Console.WriteLine($"Employee name{x.Name}, employees managers name {x.Manager.Name}, tasks on hand {x.CurrentTask.Count} "));
            Employee newEmployee = new Employee();
            Console.WriteLine("Please provide Employee name");
            newEmployee.Name = Console.ReadLine();
            Console.WriteLine("Please provide Employee manager id");
            restServ.Post<Employee>(newEmployee, "employees");
            Console.WriteLine("Successfully added");
            Console.ReadKey();
        }
        static void ReadAllEmployees()
        {
            var result = restServ.Get<Employee>("employees");
            result.ForEach(x => Console.WriteLine($"Employee name{x.Name}, employees managers name {x.Manager.Name}, tasks on hand {x.CurrentTask.Count} "));
            Console.ReadKey();
        }
        static void ReadSingleEmployee()
        {
            Console.WriteLine("Id of wanted employee+");
            var result = restServ.Get<Employee>(Convert.ToInt32(Console.ReadLine()), "employees");
            Console.WriteLine($"Employee name{result.Name}, employees managers name {result.Manager.Name}, tasks on hand {result.CurrentTask.Count} ");
            Console.ReadKey();
        }
        static void UpdateEmployee()
        {
            Console.WriteLine("Id of the employee you'd want to update");
            int id = int.Parse(Console.ReadLine());
            Employee newEmployee = new Employee();
            Console.WriteLine("New name");
            newEmployee.Name = Console.ReadLine();
            Console.WriteLine("new manager id");
            newEmployee.ManagerId = int.Parse(Console.ReadLine());
            restServ.Put<Employee>(newEmployee, "employees");
            Console.WriteLine($"Successfuly updated {id}");
            Console.ReadKey();
        }
        static void DeleteEmployee()
        {
            Console.WriteLine("Id of the soon to be deleted employee");
            int id = int.Parse(Console.ReadLine());
            restServ.Delete(id, "employees");
            Console.WriteLine("Successfuly deleted the item");
            Console.ReadKey();
        }
        

    }
}
