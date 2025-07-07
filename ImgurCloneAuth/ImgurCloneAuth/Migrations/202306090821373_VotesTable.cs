namespace ImgurCloneAuth.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class VotesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                {
                    PhotoId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                    // enum votetype 
                    VoteType = c.Int(nullable: false),
                })
                // primary key is composite key of photoId and userId
                .PrimaryKey(t => new { t.PhotoId, t.UserId })
/*                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Photos", t => t.PhotoId)*/
                .Index(t => t.PhotoId)
                .Index(t => t.UserId);

            AddForeignKey("dbo.Votes", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Votes", "PhotoId", "dbo.Photos", "Id");

        }

        public override void Down()
        {
            DropTable("dbo.Photos");
        }
    }
}
