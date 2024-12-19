using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Infra.Data.Constants;
using Projeto.Domain.Models;

namespace Projeto.Infra.Data.Mapping
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.ToTable("colaborador");

            builder.HasKey(x => x.Id);


            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(t => t.Nome)
                .HasColumnName("nome")
                .HasMaxLength(ModelMaxLengthConstants.Nome)
                .IsRequired();

        }
    }
}
