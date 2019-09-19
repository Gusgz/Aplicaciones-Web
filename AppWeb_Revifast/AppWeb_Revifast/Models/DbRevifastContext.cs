using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppWeb_Revifast.Models
{
    public partial class DbRevifastContext : DbContext
    {
        public DbRevifastContext()
        {
        }

        public DbRevifastContext(DbContextOptions<DbRevifastContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Afiliado> Afiliado { get; set; }
        public virtual DbSet<Conductor> Conductor { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=DbRevifast;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Afiliado>(entity =>
            {
                entity.HasKey(e => e.IdAfiliado);

                entity.Property(e => e.Correo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Conductor>(entity =>
            {
                entity.HasKey(e => e.IdConductor);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdAfiliadoNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdAfiliado)
                    .HasConstraintName("FK_Reserva_Afiliado");

                entity.HasOne(d => d.IdConductorNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdConductor)
                    .HasConstraintName("FK_Reserva_Conductor");
            });
        }
    }
}
