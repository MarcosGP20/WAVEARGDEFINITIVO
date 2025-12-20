using Data.Dtos;
using System.Collections.Generic;

namespace WaveArg.Interfaces
{
    public interface IProductoService
    {
        //Solo se define QUE hace, no COMO
        Task CrearProducto(ProductoCreateDto dto);
        Task<List<ProductoDto>> ObtenerProductos(bool soloDisponibles);
        Task<bool> ModificarProducto(ProductoUpdateDto dto);
        Task<bool> EliminarProducto(int id);
        Task<ProductoDto> ObtenerProductoPorId(int id);
        Task<bool> AgregarImagen(Data.Dtos.ImagenDto dto);
        Task<bool> EliminarImagen(int id);
    }
}
