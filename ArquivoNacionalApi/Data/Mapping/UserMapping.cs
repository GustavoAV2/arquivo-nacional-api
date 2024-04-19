using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Password).HasColumnName("password").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.State).HasColumnName("state").HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(x => x.Session)
                   .WithMany(s => s.Users)
                   .HasForeignKey(x => x.SessionId);
        }
    }
}
