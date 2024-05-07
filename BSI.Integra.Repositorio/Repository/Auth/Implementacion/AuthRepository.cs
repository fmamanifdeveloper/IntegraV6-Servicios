
using BSI.Integra.Aplicacion.DTOs.Auth;
using BSI.Integra.Repositorio.Repository.Auth.Interfaz;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Auth.Implementacion
{
    public class AuthRepository : IAuthRepository
    {
        private IDapperRepository _dapperRepository;
        public AuthRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        public DatosTokenDTO? ObtenerDatosToken(UserCredentialDTO userCredential)
        {
            try
            {
                var query = @"
                        SELECT
                            Id,
                            PerId AS IdPersonal,
                            RolId AS IdRol,
                            ISNULL(AreaTrabajo, '') AS AreaTrabajo,
                            UserName,
                            TipoPersonal
                         FROM 
                            gp.V_ObtenerDatosToken
                         WHERE 
                            UserName = @UserName
                            AND UsClave = @UsClave";
                var respuesta = _dapperRepository.FirstOrDefault(query, new
                {
                    userCredential.UserName,
                    UsClave = userCredential.Password
                });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                {
                    return JsonConvert.DeserializeObject<DatosTokenDTO>(respuesta);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
