using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vessel.Models.DomainModels
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
    }

    public class GroupMembers
    {
        [Key]
        public int Id { get; set; }
        public string GroupId { get; set; }
        public int UserId { get; set; }
    }
}