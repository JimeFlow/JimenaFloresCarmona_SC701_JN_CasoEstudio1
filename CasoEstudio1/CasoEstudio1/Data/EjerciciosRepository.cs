using CasoEstudio1.Models;
using Dapper;
using System.Data;

namespace CasoEstudio1.Data
{
    public class EjerciciosRepository : IEjerciciosRepository
    {
        private readonly DapperContext _context;

        public EjerciciosRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string? Message)> CreateAsync(EjercicioCreateModel model)
        {
            using var conn = _context.CreateConnection();
            var p = new DynamicParameters();
            p.Add("@Identificacion", model.Identificacion, DbType.String);
            p.Add("@Monto", model.Monto, DbType.Decimal);
            p.Add("@DescripcionTipoEjercicio", model.DescripcionTipoEjercicio, DbType.String);

            // sp returns a single-row with Result integer
            var result = await conn.QueryFirstOrDefaultAsync<int>("InsertEjercicio_SP", p, commandType: CommandType.StoredProcedure);

            return result switch
            {
                1 => (true, null),
                -1 => (false, "El tipo de ejercicio indicado no existe."),
                -2 => (false, "Ya se han solicitado 2 tipos distintos para esta identificación. No se permiten más."),
                _ => (false, "Error desconocido al crear la solicitud.")
            };
        }

        public async Task<IEnumerable<EjercicioCreateModel>> GetAllAsync()
        {
            using var conn = _context.CreateConnection();
            var items = await conn.QueryAsync<EjercicioCreateModel>("GetEjercicios_SP", commandType: CommandType.StoredProcedure);
            return items;
        }
    }
}