using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SocialMedia.Models.Data.DataEntities;

namespace SocialMedia.Models.ViewModels.Account
{
    public class UserViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public UserViewModel(){}

        public UserViewModel(UserEntity userEntity)
        {
            Id = userEntity.Id;
            FirstName = userEntity.FirstName;
            LastName = userEntity.LastName;
            EmailAddress = userEntity.EmailAddress;
            UserName = userEntity.UserName;
            Password = userEntity.Password;
        }
    }
}