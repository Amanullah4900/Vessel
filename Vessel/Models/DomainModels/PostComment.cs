using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vessel.Models.DomainModels
{
    public class PostComment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
        public string TimeStamp { get; set; }
        public string FileUrl { get; set; }
    }
}