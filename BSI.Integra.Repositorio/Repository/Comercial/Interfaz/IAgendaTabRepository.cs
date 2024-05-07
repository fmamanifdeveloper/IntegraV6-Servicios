using BSI.Integra.Aplicacion.DTOs.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Comercial.Interfaz
{
    public interface IAgendaTabRepository : IGenericRepository<TAgendaTab>
    {
        #region Metodos Base
        TAgendaTab Add(AgendaTab entidad);
        IEnumerable<TAgendaTab> Add(IEnumerable<AgendaTab> listadoEntidad);
        TAgendaTab Update(AgendaTab entidad);
        IEnumerable<TAgendaTab> Update(IEnumerable<AgendaTab> listadoEntidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        AgendaTab? ObtenerPorId(int id);
        IEnumerable<AgendaTabDTO> Obtener();
    }
}
