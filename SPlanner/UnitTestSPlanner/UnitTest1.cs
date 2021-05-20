using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SPlanner;
using SPlanner.Security;
using SPlanner.Controllers;
using System.Web.Mvc;
using SPlanner.Models;
using System.Web;

namespace UnitTestSPlanner
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Hash_Password()
        {
            //Arrange
            string password = "WDsd24#a";
            //Act
            string actual = PasswordSecurity.HashPassword(password);
            //Assert
            string expected = "942a6f8bcdac8489cdb5c5fdb3c0abf55b402401b2ffafc1e7ae70657a76d19a";
            Assert.AreEqual(expected, actual, "Password hash is not equal");
        }
        [TestMethod]
        public void Check_Leanght_Password()
        {
            //Arrange
            string password = "WDsd2";
            //Act
            bool actual = PasswordSecurity.CheckPassword(password);
            //Assert
            bool expected = false;
            Assert.AreEqual(expected, actual, "Password dont have 7 chars");
        }
        [TestMethod]
        public void Special_Char_Is_Miss_Password()
        {
            //Arrange
            string password = "WDsd2s1";
            //Act
            bool actual = PasswordSecurity.CheckPassword(password);
            //Assert
            bool expected = false;
            Assert.AreEqual(expected, actual, "Password dont have Special char");
        }
        [TestMethod]
        public void Check_Upper_Case_Letter_Password()
        {
            //Arrange
            string password = "gggsd2s1";
            //Act
            bool actual = PasswordSecurity.CheckPassword(password);
            //Assert
            bool expected = false;
            Assert.AreEqual(expected, actual, "Password dont have Upper Case Letter");
        }
        [TestMethod]
        public void Check_Numeric_Value_Password()
        {
            //Arrange
            string password = "gggsWdhh";
            //Act
            bool actual = PasswordSecurity.CheckPassword(password);
            //Assert
            bool expected = false;
            Assert.AreEqual(expected, actual, "Password dont have numeric value");
        }
        [TestMethod]
        public void Check_Password()
        {
            //Arrange
            string password = "ddRT23!as";
            //Act
            bool actual = PasswordSecurity.CheckPassword(password);
            //Assert
            bool expected = true;
            Assert.AreEqual(expected, actual, "Password dont have lower/upper case letter, or numeric value, or special case characters");
        }
       
        //Budget Controller
        [TestMethod]
        public void Check_Budgets_Controller_Create()
        {
            //Arrange
            var controller = new BudgetsController();
            //Act
            var result = controller.Create() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Check_Budgets_Controller_Details()
        {
            /*
            //Arrange
            var controller = new BudgetsController();
            //Act
            var result = controller.Details(1) as ViewResult; //controller.Logout() as ViewResult;
            var budget = (Budget) result.ViewData.Model;
            //Assert
            Assert.AreEqual(1, budget.UserID);*/
        }

        //Grade Controller
        [TestMethod]
        public void Check_Grades_Controller_Create()
        {
            //Arrange
            var controller = new GradesController();
            //Act
            var result = controller.Create() as ViewResult;
            //Assert
            Assert.IsNotNull(result);

        }
        //Home Controller
        [TestMethod]
        public void Check_Home_Controller_Index()
        {
            //Arrange
            var controller = new HomeController();
            //Act
            var result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Check_Home_Controller_About()
        {
            //Arrange
            var controller = new HomeController();
            //Act
            var result = controller.About() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Check_Home_Controller_Contact()
        {
            //Arrange
            var controller = new HomeController();
            //Act
            var result = controller.Contact() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        //User Controller
        [TestMethod]
        public void Check_User_Controller_Index()
        {
            //Arrange
            var controller = new UsersController();
            //Act
            var result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_User_Controller_Register()
        {
            //Arrange
            var controller = new UsersController();
            //Act
            var result = controller.Register() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Check_User_Controller_Login()
        {
            //Arrange
            var controller = new UsersController();
            //Act
            var result = controller.Login() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Check_User_Controller_UserDashBoard()
        {
            //Arrange
            var controller = new UsersController();
            //Act
            var result = controller.UserDashBoard() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Check_User_Controller_AdminDashBoard()
        {
            //Arrange
            var controller = new UsersController();
            //Act
            var result = controller.AdminDashBoard() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
