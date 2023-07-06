using ComplexManagment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComplexManagment.EntityMaps
{
    public class BlockEntityMap : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.ToTable("Complexes");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();

            builder.Property(_=>_.Name).IsRequired();
            builder.Property(_=>_.ComplexId).IsRequired();
        }
    }
}
