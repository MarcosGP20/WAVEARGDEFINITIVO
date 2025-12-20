using Data.Dtos;
using Data.Manager;
using WaveArg.Interfaces;

namespace WaveArg.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ProductoManager _productomanager;

        public ProductoService(ProductoManager productoManager)
        {
            _productomanager = productoManager;
        }

        public async Task CrearProducto(ProductoCreateDto dto)
        {
            // Delegamos al manager. Las validaciones de precios van ahora en Variantes.
            await _productomanager.CrearProducto(dto);
        }

        public async Task<List<ProductoDto>> ObtenerProductos(bool soloDisponibles)
        {
            return await _productomanager.ObtenerTodos(soloDisponibles);
        }

        public async Task<bool> ModificarProducto(ProductoUpdateDto dto)
        {
            return await _productomanager.ActualizarProducto(dto);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            return await _productomanager.EliminarProducto(id);
        }
        public async Task<ProductoDto> ObtenerProductoPorId(int id)
        {
            return await _productomanager.ObtenerProductoPorId(id);
        }
        public async Task<bool> AgregarImagen(Data.Dtos.ImagenDto dto)
        {
            return await _productomanager.AgregarImagen(dto);
        }

        public async Task<bool> EliminarImagen(int id)
        {
            return await _productomanager.EliminarImagen(id);
        }
    }
}
