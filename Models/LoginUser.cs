using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BookClub.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "User name is required")]
        public string LUser{get;set;}



        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string LPassword{get;set;}
    }

}