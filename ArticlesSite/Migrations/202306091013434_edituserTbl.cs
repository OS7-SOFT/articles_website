namespace ArticlesSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edituserTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Type", c => c.String());
            AddColumn("dbo.AspNetUsers", "AboutYou", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AboutYou");
            DropColumn("dbo.AspNetUsers", "Type");
            DropColumn("dbo.AspNetUsers", "Image");
        }
    }
}
