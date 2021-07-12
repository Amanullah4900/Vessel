using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vessel.Models.DomainModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Email Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string Phone { get; set; }
        public string Profile { get; set; }
        public string BannerImage { get; set; }
        public string Bio { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}