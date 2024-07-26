using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interfaz;
using BSI.Integra.Aplicacion.Configuracion.Service.Implementacion;
using BSI.Integra.Aplicacion.Configuracion.Service.Interfaz;
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
    [AllowAnonymous]
    public class CodigoPostalMigracionController : ControllerBase
    {
        private ICodigoPostalMigracionService _service;
        public CodigoPostalMigracionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _service = new CodigoPostalMigracionService(unitOfWork);
        }
        [HttpPost("[action]")]
        public IActionResult ProcesarFile([FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest(new { ErrorBody = "No se envio un archivo valido." });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _service.ProcesarFile(file);
            return Ok();
        }
    }
}
