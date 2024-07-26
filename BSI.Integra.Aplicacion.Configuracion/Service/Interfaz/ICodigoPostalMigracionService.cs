
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Configuracion.Service.Interfaz
{
    public interface ICodigoPostalMigracionService
    {
        void ProcesarFile(IFormFile archivoExcel);
    }
}
