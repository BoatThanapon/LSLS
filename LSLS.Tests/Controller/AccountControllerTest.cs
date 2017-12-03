using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LSLS.Controllers;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LSLS.Tests.Controller
{
    [TestClass]
    public class AccountControllerTest
    {

        //ViewLoginStaff-----------------------------

        [TestMethod]
        public void Test_ViewLoginStaff_Return_ViewLoginStaff_When_Staff_IsNot_Authenticated()
        {
            // Arrange
            var authProvider = new Mock<IAuthProvider>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockHttpRequest = new Mock<HttpRequestBase>();
            var mockControllerContext = new Mock<ControllerContext>();

            mockHttpRequest.Setup(request => request.IsAuthenticated)
                .Returns(false);
            mockHttpContext.Setup(x => x.Request)
                .Returns(mockHttpRequest.Object);
            mockControllerContext.Setup(context => context.HttpContext)
                .Returns(mockHttpContext.Object);

            var controller =
                new AccountController(authProvider.Object) {ControllerContext = mockControllerContext.Object};

            // Act
            var result = controller.ViewLoginStaff() as ViewResult;
            var viewName = result.ViewName;
            // Assert
            Assert.AreEqual(viewName, "ViewLoginStaff");
        }


        [TestMethod]
        public void Test_ViewLoginStaff_Return_MainView_When_Staff_Is_Authenticated()
        {
            // Arrange
            var authProvider = new Mock<IAuthProvider>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockHttpRequest = new Mock<HttpRequestBase>();
            var mockControllerContext = new Mock<ControllerContext>();

            mockHttpRequest.Setup(request => request.IsAuthenticated)
                .Returns(true);
            mockHttpContext.Setup(x => x.Request)
                .Returns(mockHttpRequest.Object);
            mockControllerContext.Setup(context => context.HttpContext)
                .Returns(mockHttpContext.Object);

            var controller =
                new AccountController(authProvider.Object) { ControllerContext = mockControllerContext.Object };

            // Act
            var result = controller.ViewLoginStaff() as ViewResult;
            var viewName = result.ViewName;
            // Assert
            Assert.AreEqual(viewName, "Main");
        }


        //CheckLoginStaff------------------------------------------------------------
        [TestMethod]
        public void Test_CheckLoginStaff_Return_Main_When_Login_Success()
        {
            //Arrage
            var loginStaff = new LoginStaffViewModel()
            {
                StaffEmployeeId = "E0001",
                StaffUsername = "Admin",
                StaffPassword = "Admin"
            };

            var staff = new Staff
            {
                StaffId = 1,
                StaffEmployeeId = "E0001",
                StaffUsername = "AdminA",
                StaffPassword = "1234",
                StaffConfirmPassword = "1234",
                StaffFullname = "Thanapon",
                StaffCitizenId = "15799900617731",
                StaffGender = "Male",
                StaffAddress = "264 Chiang Saen Chiang Rai",
                StaffBirthdate = new DateTime(2010, 3, 11).ToString(CultureInfo.InvariantCulture),
                StaffEmail = "AdminA@gmail.com",
                StaffTelephoneNo = "0970747125"
            };

            var authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(a => a.AuthenticateStaff(loginStaff)).Returns(staff);

            var controller = new AccountController(authProvider.Object);

            // Act
            var result = controller.CheckLoginStaff(loginStaff) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "Main");
        }

        [TestMethod]
        public void Test_CheckLoginStaff_Return_ViewLoginStaff_When_Login_False()
        {
            //Arrage
            var loginStaff = new LoginStaffViewModel()
            {
                StaffEmployeeId = "E1234",
                StaffUsername = "Admin",
                StaffPassword = "Admin"
            };

            var staff = new Staff
            {
                StaffId = 1,
                StaffEmployeeId = "E0001",
                StaffUsername = "AdminA",
                StaffPassword = "1234",
                StaffConfirmPassword = "1234",
                StaffFullname = "Thanapon",
                StaffCitizenId = "15799900617731",
                StaffGender = "Male",
                StaffAddress = "264 Chiang Saen Chiang Rai",
                StaffBirthdate = new DateTime(2010, 3, 11).ToString(CultureInfo.InvariantCulture),
                StaffEmail = "AdminA@gmail.com",
                StaffTelephoneNo = "0970747125"
            };

            var authProvider = new Mock<IAuthProvider>();
            authProvider.Setup(a => a.AuthenticateStaff(loginStaff)).Returns(staff);

            var controller = new AccountController(authProvider.Object);
            controller.ModelState.AddModelError("", "");

            // Act
            var result = controller.CheckLoginStaff(null) as ViewResult;
            var redirectViewName = result.ViewName;          

            // Assert
            Assert.AreEqual(redirectViewName, "ViewLoginStaff");
        }

        //Main--------------------------------------------------------------
        [TestMethod]
        public void Test_Main_Return_MainView()
        {
            // Arrange
            var authProvider = new Mock<IAuthProvider>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockHttpRequest = new Mock<HttpRequestBase>();
            var mockControllerContext = new Mock<ControllerContext>();

            mockHttpRequest.Setup(request => request.IsAuthenticated)
                .Returns(true);
            mockHttpContext.Setup(x => x.Request)
                .Returns(mockHttpRequest.Object);
            mockControllerContext.Setup(context => context.HttpContext)
                .Returns(mockHttpContext.Object);

            var controller =
                new AccountController(authProvider.Object) { ControllerContext = mockControllerContext.Object };

            // Act
            var result = controller.Main() as ViewResult;
            var viewName = result.ViewName;
            // Assert
            Assert.AreEqual(viewName, "Main");
        }

        /*
        //Logout------------------------------------
        [TestMethod]
        public void Test_Logout_Return_MainView()
        {
            // Arrange
            var authProvider = new Mock<IAuthProvider>();

            var controller =
                new AccountController(authProvider.Object);

            var result = controller.Logout() as RedirectToRouteResult;
            var viewName = result.RouteValues["Action"];

            Assert.AreEqual("ViewLoginStaff", viewName);
        }

    */

    }
}
