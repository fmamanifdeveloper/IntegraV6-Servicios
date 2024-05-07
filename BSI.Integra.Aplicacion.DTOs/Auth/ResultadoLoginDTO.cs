
namespace BSI.Integra.Aplicacion.DTOs.Auth
{
    public class ResultadoLoginDTO
    {
        public int IdPersonal { get; set; }
        public int IdRol { get; set; }
        public string AreaTrabajo { get; set; }
        public string UserName { get; set; } = null!;
        public string TipoPersonal { get; set; } = null!;
        public string Token { get; set; }
    }
}
