
using BSI.Integra.Aplicacion.DTOs.IntegraDB.Comercial;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interfaz
{
    public interface IAgendaTabService
    {
        IEnumerable<AgendaTabDTO> Obtener();
        AgendaTabDTO Actualizar(AgendaTabDTO model, string usuario);
        AgendaTabDTO Insertar(AgendaTabDTO model, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
