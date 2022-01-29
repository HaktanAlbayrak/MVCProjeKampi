namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_aboutdetailsChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abouts", "AboutDetails2", c => c.String(maxLength: 1000));
            DropColumn("dbo.Abouts", "AboutDetais2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Abouts", "AboutDetais2", c => c.String(maxLength: 1000));
            DropColumn("dbo.Abouts", "AboutDetails2");
        }
    }
}
