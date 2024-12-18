using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Domain.Models;
using Projeto.Infra.Data.Constants;
using static Projeto.Infra.Data.Constants.ModelMaxLengthConstants;

namespace Projeto.Infra.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Login)
                .HasMaxLength(ModelMaxLengthConstants.Usuario.Login);

            builder.Property(u => u.Password);

            builder.Property(t => t.Ativo)
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
