using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Mapping
{
    public class IndexPointMapping : IEntityTypeConfiguration<IndexPoint>
    {
        public void Configure(EntityTypeBuilder<IndexPoint> builder)
        {
            builder.ToTable("IndexPoint");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(150)").IsRequired();

            builder.HasMany(x => x.DocumentsMetadata)
                   .WithMany(i => i.IndexPoints);
        }
    }
}
