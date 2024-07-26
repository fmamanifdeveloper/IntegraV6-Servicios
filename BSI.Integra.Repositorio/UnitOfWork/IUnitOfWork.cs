using BSI.Integra.Repositorio.Repository.Auth.Interfaz;
using BSI.Integra.Repositorio.Repository.Comercial.Interfaz;
using BSI.Integra.Repositorio.Repository.Configuracion.Interfaz;

namespace BSI.Integra.Repositorio.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        void Dispose();
        void DetachAll();
        IAuthRepository AuthRepository { get; }
        IAgendaTabRepository AgendaTabRepository { get; }
        ICodigoPostalMigracionRepository CodigoPostalMigracionRepository { get; }
    }
}