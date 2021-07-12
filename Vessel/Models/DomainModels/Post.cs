using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vessel.Models.DomainModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int PostedById { get; set; }
        public int ShareById { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public string TimeStamp { get; set; }
        public string Tags { get; set; }
        public int Category { get; set; }
        public string Type { get; set; }
    }
}