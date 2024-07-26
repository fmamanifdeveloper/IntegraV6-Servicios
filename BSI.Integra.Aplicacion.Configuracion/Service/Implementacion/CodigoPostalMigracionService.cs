
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Configuracion.Service.Interfaz;
using BSI.Integra.Aplicacion.DTOs.IntegraDB.Configuracion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32;

namespace BSI.Integra.Aplicacion.Configuracion.Service.Implementacion
{
    public class CodigoPostalMigracionService : ICodigoPostalMigracionService
    {
        private IUnitOfWork _unitOfWork;
        public CodigoPostalMigracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void ProcesarFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new BadRequestException("No se ha proporcionado ningún archivo o el archivo está vacío.");
                }
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var data = new List<string>();
                    var contador = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        contador++;
                        try
                        {
                            if (line != null)
                            {
                                if (line.EndsWith("|"))
                                {
                                    line += "<vacio>";
                                }
                                var listaDeCampos = line.Split("|");
                                var registro = new CodigoPostalMigracionDTO();
                                try { registro.d_codigo = listaDeCampos[0]; } catch { }
                                try { registro.d_asenta = listaDeCampos[1]; } catch { }
                                try { registro.d_tipo_asenta = listaDeCampos[2]; } catch { }
                                try { registro.D_mnpio = listaDeCampos[3]; } catch { }
                                try { registro.d_estado = listaDeCampos[4]; } catch { }
                                try { registro.d_ciudad = listaDeCampos[5]; } catch { }
                                try { registro.d_CP = listaDeCampos[6]; } catch { }
                                try { registro.c_estado = listaDeCampos[7]; } catch { }
                                try { registro.c_oficina = listaDeCampos[8]; } catch { }
                                try { registro.c_CP = listaDeCampos[9]; } catch { }
                                try { registro.c_tipo_asenta = listaDeCampos[10]; } catch { }
                                try { registro.c_mnpio = listaDeCampos[11]; } catch { }
                                try { registro.id_asenta_cpcons = listaDeCampos[12]; } catch { }
                                try { registro.d_zona = listaDeCampos[13]; } catch { }
                                try { registro.c_cve_ciudad = listaDeCampos[14]; } catch { }
                                _unitOfWork.CodigoPostalMigracionRepository.InsertarCodigoPostalMigracion(registro);
                            }
                        }
                        catch (Exception ex)
                        {
                            var registro = new CodigoPostalMigracionDTO();
                            _unitOfWork.CodigoPostalMigracionRepository.InsertarCodigoPostalMigracion(registro);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
