﻿using BSI.Integra.Aplicacion.Base.Base;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseIntegraEntity
    {
        protected internal readonly IConnectionFactory _connectionFactory;
        private IntegraDBContext _context;
        internal IDapperRepository _dapperRepository;
        internal DbSet<TEntity> _entities;
        public GenericRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            this._connectionFactory = connectionFactory;
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _entities = context.Set<TEntity>();
            _dapperRepository = dapperRepository;
        }
        public bool Insert(TEntity entidad)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException("La Entidad a insertar es nula");

                _entities.Add(entidad);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> InsertAsync(TEntity entidad)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException("La Entidad a insertar es nula");

                await _entities.AddAsync(entidad);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(IEnumerable<TEntity> list)
        {
            try
            {
                if (list == null)
                    throw new ArgumentNullException("El listado a Insertar es nulo");

                _entities.AddRange(list);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(TEntity entidad)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException($"Entidad id: {entidad.Id} a actualizar es nula");

                _entities.Update(entidad);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(IEnumerable<TEntity> list)
        {
            try
            {
                if (list == null)
                    throw new ArgumentNullException("El listado a Actualizar es nulo");
                _entities.UpdateRange(list);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int id, string usuario)
        {
            try
            {
                var entidad = FirstBy(w => w.Id == id && w.Estado == true);
                if (entidad == null)
                    throw new ArgumentNullException($"La Entidad id: {id}, es nula y/o ya fue eliminada");
                if (string.IsNullOrEmpty(usuario) || (usuario != null && usuario.Trim() == ""))
                    throw new ArgumentNullException("El nombre de usuario es nulo y/o no se proporcionó");

                if ((bool)typeof(TEntity).GetProperty("Estado").GetValue(entidad) == false)
                    throw new ArgumentNullException($"Elemento id: {id}, ya fue eliminado previamente");

                typeof(TEntity).GetProperty("Estado").SetValue(entidad, false);
                typeof(TEntity).GetProperty("UsuarioModificacion").SetValue(entidad, usuario);
                typeof(TEntity).GetProperty("FechaModificacion").SetValue(entidad, DateTime.UtcNow.AddHours(-5));
                //_context.Set<TEntity>().Update(entidad);
                //bool estado = _context.SaveChanges() >= 0;
                //_context.Entry(entidad).State = EntityState.Detached;
                _entities.Update(entidad);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(IEnumerable<int> list, string nombreUsuario)
        {
            try
            {
                foreach (var id in list)
                {
                    bool resultado = Delete(id, nombreUsuario);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Exist(int id)
        {
            try
            {
                return _entities.Any(w => w.Id == id && w.Estado == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Exist(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return _entities.Where(w => w.Estado == true).Where(filter).Any();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEntity FirstById(int id)
        {
            try
            {
                if (!Exist(id))
                    throw new Exception($"La entidad con Id {id} de {typeof(TEntity)} no existe");

                TEntity entidad = _entities.AsNoTracking().FirstOrDefault(w => w.Id == id && w.Estado == true);

                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEntity FirstBy(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return _entities.AsNoTracking().Where(w => w.Estado == true).Where(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TType FirstBy<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
            where TType : class
        {
            try
            {
                return _entities.AsNoTracking().Where(w => w.Estado == true).Where(where).Select(select).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return _entities.AsNoTracking().Where(w => w.Estado == true).Where(filter).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return _entities.AsNoTracking().Where(w => w.Estado == true).Where(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ICollection<TType> GetBy<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            try
            {
                return _entities.AsNoTracking().Where(w => w.Estado == true).Where(where).Select(select).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<TType> GetByQuery<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            try
            {
                return _entities.AsNoTracking().Where(w => w.Estado == true).Where(where).Select(select);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<TEntity> GetAll()
        {
            return _entities.AsNoTracking().Where(w => w.Estado == true);
        }
        public IEnumerable<TEntity> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, KProperty>> orderBy, bool ascending)
        {
            try
            {
                var rpta = _entities.Where(e => e.Estado == true);

                if (filters != null && filters.Count() > 0)
                {
                    foreach (var filter in filters)
                    {
                        rpta = rpta.AsNoTracking().Where(filter);
                    }
                }

                if (ascending)
                {
                    return rpta.OrderBy(orderBy).AsNoTracking();
                }
                else
                {
                    return rpta.OrderByDescending(orderBy).AsNoTracking();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<TEntity> GetFilteredQuery<KProperty>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, KProperty>> orderBy, bool ascending)
        {
            try
            {
                var rpta = _entities.Where(e => e.Estado == true);
                if (filters != null && filters.Count() > 0)
                {
                    foreach (var filter in filters)
                    {
                        rpta = rpta.AsNoTracking().Where(filter);
                    }
                }
                if (ascending)
                {
                    return rpta.OrderBy(orderBy).AsNoTracking();
                }
                else
                {
                    return rpta.OrderByDescending(orderBy).AsNoTracking();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal GetMaxDecimal(Func<TEntity, decimal> columnSelector)
        {
            try
            {
                var GetMaxId = _entities.AsNoTracking().Where(w => w.Estado == true).Max(columnSelector);
                return GetMaxId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetMaxInt(Func<TEntity, int> columnSelector)
        {
            try
            {
                var GetMaxId = _entities.AsNoTracking().Where(w => w.Estado == true).Max(columnSelector);
                return GetMaxId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
