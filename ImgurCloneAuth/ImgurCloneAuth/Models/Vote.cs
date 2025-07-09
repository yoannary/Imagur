using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImgurCloneAuth.Models
{
    public class Vote
    {
        [Key, Column("PhotoId", Order = 0)]
        /*column name PhotoId*/
        public int PhotoId { get; set; }

        [Key, Column("UserId",Order = 1)]
        public string UserId { get; set; }
        public VoteType VoteType { get; set; }
        public virtual Photo Photo { get; set; }
        /*public virtual ApplicationUser User { get; set; }*/

        internal static Vote create(int PhotoId, string UserId, VoteType upvote)
        {
            Vote vote = new Vote();
            vote.PhotoId = PhotoId;
            vote.UserId = UserId;
            vote.VoteType = upvote;
            return vote;
        }
    }

    public class VoteDBContext : DbContext
    {
        public VoteDBContext() : base("DefaultConnection")
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<Vote> Votes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vote>()
                .HasKey(v => new { v.PhotoId, v.UserId });


            modelBuilder.Entity<Vote>()
                .HasRequired(v => v.Photo)
                .WithMany()
                .HasForeignKey(v => v.PhotoId)
                .WillCascadeOnDelete(false);
        }

        public Vote FindByPhotoAndUser(int PhotoId, string UserId)
        {
            return this.Votes.Where(v => v.PhotoId == PhotoId && v.UserId == UserId).FirstOrDefault();
        }
    }

    // vote enum for upvote and downvote
    public enum VoteType
    {
        Upvote = 1,
        Downvote = -1
    }
}