using CasoEstudio1.Models;

namespace CasoEstudio1.Data
{
    public interface IEjerciciosRepository
    {
        Task<(bool Success, string? Message)> CreateAsync(EjercicioCreateModel model);
        Task<IEnumerable<EjerciciosModel>> GetAllAsync();
    }
}