using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SocialMedia.Models.Data.DataEntities;

namespace SocialMedia.Models.Data.DataContext
{
    public class SocialMediaDbContext :DbContext    
    {
        public DbSet<UserEntity> Users { get; set; }
    }
}