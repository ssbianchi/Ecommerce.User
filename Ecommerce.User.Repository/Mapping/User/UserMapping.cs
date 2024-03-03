using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.User.Repository.Mapping.User
{
    public class UserMapping : IEntityTypeConfiguration<Ecommerce.User.Domain.Entity.User.User>
    {
        public void Configure(EntityTypeBuilder<Ecommerce.User.Domain.Entity.User.User> builder)
        {
            builder.ToTable("System_Users");
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).IsRequired().HasColumnName("Id");
            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome");
            builder.Property(x => x.Login).IsRequired().HasColumnName("Login");
            builder.Property(x => x.Password).IsRequired().HasColumnName("Password");
            builder.Property(x => x.Email).IsRequired().HasColumnName("Email");
        }
    }
}
