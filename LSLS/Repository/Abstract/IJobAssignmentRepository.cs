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
        IEnumerable<JobAssingment> GetAllJobAssingments();
        JobAssingment GetJobAssingmentById(int? jobAssignmentId);
        bool AddJobAssignment(JobAssingment jobAssingment);
        bool UpdateJobAssignment(JobAssingment jobAssingment);
        bool DeleteJobAssignment(int? jobAssignmentId);

        FormJobAssignmentViewModel FromJobAssingment(int? jobAssignmentId);

    }
}
