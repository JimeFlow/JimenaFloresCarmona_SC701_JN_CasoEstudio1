using Microsoft.Data.SqlClient;
using System.Data;

namespace CasoEstudio1.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("CasoEstudio") 
                ?? throw new InvalidOperationException("Connection string 'CasoEstudio' not found in configuration (appsettings.json).");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
