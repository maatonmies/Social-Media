using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SocialMedia.Models.Data.DataContext;
using SocialMedia.Models.Data.DataEntities;
using SocialMedia.Models.ViewModels.Account;

namespace SocialMedia.Controllers
{
    public class AccountController : Controller
    {
        //Init database
        private SocialMediaDbContext _db;
        
        //Inject database instance as dependency
        public AccountController(SocialMediaDbContext db)
        {
            _db = db;
        }

        //INDEX
        // GET: /
        public ActionResult Index()
        {
            //Check if user is already logged in
            var userName = User.Identity.Name;
            //If yes take the user to the dashboard
            if (!string.IsNullOrEmpty(userName)) return Redirect("~/" + userName);
            //If not take them to the login page
            //if there is a validation message put it in the viewbag
            if (TempData["validationMessage"] != null) ViewBag.Message = TempData["validationMessage"].ToString();

            return View();
        }

        //LOGIN
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //check if user extists, if yes take them to the dashboard
            if (_db.Users.Any(x => x.UserName.Equals(username) && x.Password.Equals(password))) {

                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect("~/" + username);
            }
            //if not redirect to index with validation message
            TempData["validationMessage"] = "Login failed. Invalid username or password.";

            return Redirect("~/");
        }

        //CREATE ACCOUNT
        // POST: /Account/CreateAccount
        [HttpPost]
        public ActionResult CreateAccount(UserViewModel model, HttpPostedFileBase imgUpload)
        {
            //Check model state
            if (!ModelState.IsValid) return View("Index", model);
            //Make sure username is unique
            if (_db.Users.Any(x => x.UserName.Equals(model.UserName)))
            {
                ModelState.AddModelError("UserNameAlreadyTaken", $"Sorry, the username: {model.UserName} is already taken.");
                model.UserName = "";
                return View("Index", model);
            }
            //Create user
            var user = new UserEntity
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                EmailAddress = model.EmailAddress,
                Password = model.Password
            };
            //Add user to database
            _db.Users.Add(user);
            //Save database
            _db.SaveChanges();
            //Get inserted id
            var userId = user.Id;
            //Index user and set cookie for username
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            //Make uploads directory
            var uploadsDir = $"{Server.MapPath(@"\")}Uploads";
            //Check if profile picture was uploaded, if not redirect to dashboard
            if (imgUpload == null || imgUpload.ContentLength <= 0) return Redirect("~/" + model.UserName); ;
            //If yes get the file extension
            var type = imgUpload.ContentType.ToLower();
            //Verify the file extension
            if (type != "image/jpg" &&
                type != "image/jpeg" &&
                type != "image/pjpeg" &&
                type != "image/x-png" &&
                type != "image/png" &&
                type != "image/gif")
            {
                ModelState.AddModelError("InvalidProfilePicture", "Could not upload profile picture. Please make sure the file is an image!");
                return View("Index", model);
            }
            //Set name for the profile picture
            var imageName = userId + ".jpg";
            //Set path for the image
            var path = $"{uploadsDir}\\{imageName}";
            //Save image
            imgUpload.SaveAs(path);
            //then redirect to dashboard
            return Redirect("~/" + model.UserName);
        }

        //DASHBOARD
        // GET: /{username}
        [Authorize]
        public ActionResult Dashboard(string username = "")
        {
            //Check if user extists and has logged in
            if (_db.Users.Any(x => x.UserName.Equals(username)))
            {
                ViewBag.Username = username;
                return View();
            }
                return Redirect("~/");            
        }

        //LOGOUT
        // GET: /Account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            //Sign Out
            FormsAuthentication.SignOut();

            //Redirect
            return RedirectToAction("Index");
        }

        //SEARCH USERS
        // GET: /Account/SearchUsers
        public JsonResult SearchUsers(string term)
        {
            var searchResults =
                _db.Users
                    .Where(u => u.UserName != User.Identity.Name && u.FirstName.StartsWith(term) || u.LastName.StartsWith(term))
                    .Take(10)
                    .Select(u => new {
                        label = u.FirstName + " " + u.LastName,
                        username = u.UserName,
                        profilePicUrl = "uploads/" + u.Id + ".jpg"
                    });

            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }
    }

}