using CarDB.Client;
using ConsoleTools;
using GVC31G_HFT_2021221.Logic;
using GVC31G_HFT_2021221.Models;
using System;
using System.Collections.Generic;

namespace GVC31G_HFT_2021221
{
    class Program
    {
        static RestService restServ = new RestService("http://localhost:51716");
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(5000);
            var assignmentMenu = new ConsoleMenu(args, level: 1)
                .Add("Add assignment", () => AddAssignment())
                .Add("Read a single assignment", () => ReadSingleAssignment())
                .Add("Read all assignments", () => ReadAllAssignment())
                .Add("Update assignment", () => UpdateAssignment())
                .Add("Delete assignment", () => DeleteAssignment())
                .Add("BACK", ConsoleMenu.Close);


            var managerMenu = new ConsoleMenu(args, level: 1)
                .Add("Add manager", () => AddManager())
                .Add("Read a single Manager", () => ReadSingleManager())
                .Add("Read all Managers", () => ReadAllManagers())
                .Add("Update Manager", () => UpdateManager())
                .Add("Delete Manager", () => DeleteManager())
                .Add("BACK", ConsoleMenu.Close);
            var employeMenu = new ConsoleMenu(args, level: 1)
                .Add("Add Employee", () => AddEmployee())
                .Add("Read a single Employee", () => ReadSingleEmployee())
                .Add("Read all Employees", () => ReadAllEmployees())
                .Add("Update Employee", () => UpdateEmployee())
                .Add("Delete Employee", () => DeleteEmployee())
                .Add("BACK", ConsoleMenu.Close);

