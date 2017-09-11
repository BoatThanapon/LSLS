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

        public IEnumerable<JobAssingment> GetAllJobAssingments()
        {
            IEnumerable<JobAssingment> jobAssingments = _context.JobAssingments.Include(j => j.TruckDriver).Include(t => t.TransportationInf).ToList();

            return jobAssingments;
        }

        public JobAssingment GetJobAssingmentById(int? jobAssignmentId)
        {
            JobAssingment jobAssingmentInDb = _context.JobAssingments.Include(j => j.TruckDriver).Include(t => t.TransportationInf).SingleOrDefault(j => j.JobAssignmentId == jobAssignmentId);

            return jobAssingmentInDb;
        }

        public bool AddJobAssignment(JobAssingment jobAssingment)
        {
            if (jobAssingment == null)
            {
                return false;
            }

            _context.JobAssingments.Add(jobAssingment);
            TransportationInf transportationInf =
                _context.TransportationInfs.Find(jobAssingment.ShippingId);

            if (transportationInf != null)
                transportationInf.JobIsActive = true;

            _context.SaveChanges();

            return true;
        }

        public bool UpdateJobAssignment(JobAssingment jobAssingment)
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
           
            JobAssingment jobAssingment = _context.JobAssingments.Find(jobAssignmentId);
            if (jobAssingment == null)
                return false;

            _context.JobAssingments.Remove(jobAssingment);

            TransportationInf transportationInf =
                _context.TransportationInfs.Find(jobAssingment.ShippingId);

            if (transportationInf != null)
                transportationInf.JobIsActive = false;

            _context.SaveChanges();

            return true;
        }

        public FormJobAssignmentViewModel FromJobAssingment(int? jobAssignmentId)
        {
            JobAssingment findJobAssingment = _context.JobAssingments.Find(jobAssignmentId);
            if (findJobAssingment == null)
            {
                return null;
            }

            FormJobAssignmentViewModel formEditJobAssignment = new FormJobAssignmentViewModel
            {                
                TruckDrivers = _context.TruckDrivers.ToList(),
                JobAssingment = findJobAssingment,
            };

            return formEditJobAssignment;
        }


    }
}