using Aliquota.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Infrasctructure.Context
{
    public class AliquotaContext:DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=db;database=aliquota;user=admin;password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity => 
            {
                entity.ToTable("produto");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
            });

            modelBuilder.Entity<Movimentacao>(entity => 
            {
                entity.ToTable("movimentacao");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Valor).IsRequired();
                entity.Property(e => e.DataMovimentacao).IsRequired();
                entity.Property(e => e.Tipo).HasConversion<string>().IsRequired();
            });
        }
    }
}