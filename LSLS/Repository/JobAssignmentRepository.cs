using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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

        public bool AddJobAssignment(JobAssignment jobAssignment)
        {
            if (jobAssignment == null)
            {
                return false;
            }

            _context.JobAssignments.Add(jobAssignment);
            TransportationInf transportationInf =
                _context.TransportationInfs.Find(jobAssignment.ShippingId);

            if (transportationInf != null)
                transportationInf.JobIsActive = true;

            _context.SaveChanges();

            return true;
        }

        public bool UpdateJobAssignment(JobAssignment jobAssingment)
        {
            if (jobAssingment == null)
            {
                return false;
            }

            _context.Entry(jobAssingment).State = EntityState.Modified;

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

        public FormJobAssignmentViewModel FromJobAssignment(int? jobAssignmentId)
        {
            JobAssignment findJobAssignment = _context.JobAssignments.Find(jobAssignmentId);
            if (findJobAssignment == null)
            {
                return null;
            }

            FormJobAssignmentViewModel formEditJobAssignment = new FormJobAssignmentViewModel
            {                
                TruckDrivers = _context.TruckDrivers.ToList(),
                JobAssignment = findJobAssignment,
            };

            return formEditJobAssignment;
        }


    }
}