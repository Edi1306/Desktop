using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SalaFitnessModel
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=SalaFitnessEntitiesModel")
        {
        }

        public virtual DbSet<Antrenori> Antrenoris { get; set; }
        public virtual DbSet<Clienti> Clientis { get; set; }
        public virtual DbSet<Exercitii> Exercitiis { get; set; }
        public virtual DbSet<Programare> Programares { get; set; }
        public virtual DbSet<Sala> Salas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Antrenori>()
                .HasMany(e => e.Programares)
                .WithOptional(e => e.Antrenori)
                .HasForeignKey(e => e.IdAntrenor)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Clienti>()
                .HasMany(e => e.Programares)
                .WithOptional(e => e.Clienti)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Sala>()
                .Property(e => e.Ziua)
                .IsUnicode(false);
        }
    }
}
