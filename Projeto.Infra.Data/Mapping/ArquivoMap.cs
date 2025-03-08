using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Domain.Models;
using Projeto.Infra.Data.Constants;

namespace Projeto.Infra.Data.Mapping
{
    public class ArquivoMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.ToTable("arquivo");

            builder.HasKey(c => c.Id);


            builder.Property(c => c.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasMaxLength(ModelMaxLengthConstants.Nome)
                .IsRequired();

            builder.Property(c => c.Extensao)
                .HasColumnName("extensao")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.Bytes)
                .HasColumnName("bytes")
                .IsRequired();
        }
    }
}
