using SPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPlanner.Interfaces
{
    interface IUserController
    {
        ActionResult Index();
        ActionResult Details();
        ActionResult Register();
        ActionResult Register([Bind(Include = "UserID,FirstName,LastName,EmailAddress,Password,Remember,College")] User user);
        ActionResult Login();
        ActionResult Login(User u);
        ActionResult UserDashBoard();
        ActionResult Logout();
        ActionResult Edit();
        ActionResult Edit([Bind(Include = "FirstName,LastName,EmailAddress,Password,College")] User user);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);

    }
}