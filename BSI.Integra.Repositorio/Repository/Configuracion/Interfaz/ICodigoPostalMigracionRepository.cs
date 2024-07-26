

using BSI.Integra.Aplicacion.DTOs.IntegraDB.Configuracion;

namespace BSI.Integra.Repositorio.Repository.Configuracion.Interfaz
{
    public interface ICodigoPostalMigracionRepository
    {
        void InsertarCodigoPostalMigracion(CodigoPostalMigracionDTO model);
    }
}
