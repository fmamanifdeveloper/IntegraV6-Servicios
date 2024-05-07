

using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interfaz;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    public class AlumnoService : IAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public AlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {

                }
            );
            _mapper = new Mapper(config);
        }
    }
}
