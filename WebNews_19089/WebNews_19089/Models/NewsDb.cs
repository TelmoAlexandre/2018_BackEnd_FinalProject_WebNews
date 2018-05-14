using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebNews_19089.Models {
    public class NewsDb : DbContext {

        public NewsDb() : base("NewsDb") {

        }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<UsersProfile> UsersProfile { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}