
namespace BSI.Integra.Aplicacion.DTOs.Auth
{
    public class DatosTokenDTO
    {
        public string Id { get; set; } = null!;
        public int IdPersonal { get; set; }
        public int IdRol { get; set; }
        public string AreaTrabajo { get; set; }
        public string UserName { get; set; } = null!;
        public string TipoPersonal { get; set; } = null!;
    }
}
