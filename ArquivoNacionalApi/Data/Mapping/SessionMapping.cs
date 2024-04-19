using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Mapping
{
    public class SessionMapping : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("session");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.PlayerLimit).HasColumnName("playerLimit").HasColumnType("int").IsRequired();

            builder.HasMany(x => x.Users)
                   .WithOne()
                   .HasPrincipalKey(x => x.Id);
        }
    }
}
