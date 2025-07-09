using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace ImgurCloneAuth.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }

        // one photo can have many votes
        public virtual ISet<Vote> Votes { get; } = new HashSet<Vote>();

        public static Photo create(string title, string imagePath)
        {
            Photo photo = new Photo();
            photo.Title = title;
            photo.ImagePath = imagePath;

            return photo;
        }
    }


    public class PhotoDBContext : DbContext
    {

        public PhotoDBContext() : base("DefaultConnection")
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure the relationships between Photo and Vote entities, with Vote only having PhotoId and UserId as the primary key, and does not have Photo and User as navigation properties
            modelBuilder.Entity<Photo>()
                .HasMany(p => p.Votes)
                .WithRequired(v => v.Photo)
                .HasForeignKey(v => v.PhotoId);

            modelBuilder.Entity<Vote>()
                .HasKey(v => new { v.PhotoId, v.UserId });
               
        }
    }

}