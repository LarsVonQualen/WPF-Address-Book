namespace WPFAddressBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddressTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressString = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Address");
        }
    }
}
