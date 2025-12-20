using Data.Dtos;
using Data.Entities;

namespace WaveArg.Interfaces
{
    public interface IVarianteService
    {
        Task<ProductoVariante> AgregarVariante(ProductoVarianteCreateDto dto);
        Task<bool> ModificarVariante(ProductoVarianteUpdateDto dto);
        Task<bool> EliminarVariante(int id);
    }
}
