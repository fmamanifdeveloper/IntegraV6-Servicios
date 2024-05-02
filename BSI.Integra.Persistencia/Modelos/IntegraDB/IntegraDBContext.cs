using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class IntegraDBContext : DbContext
    {
        public IntegraDBContext()
        {
        }

        public IntegraDBContext(DbContextOptions<IntegraDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAgendaTab> TAgendaTabs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=23.96.90.6;Initial Catalog=integraDB-MapeoV5;Persist Security Info=False;User ID=pase;Password=productionOn;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<TAgendaTab>(entity =>
            {
                entity.ToTable("T_AgendaTab", "com");

                entity.HasIndex(e => new { e.CodigoAreaTrabajo, e.Estado }, "INC_T_AgendaTabs_PorCodigoAreaTrabajo_PorEstado");

                entity.Property(e => e.Id).HasComment("Clave Primaria de la Tabla");

                entity.Property(e => e.CargarInformacionInicial).HasComment("Validadcion para cargar o no informacion de las actvidades en los Tabs de la Agenda");

                entity.Property(e => e.CodigoAreaTrabajo)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasComment("Estado del registro (creado o eliminado)");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Fecha creacion");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Usuario de creacion");

                entity.Property(e => e.IdMigracion).HasComment("Id de migracion de la tabla");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Nombre de los tabs de la Agenda");

                entity.Property(e => e.Numeracion).HasComment("Indica el orden de priorizacion de los Tab");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasComment("Campo de sistema automatico que guarda la version del registro");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Fecha de modificacion");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Usuario de modificacion");

                entity.Property(e => e.ValidarFecha).HasComment("Indica si requiere la validacion de la fecha de programacion");

                entity.Property(e => e.VisualizarActividad).HasComment("Validacion para Visualizar o no los Tabs de la Agenda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
