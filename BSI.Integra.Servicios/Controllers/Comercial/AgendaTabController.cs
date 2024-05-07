using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interfaz;
using BSI.Integra.Aplicacion.DTOs.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Validators.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: AgendaTabController
    /// Autor: Flavio R.M.F.
    /// Fecha: 06/05/2024
    /// <summary>
    /// Gestión de AgendaTab
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class AgendaTabController : ControllerBase
    {
        private IAgendaTabService _agendaTabService;
        private ITokenManager _tokenManager;
        public AgendaTabController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _agendaTabService = new AgendaTabService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de AgendaTab
        /// </summary>
        /// <returns>Registros AgendaTab</returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var respuesta = _agendaTabService.Obtener();
            return Ok(respuesta);
        }
        /// Tipo Función: POST
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Versión: 1.1
        /// <summary>
        /// Inserta un nuevo registro de AgendaTab
        /// </summary>
        /// <param name="model">Objeto de tipo AgendaTabDTO</param>
        /// <returns>Nuevo registro AgendaTabDTO</returns>
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] AgendaTabDTO model)
        {
            if (model == null) return BadRequest(new { ErrorBody = "No se envio un modelo valido." });

            var validator = new AgendaTabValidator(false);
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid || !ModelState.IsValid)
                return BadRequest(new { ErrorModelo = validationResult.Errors.Select(e => e.ErrorMessage) });

            var respuesta = _agendaTabService.Insertar(model, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza un registro de AgendaTab
        /// </summary>
        /// <param name="model">Objeto de tipo AgendaTabDTO</param>
        /// <param name="usuario">Usuario modificacion</param>
        /// <returns>Registro AgendaTabDTO</returns>
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] AgendaTabDTO model)
        {
            if (model == null) return BadRequest(new { ErrorBody = "No se envio un modelo valido." });
            var validator = new AgendaTabValidator(true);
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
                return BadRequest(new { ErrorModelo = validationResult.Errors.Select(e => e.ErrorMessage) });

            var respuesta = _agendaTabService.Actualizar(model, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R.M.F.
        /// Fecha: 06/05/2024
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de AgendaTab por el id
        /// </summary>
        /// <param name="id">Id principal de AgendaTab</param>
        /// <param name="usuario">Usuario eliminacion</param>
        /// <returns>Resultado de la eliminacion</returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = _agendaTabService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
