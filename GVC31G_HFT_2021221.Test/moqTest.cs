using GVC31G_HFT_2021221.Logic;
using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Test
{
    [TestFixture]
    public class moqTest
    {
        ManagerLogic m1;
        AssignmentLogic a1;
        EmployeeLogic e1;
        public moqTest()
        {
            Manager fakeManager = new Manager()
            {
                Id = 1,
                Name = "Erzsi",
                DepartmentName = "IT"
            };
            Assignment fakeAssignment = new Assignment()
            {
                Id = 1,
                Description = "Vonatfék csere",
                dueDate = DateTime.Now
            };
            Employee fakeEmployee = new Employee()
            {
                Id = 1,
                Name = "Józsi",
                Manager = fakeManager
            };
            Mock<IManagerRepository> mockManagerRepo = new Mock<IManagerRepository>();
            Mock<IEmployeeRepository> mockEmployeeRepo = new Mock<IEmployeeRepository>();
            Mock<IAssignmentRepository> mockAssignmentRepo = new Mock<IAssignmentRepository>();
            mockManagerRepo.Setup((t) => t.Create(It.IsAny<Manager>()));
            mockManagerRepo.Setup((t) => t.ReadAll()).Returns(
                new List<Manager>()
                {
                    new Manager()
                    {
                        Id = 1,
                        Name = "Erzsi",
                        DepartmentName = "IT"
                    },
                    new Manager()
                    {
                        Id = 2,
                        Name = "Béla",
                        DepartmentName ="OT"
                    }

                }.AsQueryable());
            m1 = new ManagerLogic(mockManagerRepo.Object);
            mockEmployeeRepo.Setup((t) => t.Create(It.IsAny<Employee>()));
            mockEmployeeRepo.Setup((t) => t.ReadAll()).Returns(
                new List<Employee>()
                {
                    new Employee()
                    {
                        Id = 1,
                        Manager =fakeManager,
                        Name="Ádám"
                    },
                    new Employee()
                    {
                        Id = 2,
                        Manager = fakeManager,
                        Name="Béla"
                    }
                }.AsQueryable());
            e1 = new EmployeeLogic(mockEmployeeRepo.Object);
            mockAssignmentRepo.Setup((t) => t.Create(It.IsAny<Assignment>()));
            mockAssignmentRepo.Setup((t) => t.Readall()).Returns(
                new List<Assignment>()
                {
                    new Assignment()
                    {
                        Id = 1,
                        Description = "aaaa",
                        dueDate = Convert.ToDateTime("2021.11.16")
                    },
                    new Assignment()
                    {
                        Id = 2,
                        Description = "bbbb",
                        dueDate = Convert.ToDateTime("2021.11.30")
                    }
                }.AsQueryable());
            a1 = new AssignmentLogic(mockAssignmentRepo.Object);


        }
        
    }
}
