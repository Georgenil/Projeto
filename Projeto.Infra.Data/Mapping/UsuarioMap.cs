using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Infra.Data.Constants;

namespace Projeto.Infra.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Domain.Models.Usuario>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.GUID)
                .HasColumnName("guid");

            builder.Property(u => u.Login).HasColumnName("login")
                .HasMaxLength(ModelMaxLengthConstants.Usuario.Login);

            builder.Property(u => u.Senha)
                .HasColumnName("senha");

            builder.Property(t => t.Ativo)
                .HasColumnName("ativo")
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
