using GVC31G_HFT_2021221.Logic;
using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GVC31G_HFT_2021221.Test
{
    [TestFixture]
    public class TestFakeDb
    {
        class FakeEmployeeRepo : IEmployeeRepository
        {
            public void Create(Employee Employee)
            {
                throw new NotImplementedException();
            }

            public void Delete(int id)
            {
                throw new NotImplementedException();
            }

            public Employee Read(int id)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Employee> ReadAll()
            {
                Manager fakeManager = new Manager()
                {
                    Name = "Józsi",
                    Id = 2
                };
                Assignment fakeAssignment = new Assignment()
                {
                    Description = "Igen"
                };
                return new List<Employee>()
                {
                    new Employee()
                    {
                        Name = "ElsoEmp",
                        Manager = fakeManager,
                        Id= 1
                    },
                    new Employee()
                    {
                        Name = "MasodikEmp",
                        Id = 2
                    }
                }.AsQueryable();
            }

            public void Update(Employee Employee)
            {
                throw new NotImplementedException();
            }
        }
        EmployeeLogic emplogic;
        public TestFakeDb()
        {
            emplogic = new EmployeeLogic(new FakeEmployeeRepo());
            
            
        }
        [Test]
        public void ManagerCheck()
        {
            var employee = emplogic.Read(1);

            var res = emplogic.DoesEmployeeHasManager(employee);

            Assert.That(res, Is.True);
        }
        [TestCase(false)]
        [Test]
        public void CreateEmpTest(bool res)
        {
            if (res)
            {
                Assert.That(() => emplogic.Create(new Employee()
                {
                    Name = "ElsoEmp",
                    Id = 1
                }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => emplogic.Create(new Employee()
                {
                    Name = "ElsoEmp",
                    Id = 1
                }), Throws.Exception);
            }
            
        }
    }
}
