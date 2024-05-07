
using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interfaz;
using BSI.Integra.Aplicacion.DTOs.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    public class AgendaTabService : IAgendaTabService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public AgendaTabService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAgendaTab, AgendaTab>(MemberList.None).ReverseMap();
                    cfg.CreateMap<AgendaTab, AgendaTabDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TAgendaTab, AgendaTabDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de AgendaTab
        /// </summary>
        /// <returns>Registros AgendaTab</returns>
        public IEnumerable<AgendaTabDTO> Obtener()
        {
            return _unitOfWork.AgendaTabRepository.Obtener();
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuevo registro de AgendaTab
        /// </summary>
        /// <param name="model">Objeto de tipo AgendaTabDTO</param>
        /// <param name="usuario">Usuario registro</param>
        /// <returns>Nuevo registro AgendaTabDTO</returns>
        public AgendaTabDTO Insertar(AgendaTabDTO model, string usuario)
        {
            try
            {
                if (model != null)
                {
                    AgendaTab entidad = new()
                    {
                        Nombre = model.Nombre,
                        CodigoAreaTrabajo = model.CodigoAreaTrabajo,
                        VisualizarActividad = true,
                        CargarInformacionInicial = true,
                        Numeracion = 1,
                        ValidarFecha = true,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.AgendaTabRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<AgendaTabDTO>(respuesta);
                }
                else
                    throw new BadRequestException("#ATS-I-001@Modelo nulo");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza un registro de AgendaTab
        /// </summary>
        /// <param name="model">Objeto de tipo AgendaTabDTO</param>
        /// <param name="usuario">Usuario modificacion</param>
        /// <returns>Registro AgendaTabDTO</returns>
        public AgendaTabDTO Actualizar(AgendaTabDTO model, string usuario)
        {
            try
            {
                AgendaTab? entidad = new();
                if (model != null)
                {
                    if (model.Id != null && model.Id != 0)
                    {
                        entidad = _unitOfWork.AgendaTabRepository.ObtenerPorId(model.Id.Value);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = model.Nombre;
                            entidad.CodigoAreaTrabajo = model.CodigoAreaTrabajo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                        }
                        else
                            throw new BadRequestException($"#ATS-A-001@No existe la entidad AgendaTab con el id {model.Id.Value}");
                    }
                    else
                        throw new BadRequestException("#ATS-A-002@Id no valido");
                }
                else
                    throw new BadRequestException("#ATS-A-003@Modelo nulo");
                var respuesta = _unitOfWork.AgendaTabRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AgendaTabDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de AgendaTab por el id
        /// </summary>
        /// <param name="id">Id principal de AgendaTab</param>
        /// <param name="usuario">Usuario eliminacion</param>
        /// <returns>Resultado de la eliminacion</returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BadRequestException($"#ATS-E-001@El id no es valido");
                }
                var obj = _unitOfWork.AgendaTabRepository.ObtenerPorId(id);
                if (obj != null && obj.Id != 0)
                {
                    var respuesta = _unitOfWork.AgendaTabRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"#ATS-E-002@La entidad no existe y/o ya fue eliminada");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
