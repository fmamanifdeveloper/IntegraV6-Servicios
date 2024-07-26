
using BSI.Integra.Aplicacion.DTOs.IntegraDB.Configuracion;
using BSI.Integra.Repositorio.Repository.Configuracion.Interfaz;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Configuracion.Implementacion
{
    public class CodigoPostalMigracionRepository : ICodigoPostalMigracionRepository
    {
        private IDapperRepository _dapperRepository;
        public CodigoPostalMigracionRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        public void InsertarCodigoPostalMigracion(CodigoPostalMigracionDTO model)
        {
            try
            {
                var query = "conf.SP_TCodigoPostalMigracion_Insertar";
                var respuesta = _dapperRepository.QuerySPDapper(query, model);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
