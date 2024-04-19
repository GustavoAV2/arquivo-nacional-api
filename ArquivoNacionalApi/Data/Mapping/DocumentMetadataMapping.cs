using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Mapping
{
    public class DocumentMetadataMapping : IEntityTypeConfiguration<DocumentMetadata>
    {
        public void Configure(EntityTypeBuilder<DocumentMetadata> builder)
        {
            builder.ToTable("documentMetadata");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.Points).HasColumnName("points").HasColumnType("int").HasMaxLength(100000);
            builder.Property(x => x.Title).HasColumnName("name").HasColumnType("varchar(300)");
            builder.Property(x => x.Context).HasColumnName("name").HasColumnType("varchar(300)");
            builder.Property(x => x.SocialMarkers).HasColumnName("name").HasColumnType("varchar(1000)");

            builder.HasOne(x => x.Document)
                   .WithMany()
                   .HasForeignKey(x => x.DocumentId)
                   .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .HasPrincipalKey(x => x.Id);
        }
    }
}
