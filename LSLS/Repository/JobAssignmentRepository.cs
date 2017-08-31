using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Repository
{
    public class JobAssignmentRepository : IJobAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public JobAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<JobAssingment> GetAllJobAssingments()
        {
            IEnumerable<JobAssingment> jobAssingments = _context.JobAssingments.Include(j => j.TruckDriver).ToList();

            return jobAssingments;
        }

        public JobAssingment GetJobAssingmentById(int? jobAssignmentId)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (jobAssignmentId != null)
                return _context.JobAssingments.Find(jobAssignmentId);

            return null;
        }

        public FormJobAssignmentViewModel FromJobAssingment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
            {
                IEnumerable<TruckDriver> truckDrivers = _context.TruckDrivers.ToList();

                FormJobAssignmentViewModel formCreateJobAssignment = new FormJobAssignmentViewModel
                {
                    JobAssingment = new JobAssingment(),
                    TruckDrivers = truckDrivers
                };

                return formCreateJobAssignment;
            }
            else
            {
                JobAssingment findJobAssingment = GetJobAssingmentById(jobAssignmentId);
                if (findJobAssingment == null)
                {
                    return null;
                }
                
                FormJobAssignmentViewModel formEditJobAssignment = new FormJobAssignmentViewModel
                {
                    JobAssingment = findJobAssingment,
                    TruckDrivers = _context.TruckDrivers.ToList()
                };

                return formEditJobAssignment;

            }
            
        }


        public bool AddJobAssignment(JobAssingment jobAssingment)
        {
           if (jobAssingment == null)
                return false;


            _context.JobAssingments.Add(jobAssingment);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateJobAssignment(JobAssingment jobAssingment)
        {
            if (jobAssingment == null)
                return false;

            var jobAssingmentInDb =
                _context.JobAssingments.FirstOrDefault(j => j.JobAssignmentId == jobAssingment.JobAssignmentId);
            if (jobAssingmentInDb != null)
            {
                jobAssingmentInDb.JobAssignmentDateTime = jobAssingment.JobAssignmentDateTime;
                jobAssingment.TruckDriverId = jobAssingment.TruckDriverId;
                jobAssingmentInDb.StartingPointJob = jobAssingment.StartingPointJob;
                jobAssingmentInDb.LatitudeStartJob = jobAssingment.LatitudeStartJob;
                jobAssingmentInDb.LongitudeStartJob = jobAssingment.LongitudeStartJob;
                jobAssingmentInDb.DestinationJob = jobAssingment.DestinationJob;
                jobAssingmentInDb.LatitudeDesJob = jobAssingment.LatitudeDesJob;
                jobAssingmentInDb.LongitudeDesJob = jobAssingment.LongitudeDesJob;
                jobAssingmentInDb.JobAssignmentStatus = jobAssingment.JobAssignmentStatus;

            }
            _context.SaveChanges();

            return true;
        }

        public bool DeleteJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return false;

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            var jobAssingment = _context.JobAssingments.Find(jobAssignmentId);
            if (jobAssingment == null)
                return false;

            _context.JobAssingments.Remove(jobAssingment);
            _context.SaveChanges();

            return true;
        }
    }
}