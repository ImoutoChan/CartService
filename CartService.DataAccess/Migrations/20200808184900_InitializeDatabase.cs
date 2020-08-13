using FluentMigrator;

namespace CartService.DataAccess.Migrations
{
    [Migration(20200808184900)]
    public class InitializeDatabase : Migration
    {
        public override void Up()
        {
            Create.Table("Cart")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Created").AsDateTimeOffset().NotNullable()
                .WithColumn("Updated").AsDateTimeOffset().NotNullable();

            Create.Table("CartItem")
                .WithColumn("CartId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("ProductId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Quantity").AsInt32().NotNullable();

            Create.Table("WebHook")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CartId").AsInt32().NotNullable()
                .WithColumn("Uri").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Cart");
            Delete.Table("CartItem");
            Delete.Table("WebHook");
        }
    }
}
