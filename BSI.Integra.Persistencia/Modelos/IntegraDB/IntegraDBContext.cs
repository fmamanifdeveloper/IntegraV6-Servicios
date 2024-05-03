using Microsoft.EntityFrameworkCore;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<TAgendaTab>(entity =>
            {
                entity.ToTable("T_AgendaTab", "com");

                entity.HasIndex(e => new { e.CodigoAreaTrabajo, e.Estado }, "INC_T_AgendaTabs_PorCodigoAreaTrabajo_PorEstado");

                entity.Property(e => e.Id).HasComment("Clave Primaria de la Tabla");

                entity.Property(e => e.AplicaMarcadorPredictivo).HasComment("Indica si el tab aplica o no para marcador predictivo");

                entity.Property(e => e.CargarInformacionInicial).HasComment("Validadcion para cargar o no informacion de las actvidades en los Tabs de la Agenda");

                entity.Property(e => e.CodigoAreaTrabajo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasComment("Campo CodigoAreaTrabajo Que define a que área de trabajo pertene el tab");

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

                entity.Property(e => e.Ponderacion).HasComment("Ponderacion del tab para el marcador predictivo");

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
