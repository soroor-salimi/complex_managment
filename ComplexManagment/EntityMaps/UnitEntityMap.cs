using ComplexManagment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComplexManagment.EntityMaps
{
    public class UnitEntityMap : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();

            builder.Property(_ => _.Name).IsRequired();
            builder.Property(_=>_.BlockId).IsRequired();
            builder.Property(_ => _.Count).IsRequired();
            builder.Property(_=>_.ResidentOfType).IsRequired();
        }
    }
}
