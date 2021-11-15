﻿using GVC31G_HFT_2021221.Models;
using GVC31G_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Logic
{
    class AssignmentLogic : IAssignmentLogic
    {
        IAssignmentRepository assignmentRepo;
        public AssignmentLogic(IAssignmentRepository repo)
        {
            this.assignmentRepo = repo;
        }
        public void Create(Assignment Assignment)
        {
            assignmentRepo.Create(Assignment);
        }

        public void Delete(int id)
        {
            assignmentRepo.Delete(id);
        }

        public Assignment Read(int id)
        {
            return assignmentRepo.Read(id);
        }

        public IEnumerable<Assignment> ReadAll()
        {
            return assignmentRepo.Readall();
        }

        public void Update(Assignment Assignment)
        {
            assignmentRepo.Update(Assignment);
        }
    }
}
