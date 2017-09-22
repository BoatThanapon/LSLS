using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;
using LSLS.ViewModels;

namespace LSLS.Repository.Abstract
{
    public interface IJobAssignmentRepository
    {
        IEnumerable<JobAssignment> GetAllJobAssignments();
        JobAssignment GetJobAssignmentById(int? jobAssignmentId);
        bool AddJobAssignment(JobAssignment jobAssignment);
        bool UpdateJobAssignment(JobAssignment jobAssignment);
        bool DeleteJobAssignment(int? jobAssignmentId);

        FormJobAssignmentViewModel FromJobAssignment(int? jobAssignmentId);

    }
}
