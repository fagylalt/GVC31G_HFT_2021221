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
            Manager fakeManager2 = new Manager()
            {
                Id = 2,
                Name = "Gizi",
                DepartmentName = "OT"
            };
            Employee fakeEmployee = new Employee()
            {
                Id = 1,
                Name = "Józsi",
                Manager = fakeManager
            };
            Employee fakeEmployee2 = new Employee()
            {
                Id = 2,
                Name = "Ádám",
                Manager = fakeManager2
            };
            Assignment fakeAssignment = new Assignment()
            {
                Id = 1,
                Description = "Vonatfék csere",
                dueDate = DateTime.Now,
                Employee = fakeEmployee,
                EmployeeId = fakeEmployee.Id

            };
            Assignment fakeAssignment2 = new Assignment()
            {
                Id = 2,
                Description = "Vonatkerék levegőfúvás",
                dueDate = Convert.ToDateTime("2022.01.01"),
                Employee = fakeEmployee2,
                EmployeeId = fakeEmployee2.Id
            };
            Assignment fakeAssignment3 = new Assignment()
            {
                Id = 3,
                Description = "igen teszt",
                dueDate = DateTime.MaxValue,
                Employee = fakeEmployee2,
                EmployeeId = fakeEmployee2.Id
                
            };
            Mock<IManagerRepository> mockManagerRepo = new Mock<IManagerRepository>();
            Mock<IEmployeeRepository> mockEmployeeRepo = new Mock<IEmployeeRepository>();
            Mock<IAssignmentRepository> mockAssignmentRepo = new Mock<IAssignmentRepository>();
            mockManagerRepo.Setup((t) => t.Create(It.IsAny<Manager>()));
            mockManagerRepo.Setup((t) => t.ReadAll()).Returns(
                new List<Manager>()
                {
                    fakeManager,
                    fakeManager2

                }.AsQueryable());
            m1 = new ManagerLogic(mockManagerRepo.Object);
            mockEmployeeRepo.Setup((t) => t.Create(It.IsAny<Employee>()));
            mockEmployeeRepo.Setup((t) => t.ReadAll()).Returns(
                new List<Employee>()
                {
                    fakeEmployee,
                    fakeEmployee2
                }.AsQueryable());
            e1 = new EmployeeLogic(mockEmployeeRepo.Object);
            mockAssignmentRepo.Setup((t) => t.Create(It.IsAny<Assignment>()));
            mockAssignmentRepo.Setup((t) => t.Readall()).Returns(
                new List<Assignment>()
                {
                    fakeAssignment,
                    fakeAssignment2,
                    fakeAssignment3
                }.AsQueryable());
            a1 = new AssignmentLogic(mockAssignmentRepo.Object);


        }
        [TestCase("Feri", "IT", true)]
        //[TestCase("Jani", null, false)]
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
        //[TestCase("Nem", null, false)]
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
       // [TestCase(null, false)]
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
            Assert.That(result.ToArray()[0].managerName =="Erzsi");
        }
        [Test]
        public void whoHasTheMostAssignmentsTest()
        {

            var result = e1.whoHasTheMostAssignments();
            Assert.That(result == "Ádám");

        }
        [Test]
        public void EmployeesMergedByManagersTest()
        {
            var result = e1.EmployeesMergedByManagers();
            Assert.That(result.ToArray()[0].managerName == "Erzsi");
        }

    }
}
