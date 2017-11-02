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

        public IEnumerable<JobAssignment> GetAllJobAssignments()
        {
            IEnumerable<JobAssignment> jobAssingments = _context.JobAssignments.Include(j => j.TruckDriver).Include(t => t.TransportationInf).ToList();

            return jobAssingments;
        }

        public JobAssignment GetJobAssignmentById(int? jobAssignmentId)
        {
            JobAssignment jobAssignmentInDb = _context.JobAssignments.Include(j => j.TruckDriver).Include(t => t.TransportationInf).SingleOrDefault(j => j.JobAssignmentId == jobAssignmentId);

            return jobAssignmentInDb;
        }

        public bool AddJobAssignment(FormJobAssignmentViewModel jobAssignmentViewModel)
        {
            if (jobAssignmentViewModel == null)
            {
                return false;
            }

            JobAssignment jobAssignment = new JobAssignment
            {
                ShippingId = jobAssignmentViewModel.JobAssignment.ShippingId,
                TruckDriverId = jobAssignmentViewModel.JobAssignment.TruckDriverId,
                JobAssignmentDate = jobAssignmentViewModel.JobAssignment.JobAssignmentDate,
                StartingPointJob = jobAssignmentViewModel.JobAssignment.StartingPointJob,
                LatitudeStartJob = jobAssignmentViewModel.JobAssignment.LatitudeStartJob,
                LongitudeStartJob = jobAssignmentViewModel.JobAssignment.LongitudeStartJob,
                DestinationJob = jobAssignmentViewModel.JobAssignment.DestinationJob,
                LatitudeDesJob = jobAssignmentViewModel.JobAssignment.LatitudeDesJob,
                LongitudeDesJob = jobAssignmentViewModel.JobAssignment.LongitudeDesJob,
            };

            _context.JobAssignments.Add(jobAssignment);
            TransportationInf transportationInf =
                _context.TransportationInfs.Find(jobAssignment.ShippingId);

            if (transportationInf != null)
            {
                transportationInf.JobIsActive = true;
                transportationInf.DateOfTransportation = jobAssignment.JobAssignmentDate;

                _context.Entry(transportationInf).State = EntityState.Modified;

            }
            _context.SaveChanges();

            return true;
        }

        public bool UpdateJobAssignment(FormJobAssignmentViewModel jobAssignmentViewModel)
        {
            if (jobAssignmentViewModel == null)
            {
                return false;
            }

            JobAssignment jobAssignment = new JobAssignment
            {
                JobAssignmentId = jobAssignmentViewModel.JobAssignment.JobAssignmentId,
                ShippingId = jobAssignmentViewModel.JobAssignment.ShippingId,
                TruckDriverId = jobAssignmentViewModel.JobAssignment.TruckDriverId,
                JobAssignmentDate = jobAssignmentViewModel.JobAssignment.JobAssignmentDate,
                StartingPointJob = jobAssignmentViewModel.JobAssignment.StartingPointJob,
                LatitudeStartJob = jobAssignmentViewModel.JobAssignment.LatitudeStartJob,
                LongitudeStartJob = jobAssignmentViewModel.JobAssignment.LongitudeStartJob,
                DestinationJob = jobAssignmentViewModel.JobAssignment.DestinationJob,
                LatitudeDesJob = jobAssignmentViewModel.JobAssignment.LatitudeDesJob,
                LongitudeDesJob = jobAssignmentViewModel.JobAssignment.LongitudeDesJob,
            };

            _context.Entry(jobAssignment).State = EntityState.Modified;

            var findTransportationInf = _context.TransportationInfs.Find(jobAssignment.ShippingId);
            if (findTransportationInf == null)
                return false;
          
            findTransportationInf.DateOfTransportation = jobAssignment.JobAssignmentDate;
            findTransportationInf.StartingPoint = jobAssignment.StartingPointJob;
            findTransportationInf.Destination = jobAssignment.DestinationJob;

            _context.Entry(findTransportationInf).State = EntityState.Modified;

            _context.SaveChanges();

            return true;
        }

        public bool DeleteJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return false;
           
            JobAssignment jobAssignmentIndb = _context.JobAssignments.Find(jobAssignmentId);
            if (jobAssignmentIndb == null)
                return false;

            _context.JobAssignments.Remove(jobAssignmentIndb);

            TransportationInf transportationInf =
                _context.TransportationInfs.Find(jobAssignmentIndb.ShippingId);

            if (transportationInf != null)
                transportationInf.JobIsActive = false;

            _context.SaveChanges();

            return true;
        }

        public List<JobAssignment> GetListJobByTruckDriverId(int? truckDriverId)
        {
            List<JobAssignment> listJobAssignmentsByTruckDriverId = _context.JobAssignments.Where(j => j.TruckDriverId == truckDriverId).ToList();
            return listJobAssignmentsByTruckDriverId;
        }

        /*
        public JobAssignment GetJobAssingmentInfoByJobAssignmentId(int? jobAssignmentId)
        {
            
        }
        */
    }
}