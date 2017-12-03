using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Controllers
{
    [Authorize]
    public class ReportTransportationController : Controller
    {
        private readonly IReportTransportationRepository _reportTransportationRepository;

        public ReportTransportationController(IReportTransportationRepository reportTransportationRepository)
        {
            _reportTransportationRepository = reportTransportationRepository;
        }

        // GET: ReportTransportation
        [HttpGet]
        public ViewResult ReportTransportationMainView()
        {
            ReportTransportationViewModel viewModel = new ReportTransportationViewModel();
            return View("ReportTransportationMainView", viewModel);
        }

        [HttpGet]
        public ActionResult ListTransportationInfIsCompletedInMonth(ReportTransportationViewModel viewModel)
        {
            var listIsCompleteInMonth =
                _reportTransportationRepository.ListTransportationInfIsCompletedInMonth(viewModel.Month, viewModel.Year);

            return View("ListTransportationInfIsCompletedInMonth", listIsCompleteInMonth);
        }

        [HttpGet]
        public ActionResult ListTransportationInfIsNotCompletedInMonth(ReportTransportationViewModel viewModel)
        {
            var listIsNotCompleteInMonth =
                _reportTransportationRepository.ListTransportationInfIsNotCompletedInMonth(viewModel.Month, viewModel.Year);

            return View("ListTransportationInfIsNotCompletedInMonth", listIsNotCompleteInMonth);
        }
    }
}