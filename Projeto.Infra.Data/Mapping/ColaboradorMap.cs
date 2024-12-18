using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Infra.Data.Constants;
using Projeto.Models;

namespace Projeto.Infra.Data.Mapping
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(t => t.Nome)
                .HasMaxLength(ModelMaxLengthConstants.Nome)
                .IsRequired();

        }
    }
}
