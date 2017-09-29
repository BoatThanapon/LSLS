using System.Collections.Generic;
using LSLS.Models;
using LSLS.ViewModels;

namespace LSLS.Repository.Abstract
{
    public interface IJobAssignmentRepository
    {
        IEnumerable<JobAssignment> GetAllJobAssignments();
        JobAssignment GetJobAssignmentById(int? jobAssignmentId);
        bool AddJobAssignment(FormJobAssignmentViewModel jobAssignmentViewModel);
        bool UpdateJobAssignment(FormJobAssignmentViewModel jobAssignmentViewModel);
        bool DeleteJobAssignment(int? jobAssignmentId);

        List<JobAssignment> GetListJobByTruckDriverId(int truckDriverId);
    }
}
