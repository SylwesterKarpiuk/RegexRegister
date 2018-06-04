using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LoginRegister.Models
{

    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Pole adresu email jest wymagane")]
        [DataType(DataType.EmailAddress)]
        [Remote("doesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pole hasła jest wymagane")]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage ="Hasła nie są zgodne")]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public bool MinLength { get; set; }
        [NotMapped]
        public bool MaxLength { get; set; }
        [NotMapped]
        public bool SpecialChar { get; set; }
        [NotMapped]
        public bool Numbers { get; set; }
        [NotMapped]
        public bool Upper { get; set; }

    }
}