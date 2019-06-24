namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'777855fc-d05c-4248-af15-77ffb0d18565', N'guest@vidly.com', 0, N'AGHTedN2Ip7kUava38tKK+l8ObAhiwHmDRlYL7YynaPrmh1hVtwKkgYFUN3mDzWh6w==', N'd5e6ed44-4dca-4f2e-8edc-b9567afce76a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9bf58477-ecb0-4ab4-9c28-b1c8e169edc4', N'admin@vidly.com', 0, N'AHtwYqA8AjXnfwNgZhG/aRLMQs7O+2VIQIYOHSchfScg6w72GKyjgL6Tq6uN2HkrRw==', N'414ab9c2-d541-4546-b1fd-4b2f72c2d470', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f603a7a9-dff3-4bda-acec-4fc3241c6f46', N'CanManageMovies')
            
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9bf58477-ecb0-4ab4-9c28-b1c8e169edc4', N'f603a7a9-dff3-4bda-acec-4fc3241c6f46')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
