using BSI.Integra.Aplicacion.Base.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AgendaTab : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha { get; set; }
        public string? CodigoAreaTrabajo { get; set; }
    }
}