            var statMenu = new ConsoleMenu(args, level: 1)
                .Add("Employee's name with the least urgent assignment", () => ListEmployeeWithLatestAssignment())
                .Add("Employee with the longest assignment description", () => ListEmployeeWithLongestAssignmentDescription())
                .Add("Employees with their managers", () => ListAllEmployeesWithTheirManager())
                .Add("Employees merged with their managers: ", () => ListEmployeesMergedwithTheirManagers())
                .Add("Employee with the most assignments", () => ListMostAssignments())
                .Add("BACK", ConsoleMenu.Close);
            ConsoleMenu mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Assignment related tasks: ", () => assignmentMenu.Show())
                .Add("Manager related tasks: ", () => managerMenu.Show())
                .Add("Employee related tasks: ", () => employeMenu.Show())
                .Add("Statistics menu", () => statMenu.Show())
                .Add("QUIT", ConsoleMenu.Close);
            mainMenu.Show();

        }
        static void AddAssignment()
        {
            Console.WriteLine("The current assignments");
            var getAssignments = restServ.Get<Assignment>("assignments");
            getAssignments.ForEach(x => Console.WriteLine($"Id:{x.Id} Assignment description: {x.Description} , assignment due date: {x.dueDate}, employee responsible: {x.Employee.Name} "));
            Assignment newAssignment = new Assignment();
            Console.WriteLine("Please provide assignment description");
            newAssignment.Description = Console.ReadLine();
            Console.WriteLine("Please provide assignment due dat, format:\"2021.01.01\"");
            newAssignment.dueDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Please provide an employee number: ");
            newAssignment.EmployeeId = int.Parse(Console.ReadLine());
            restServ.Post<Assignment>(newAssignment, "assignment");
            Console.WriteLine("Successfully added");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ReadAllAssignment()
        {
            var result = restServ.Get<Assignment>("assignment");
            result.ForEach(x => Console.WriteLine($"ID {x.Id} Assignment desc: {x.Description}, assigment duedate {x.dueDate}, employee responsible {x.EmployeeId} "));
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();

        }
        static void ReadSingleAssignment()
        {
            Console.WriteLine("Id of wanted task");
            var result = restServ.Get<Assignment>(Convert.ToInt32(Console.ReadLine()), "assignment");
            Console.WriteLine($"Assignment desc: {result.Description}, assigment duedate {result.dueDate}, employee responsible {result.EmployeeId} ");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void UpdateAssignment()
        {
            Console.WriteLine("Id of the task you'd want to update");
            int id = int.Parse(Console.ReadLine());
            Assignment newAssignment = new Assignment();
            newAssignment.Id = id;
            Console.WriteLine("New description");
            newAssignment.Description = Console.ReadLine();
            Console.WriteLine("New deadline: format:\"2021.01.01\"");
            newAssignment.dueDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("New employee id");
            newAssignment.EmployeeId = int.Parse(Console.ReadLine());
            restServ.Put<Assignment>(newAssignment, "assignment");
            Console.WriteLine($"Successfuly updated {id}");
            Console.WriteLine("Press a key to return to menu");
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
            var getAssignments = restServ.Get<Manager>("manager");
            getAssignments.ForEach(x => Console.WriteLine($"Manager name: {x.Name} , department name: {x.DepartmentName}, employees under control: {x.Employees.Count} "));
            Manager newManager = new Manager();
            Console.WriteLine("Please provide manager name");
            newManager.Name = Console.ReadLine();
            Console.WriteLine("Please provide department name");
            newManager.DepartmentName = Console.ReadLine();
            restServ.Post<Manager>(newManager, "manager");
            Console.WriteLine("Successfully added");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ReadAllManagers()
        {
            var result = restServ.Get<Manager>("manager");
            result.ForEach(x => Console.WriteLine($"Manager name: {x.Name}, department {x.DepartmentName}, employees controlled {x.Employees.Count} "));
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ReadSingleManager()
        {
            Console.WriteLine("Id of wanted manager");
            var result = restServ.Get<Manager>(Convert.ToInt32(Console.ReadLine()), "manager");
            Console.WriteLine($"Manager name: {result.Name}, department {result.DepartmentName}, employees controlled {result.Employees.Count} ");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void UpdateManager()
        {
            Console.WriteLine("Id of the manager you'd want to update");
            int id = int.Parse(Console.ReadLine());
            Manager oldmanager = restServ.Get<Manager>(id, "manager");
            Manager newManager = new Manager();
            newManager.Id = id;
            Console.WriteLine("New name");
            newManager.Name = Console.ReadLine();
            Console.WriteLine("New department name");
            newManager.DepartmentName = Console.ReadLine();
            newManager.Employees = oldmanager.Employees;
            restServ.Put<Manager>(newManager, "manager");
            Console.WriteLine($"Successfuly updated {newManager.Name}");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void DeleteManager()
        {
            Console.WriteLine("Id of the soon to be deleted manager");
            int id = int.Parse(Console.ReadLine());
            restServ.Delete(id, "manager");
            Console.WriteLine("Successfuly deleted the item");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void AddEmployee()
        {
            Console.WriteLine("The current employees");
            var getEmployees = restServ.Get<Employee>("employees");
            getEmployees.ForEach(x => Console.WriteLine($"Employee name {x.Name}, employees managers name {x.Manager.Name}, tasks on hand {x.CurrentTask.Count} "));
            Employee newEmployee = new Employee();
            Console.WriteLine("Please provide Employee name");
            newEmployee.Name = Console.ReadLine();
            Console.WriteLine("Please provide Employee manager id");
            newEmployee.ManagerId = int.Parse(Console.ReadLine());
            restServ.Post<Employee>(newEmployee, "employee");
            Console.WriteLine("Successfully added");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ReadAllEmployees()
        {
            var result = restServ.Get<Employee>("employee");
            result.ForEach(x => Console.WriteLine($"Employee name {x.Name}, employees manager's id {x.ManagerId}, tasks on hand {x.CurrentTask.Count} "));
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ReadSingleEmployee()
        {
            Console.WriteLine("Id of wanted employee");
            var result = restServ.Get<Employee>(Convert.ToInt32(Console.ReadLine()), "employee");
            Console.WriteLine($"Employee name{result.Name}, employees manager's id {result.Manager.Name}, tasks on hand {result.CurrentTask.Count} ");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void UpdateEmployee()
        {
            Console.WriteLine("Id of the employee you'd want to update");
            int id = int.Parse(Console.ReadLine());
            Employee oldEmployee = restServ.Get<Employee>(id, "employee");
            Employee newEmployee = new Employee();
            newEmployee.CurrentTask = oldEmployee.CurrentTask;
            newEmployee.Id = id;
            Console.WriteLine("New name");
            newEmployee.Name = Console.ReadLine();
            Console.WriteLine("new manager id");
            newEmployee.ManagerId = int.Parse(Console.ReadLine());
            restServ.Put<Employee>(newEmployee, "employee");
            Console.WriteLine($"Successfuly updated {id}");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void DeleteEmployee()
        {
            Console.WriteLine("Id of the soon to be deleted employee");
            int id = int.Parse(Console.ReadLine());
            restServ.Delete(id, "employee");
            Console.WriteLine("Successfuly deleted the item");
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ListEmployeeWithLatestAssignment()
        {
            Console.WriteLine("The employee with the latest assignment is");
            var res = restServ.GetSingle<string>("stat/EmployeeWithLatestAssignment");
            Console.WriteLine(res);
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ListEmployeeWithLongestAssignmentDescription()
        {
            Console.WriteLine("The employee with the latest assignment is");
            var res = restServ.GetSingle<string>("stat/getEmployeeLongestAssignmentDescription");
            Console.WriteLine(res);
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ListAllEmployeesWithTheirManager()
        {
            var res = restServ.Get<SelectAllEmp>("stat/ListAllEmployeesWithTheirManagers");
            res.ForEach(x => Console.WriteLine($"Manager name: {x.managerName}, Employee's name: {x.name} "));
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ListEmployeesMergedwithTheirManagers()
        {
            var res = restServ.Get<SelectEmpCount>("stat/EmployeesMergedByManagers");
            res.ForEach(x => Console.WriteLine($"{x.managerName}, {x.count}"));
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
        static void ListMostAssignments()
        {
            var res = restServ.GetSingle<string>("stat/whohasthemostassignments");
            Console.WriteLine(res);
            Console.WriteLine("Press a key to return to menu");
            Console.ReadKey();
        }
    }
}
