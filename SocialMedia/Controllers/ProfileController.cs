using SocialMedia.Models.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {
        //Init database
        private SocialMediaDbContext _db;

        //Inject database instance as dependency
        public ProfileController(SocialMediaDbContext db)
        {
            _db = db;
        }

        
    }
}