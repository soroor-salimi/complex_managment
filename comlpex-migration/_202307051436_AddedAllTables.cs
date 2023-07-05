using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comlpex_migration
{
    [Migration(202307051436)]
    public class _202307051436_AddedAllTables : Migration
    {
        public override void Up()
        {
            Create.Table("Complexes")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("UnitCount").AsInt32().NotNullable();

            Create.Table("Blocks")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ComplexId").AsInt32().NotNullable()
                .ForeignKey("FK-Complexes-Blocks", "Complexes", "Id");

            Create.Table("Units")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("BlockId").AsInt32().NotNullable()
                .WithColumn("ResidentOfType").AsInt32().NotNullable();

                
        }

        public override void Down()
        {
            Delete.Table("Units");
            Delete.Table("Blocks");
            Delete.Table("Camplexes");
        }

       


    }
}
