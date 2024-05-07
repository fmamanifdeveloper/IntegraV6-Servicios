
using System.Text.Json.Serialization;

namespace BSI.Integra.Aplicacion.DTOs.IntegraDB.Comercial
{
    public class AgendaTabDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        [JsonIgnore]
        public bool VisualizarActividad { get; set; }
        [JsonIgnore]
        public bool CargarInformacionInicial { get; set; }
        [JsonIgnore]
        public int Numeracion { get; set; }
        [JsonIgnore]
        public bool ValidarFecha { get; set; }
        public string? CodigoAreaTrabajo { get; set; }
        [JsonIgnore]
        public int? Ponderacion { get; set; }
        [JsonIgnore]
        public bool? AplicaMarcadorPredictivo { get; set; }
    }
}
