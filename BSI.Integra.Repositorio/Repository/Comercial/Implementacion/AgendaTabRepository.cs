
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Comercial.Interfaz;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Comercial.Implementacion
{
    public class AgendaTabRepository : GenericRepository<TAgendaTab>, IAgendaTabRepository
    {
        private Mapper _mapper;

        public AgendaTabRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAgendaTab, AgendaTab>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAgendaTab MapeoEntidad(AgendaTab entidad)
        {
            try
            {
                TAgendaTab modelo = _mapper.Map<TAgendaTab>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAgendaTab Add(AgendaTab entidad)
        {
            try
            {
                var AgendaTab = MapeoEntidad(entidad);
                Insert(AgendaTab);
                return AgendaTab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TAgendaTab> Add(IEnumerable<AgendaTab> listadoEntidad)
        {
            try
            {
                List<TAgendaTab> listado = new List<TAgendaTab>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAgendaTab Update(AgendaTab entidad)
        {
            try
            {
                var AgendaTab = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AgendaTab.RowVersion = entidadExistente.RowVersion;

                base.Update(AgendaTab);
                return AgendaTab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TAgendaTab> Update(IEnumerable<AgendaTab> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAgendaTab> listado = new List<TAgendaTab>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
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
                return base.Delete(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                return base.Delete(listadoIds, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Flavio R. M. F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la tabla T_AgendaTab por su Id.
        /// </summary>
        /// <param name="id"> Id principal de AgentaTab </param>
        /// <returns>Entidad AgendaTab</returns>
        public AgendaTab? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
                        Nombre,
                        VisualizarActividad,
                        CargarInformacionInicial,
                        Numeracion,
                        ValidarFecha,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        CodigoAreaTrabajo,
                        Ponderacion,
                        AplicaMarcadorPredictivo
                    FROM com.T_AgendaTab
                    WHERE Estado = 1
                        AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<AgendaTab>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. M. F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla T_AgendaTab.
        /// </summary>
        /// <returns>Lista de objetos AgendaTabDTO</returns>
        public IEnumerable<AgendaTabDTO> Obtener()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        Nombre,
                        VisualizarActividad,
                        CargarInformacionInicial,
                        Numeracion,
                        ValidarFecha,
                        CodigoAreaTrabajo,
                        Ponderacion,
                        AplicaMarcadorPredictivo
                    FROM com.T_AgendaTab WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<AgendaTabDTO>>(resultado)!;
                }
                return new List<AgendaTabDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
