namespace ImgurCloneAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(unicode: false),
                    ImagePath = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Photos");
        }
    }
}
