using System.Data;
using FluentMigrator;

namespace CartService.DataAccess.Migrations
{
    [Migration(20200814020800)]
    public class AddForeignKeysDatabase : Migration
    {
        public override void Up()
        {
            Create.ForeignKey("FK_CartItem_Cart")
                .FromTable("CartItem").ForeignColumn("CartId")
                .ToTable("Cart").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            Create.ForeignKey("FK_WebHook_Cart")
                .FromTable("WebHook").ForeignColumn("CartId")
                .ToTable("Cart").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_CartItem_Cart")
                .OnTable("CartItem");
            Delete.ForeignKey("FK_WebHook_Cart")
                .OnTable("WebHook");
        }
    }
}