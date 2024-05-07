namespace BSI.Integra.Repositorio.Repository
{
    public interface IDapperRepository
    {
        string QueryDapper(string sql, object? parametros = null);
        string QueryDapper(string sql, object? parametros, int timeoutMinutos);
        Task<string> QueryDapperAsync(string sql, object? parametros = null);
        Task<string> QueryDapperAsync(string sql, object? parametros, int timeoutMinutos);
        string FirstOrDefault(string sql, object? parametros = null);
        string FirstOrDefault(string sql, object? parametros, int timeoutMinutos);
        Task<string> FirstOrDefaultAsync(string sql, object? parametros = null);
        Task<string> FirstOrDefaultAsync(string sql, object? parametros, int timeoutMinutos);
        string QuerySPDapper(string sql, object? parametros = null);
        string QuerySPDapper(string sql, object? parametros, int timeoutMinutos);
        Task<string> QuerySPDapperAsync(string sql, object? parametros = null);
        Task<string> QuerySPDapperAsync(string sql, object? parametros, int timeoutMinutos);
        string QuerySPFirstOrDefault(string sql, object? parametros = null);
        string QuerySPFirstOrDefault(string sql, object? parametros, int timeoutMinutos);
        Task<string> QuerySPFirstOrDefaultAsync(string sql, object? parametros = null);
        Task<string> QuerySPFirstOrDefaultAsync(string sql, object? parametros, int timeoutMinutos);
    }
}
