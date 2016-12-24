using System.Data.Entity.Migrations;

namespace Key.Int.Migrations
{
	public partial class InitialCreate : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Blogs",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Name = c.String(),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.AspNetRoleClaims",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					RoleId = c.Int(nullable: false),
					ClaimType = c.String(),
					ClaimValue = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
				.Index(t => t.RoleId);

			CreateTable(
				"dbo.AspNetRoles",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Name = c.String(nullable: false, maxLength: 256),
					NormalizedName = c.String(),
					ConcurrencyStamp = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Name, unique: true, name: "RoleNameIndex");

			CreateTable(
				"dbo.AspNetUserRoles",
				c => new
				{
					UserId = c.Int(nullable: false),
					RoleId = c.Int(nullable: false),
				})
				.PrimaryKey(t => new { t.UserId, t.RoleId })
				.ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId)
				.Index(t => t.RoleId);

			CreateTable(
				"dbo.AspNetUserClaims",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					UserId = c.Int(nullable: false),
					ClaimType = c.String(),
					ClaimValue = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.AspNetUserLogins",
				c => new
				{
					LoginProvider = c.String(nullable: false, maxLength: 128),
					ProviderKey = c.String(nullable: false, maxLength: 128),
					UserId = c.Int(nullable: false),
					ProviderDisplayName = c.String(),
				})
				.PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.AspNetUsers",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Name = c.String(),
					UserName = c.String(nullable: false, maxLength: 256),
					NormalizedUserName = c.String(),
					Email = c.String(maxLength: 256),
					NormalizedEmail = c.String(),
					EmailConfirmed = c.Boolean(nullable: false),
					PasswordHash = c.String(),
					SecurityStamp = c.String(),
					ConcurrencyStamp = c.String(),
					PhoneNumber = c.String(),
					PhoneNumberConfirmed = c.Boolean(nullable: false),
					TwoFactorEnabled = c.Boolean(nullable: false),
					LockoutEnd = c.DateTimeOffset(precision: 7),
					LockoutEnabled = c.Boolean(nullable: false),
					AccessFailedCount = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.Id)
				.Index(t => t.UserName, unique: true, name: "UserNameIndex")
				.Index(t => t.Email, unique: true, name: "UserEmailIndex");
		}

		public override void Down()
		{
			DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
			DropForeignKey("dbo.AspNetRoleClaims", "RoleId", "dbo.AspNetRoles");
			DropIndex("dbo.AspNetUsers", "UserEmailIndex");
			DropIndex("dbo.AspNetUsers", "UserNameIndex");
			DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
			DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
			DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
			DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
			DropIndex("dbo.AspNetRoles", "RoleNameIndex");
			DropIndex("dbo.AspNetRoleClaims", new[] { "RoleId" });
			DropTable("dbo.AspNetUsers");
			DropTable("dbo.AspNetUserLogins");
			DropTable("dbo.AspNetUserClaims");
			DropTable("dbo.AspNetUserRoles");
			DropTable("dbo.AspNetRoles");
			DropTable("dbo.AspNetRoleClaims");
			DropTable("dbo.Blogs");
		}
	}
}
