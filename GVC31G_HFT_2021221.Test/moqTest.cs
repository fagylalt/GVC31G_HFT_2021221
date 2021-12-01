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
                        Name ="Gizi",
                        DepartmentName="IT"
                        

                    },
                    new Manager()
                    {
                        Id = 2,
                        Name ="Béla",
                        DepartmentName="Vonat"
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
                        Name = "Géza",
                        ManagerId = 1,
                        Manager = new Manager(){Id = 1, Name="Jocó", DepartmentName="IT"},
                        CurrentTask = new List<Assignment>(){new Assignment() { Description="AAA", dueDate=DateTime.MaxValue, Employee = new Employee() { Name = "Jani", Id = 1, ManagerId = 1}, EmployeeId = 1, Id = 1 }, new Assignment() { Description = "AAAA", dueDate = DateTime.MinValue, Employee = new Employee() { Name = "Janika", Id = 2, ManagerId = 1 }, EmployeeId = 2, Id = 2 } }
                    },
                    new Employee()
                    {
                        Id = 2, 
                        Name="Árpád",
                        ManagerId = 2,
                        Manager = new Manager(){Id = 1, Name="Gábor", DepartmentName="OT"},
                        CurrentTask = new List<Assignment>(){new Assignment() { Description="AAA", dueDate=DateTime.MaxValue, Employee = new Employee() { Name = "Jani", Id = 1, ManagerId = 1}, EmployeeId = 1, Id = 1 } }

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
                        Description="A",
                        dueDate = DateTime.Now,
                        EmployeeId = 1,
                        Employee = new Employee(){Id = 1, Name = "Gézu", ManagerId = 1}

                    },
                    new Assignment()
                    {
                        Id = 2,
                        Description="AA",
                        dueDate = DateTime.MinValue,
                        EmployeeId = 1,
                        Employee = new Employee(){Id = 1, Name = "Gézu", ManagerId = 1}
                    },
                    new Assignment()
                    {
                        Id = 3,
                        Description="AAA",
                        dueDate=DateTime.MaxValue,
                        EmployeeId = 2,
                        Employee = new Employee(){Id = 2, Name = "István", ManagerId = 1}
                    }
                }.AsQueryable());
            a1 = new AssignmentLogic(mockAssignmentRepo.Object);

        }
        [TestCase("Feri", "IT", true)]
        [TestCase("Jani", null, false)]
        public void CreateManagerTest(string name, string depname, bool result)
        {
            if (result)
            {
                Assert.That(() => m1.Create(new Manager()
                {
                    Name = name,
                    DepartmentName = depname

                }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => m1.Create(new Manager()
                {
                    Name = name,
                    DepartmentName = depname
                }), Throws.Exception);
            }
        }
        [TestCase("Igen", "2022.01.01", true)]
        [TestCase("Nem", null, false)]
        public void CreateAssignmentTest(string desc, DateTime time, bool result)
        {
            if (result)
            {
                Assert.That(() => a1.Create(new Assignment()
                {
                    Description = desc,
                    dueDate = time

                }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => a1.Create(new Assignment()
                {
                    Description = desc,
                    dueDate = time

                }), Throws.Exception);
            }
        }
        [TestCase("Igen", true)]
        [TestCase(null, false)]
        public void CreateEmployeeTest(string name, bool result)
        {
            if (result)
            {
                Assert.That(() => e1.Create(new Employee()
                {
                    Name = name


                }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => e1.Create(new Employee()
                {
                    Name = name

                }), Throws.Exception);
            }
        }
        [Test]
        public void getEmployeeWithLatestAssignmentTest()
        {
            var result = a1.getEmployeeWithLatestAssignment();
            Assert.That(result.Id == 2);

        }
        [Test]
        public void getLongestAssignmentDescriptionTest()
        {
            var result = a1.getEmployeeWithLongestAssignmentDescription();
            Assert.That(result.Id == 2);
        }
        [Test]
        public void ListAllEmployeesWithTheirManagerTest()
        {
            var result = e1.ListAllEmployeesWithTheirManager();
            ;
            Assert.That(result.ToArray()[0].managerName =="Jocó");
        }
        [Test]
        public void whoHasTheMostAssignmentsTest()
        {

            var result = e1.whoHasTheMostAssignments();
            Assert.That(result == "Géza");

        }
        [Test]
        public void EmployeesMergedByManagersTest()
        {
            var result = e1.EmployeesMergedByManagers();
            Assert.That(result.ToArray()[0].managerName == "Jocó");
        }
        [TestCase(1, true)]
        [TestCase(-1, false)]
        public void ManagerReadTest(int id, bool result)
        {
            if (result)
            {
                Assert.That(() => m1.Read(id), Throws.Nothing);
            }
            else
            {
                Assert.That(() => m1.Read(id), Throws.Exception);
            }
        }
        [TestCase(1, true)]
        [TestCase(-1, false)]
        public void ManagerDeleteTest(int id, bool result)
        {
            if (result)
            {
                Assert.That(() => m1.Delete(id), Throws.Nothing);
            }
            else
            {
                Assert.That(() => m1.Delete(id), Throws.Exception);
            }
        }

    }
}
