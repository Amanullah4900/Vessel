using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vessel.Models.DomainModels;

namespace Vessel.Models
{
    public class DataContext: DbContext
    {
        public DataContext()
           : base("Vessel")
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostLike> PostLiskes { get; set; }
    }
}