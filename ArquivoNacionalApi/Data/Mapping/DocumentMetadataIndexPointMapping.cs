using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Mapping
{
    public class DocumentMetadataIndexPointMapping : IEntityTypeConfiguration<DocumentMetadataIndexPoint>
    {
        public void Configure(EntityTypeBuilder<DocumentMetadataIndexPoint> builder)
        {
            builder.ToTable("documentMetadataIndexPoint");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();

            builder.HasOne(x => x.Document)
                   .WithMany()
                   .HasForeignKey(x => x.DocumentId)
                   .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.IndexPoint)
                   .WithMany()
                   .HasForeignKey(x => x.IndexPointId)
                   .HasPrincipalKey(x => x.Id);
        }
    }
}
