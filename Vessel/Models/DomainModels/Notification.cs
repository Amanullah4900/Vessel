using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vessel.Models.DomainModels
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Title { get; set; }
        public string body { get; set; }
        public string TimeStamp { get; set; }
    }
}