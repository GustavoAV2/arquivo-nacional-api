using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Mapping
{
    public class DocumentMapping : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("document");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.FilePath).HasColumnName("filePath").HasColumnType("varchar(400)").IsRequired();
        }
    }
}
