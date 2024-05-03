using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Repositorio.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IConnectionFactory _connectionFactory;
        private IntegraDBContext _context;
        private bool _disposed;
        private IDapperRepository _dapperRepository;

        public UnitOfWork(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            _context = context;
            _connectionFactory = connectionFactory;
            _dapperRepository = dapperRepository;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Rollback()
        {
            try
            {
                // Descartar los cambios no guardados
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DetachAll()
        {
            try
            {
                foreach (var entry in _context.ChangeTracker.Entries().ToArray())
                {
                    entry.State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}