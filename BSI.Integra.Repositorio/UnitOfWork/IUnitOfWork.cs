namespace BSI.Integra.Repositorio.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        void Dispose();
        void DetachAll();

    }
}