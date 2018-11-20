namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreAgain : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT dbo.Genres ON");
            Sql("INSERT INTO Genres (Id, Name) VALUES (1,'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2,'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3,'Drama')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4,'Western')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5,'Family')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6,'Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
