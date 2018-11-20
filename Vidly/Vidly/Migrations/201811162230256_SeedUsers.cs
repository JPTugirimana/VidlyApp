namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fa7ece82-ed74-46a6-9f85-8a583e054e76', N'admin@domain.com', 0, N'AMm4Epo9i4PXEg8KMNO2iuQP+CsoFKWMQAJXuolDW8XXtD8qkodbubEpp1EVoG2XZg==', N'1f1fec1a-8f41-41c1-a91a-0395b46c643e', NULL, 0, 0, NULL, 1, 0, N'admin@domain.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fc79ce4e-51a2-42f2-9aab-fa3f18dd6e39', N'staff1@domain.com', 0, N'AAHdpWrbzqbkwg8BaOJNhENir1abCUBTTPnV3ZWbVxjeOsAbwrc7oNXD4QDPa84vBg==', N'e50549bd-8983-42cd-ba3c-ccf0c791e732', NULL, 0, 0, NULL, 1, 0, N'staff1@domain.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b4ae99a6-a024-4806-8d21-6f9bfb538989', N'canManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fa7ece82-ed74-46a6-9f85-8a583e054e76', N'b4ae99a6-a024-4806-8d21-6f9bfb538989')

");
        }
        
        public override void Down()
        {
        }
    }
}
