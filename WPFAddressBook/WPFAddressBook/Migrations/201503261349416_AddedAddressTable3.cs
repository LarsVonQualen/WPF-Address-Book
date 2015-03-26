namespace WPFAddressBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddressTable3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Address", "PersonId");
            AddForeignKey("dbo.Address", "PersonId", "dbo.Person", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "PersonId", "dbo.Person");
            DropIndex("dbo.Address", new[] { "PersonId" });
        }
    }
}
