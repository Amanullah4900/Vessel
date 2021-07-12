using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vessel.Models.DomainModels
{
    public class Friends
    {
        [Key]
        public int Id { get; set; }
        public int FriendId{ get; set; }
        public int UserId{ get; set; }
        public int Status{ get; set; }

    }
}