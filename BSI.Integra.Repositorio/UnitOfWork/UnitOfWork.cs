using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Auth.Implementacion;
using BSI.Integra.Repositorio.Repository.Auth.Interfaz;
using BSI.Integra.Repositorio.Repository.Comercial.Implementacion;
using BSI.Integra.Repositorio.Repository.Comercial.Interfaz;
using BSI.Integra.Repositorio.Repository.Configuracion.Implementacion;
using BSI.Integra.Repositorio.Repository.Configuracion.Interfaz;
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
        private IAuthRepository _authRepository;
        IAuthRepository IUnitOfWork.AuthRepository
        {
            get
            {
                return _authRepository ?? new AuthRepository(_dapperRepository);
            }
        }
        private IAgendaTabRepository _agendaTabRepository;
        IAgendaTabRepository IUnitOfWork.AgendaTabRepository
        {
            get
            {
                return _agendaTabRepository ?? new AgendaTabRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
        private ICodigoPostalMigracionRepository _codigoPostalMigracionRepository;
        ICodigoPostalMigracionRepository IUnitOfWork.CodigoPostalMigracionRepository
        {
            get
            {
                return _codigoPostalMigracionRepository ?? new CodigoPostalMigracionRepository(_dapperRepository);
            }
        }
    }
}