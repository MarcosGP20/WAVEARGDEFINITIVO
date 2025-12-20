using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace Data.Manager
    {
        public class ProductoManager
        {
            private readonly ApplicationDbContext _context;

            public ProductoManager(ApplicationDbContext context)
            {
                _context = context;
            }

            // --- FORMULARIO 1: CREAR SOLO EL MODELO BASE ---
            public async Task CrearProducto(ProductoCreateDto dto)
            {
                // 1. Creamos la entidad principal (El Padre)
                var nuevoProducto = new Productos
                {
                    Nombre = dto.Nombre,
                    Modelo = dto.Modelo,
                    Descripcion = dto.Descripcion,
                    Activo = true,

                    // 2. Inicializamos la lista de imágenes
                    Imagenes = new List<Imagen>()
                };

                // 3. Agregamos las imágenes si existen
                if (dto.ImagenesUrls != null && dto.ImagenesUrls.Count > 0)
                {
                    foreach (var url in dto.ImagenesUrls)
                    {
                        nuevoProducto.Imagenes.Add(new Imagen
                        {
                            Url = url,
                            EsPrincipal = false
                        });
                    }

                    // Marcamos la primera como principal automáticamente
                    nuevoProducto.Imagenes.First().EsPrincipal = true;
                }

                _context.Productos.Add(nuevoProducto);
                await _context.SaveChangesAsync();
            }

        // --- LISTADO ---
        public async Task<List<ProductoDto>> ObtenerTodos(bool soloDisponibles)
        {
            var query = _context.Productos
                .Include(p => p.Imagenes)
                .Include(p => p.Variantes) // Importante: Traemos variantes para chequear stock
                .AsQueryable();

            // INTERRUPTOR ACTIVADO:
            // Si soloDisponibles es TRUE, filtramos los que tengan AL MENOS UNA variante con stock > 0.
            if (soloDisponibles)
            {
                query = query.Where(p => p.Variantes.Any(v => v.Stock > 0));
            }

            var productosDb = await query.ToListAsync();

            // Mapeo manual a DTO (igual que antes)
            return productosDb.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Modelo = p.Modelo,
                Descripcion = p.Descripcion,
                Imagenes = p.Imagenes.Select(i => i.Url).ToList(),
                Variantes = p.Variantes.Select(v => new ProductoVarianteDto
                {
                    Id = v.Id,
                    Color = v.Color,
                    Memoria = v.Memoria,
                    Precio = v.Precio,
                    Stock = v.Stock,
                    EsUsado = v.EsUsado,
                    FotoEstadoUrl = v.FotoEstadoUrl
                }).ToList()
            }).ToList();
        }


        // --- ACTUALIZAR SOLO DATOS BASE ---
        public async Task<bool> ActualizarProducto(ProductoUpdateDto dto)
        {
            var productoDb = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (productoDb == null) return false;

            // Actualizamos los campos
            productoDb.Nombre = dto.Nombre;
            productoDb.Modelo = dto.Modelo;
            productoDb.Descripcion = dto.Descripcion;

            // --- AGREGAMOS ESTO PARA FORZAR EL UPDATE ---
            // Le decimos explícitamente a la BD: "Este objeto fue modificado"
            _context.Entry(productoDb).State = EntityState.Modified;

            // Guardamos los cambios
            await _context.SaveChangesAsync();

            return true;
        }


        // --- ELIMINAR PRODUCTO Y SUS VARIANTES ---
        public async Task<bool> EliminarProducto(int id)
        {
            // 1. Buscamos el producto INCLUYENDO sus relaciones (Imagenes y Variantes)

            var productoDb = await _context.Productos
                .Include(p => p.Imagenes)    // <--- Traemos las fotos
                .Include(p => p.Variantes)   // <--- Traemos las variantes (por seguridad, por si quedó alguna)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (productoDb == null) return false;

            _context.Productos.Remove(productoDb);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductoDto> ObtenerProductoPorId(int id)
        {
            // Buscamos el producto y "cargamos" sus hijos (Fotos y Variantes)
            var productoDb = await _context.Productos
                .Include(p => p.Imagenes)
                .Include(p => p.Variantes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (productoDb == null) return null;

            return new ProductoDto
            {
                Id = productoDb.Id,
                Nombre = productoDb.Nombre,
                Modelo = productoDb.Modelo,
                Descripcion = productoDb.Descripcion,

                // Mapeamos las URLs de las imagenes
                Imagenes = productoDb.Imagenes.Select(i => i.Url).ToList(),

                // Mapeamos las variantes
                Variantes = productoDb.Variantes.Select(v => new ProductoVarianteDto
                {
                    Id = v.Id,
                    Color = v.Color,
                    Memoria = v.Memoria,
                    Precio = v.Precio,
                    Stock = v.Stock,
                    EsUsado = v.EsUsado,
                    FotoEstadoUrl = v.FotoEstadoUrl
                }).ToList()
            };
        }

        // --- GESTIÓN DE IMÁGENES ---

        public async Task<bool> AgregarImagen(Data.Dtos.ImagenDto dto)
        {
            // 1. Verificamos que el producto exista antes de pegarle la foto
            var existeProducto = await _context.Productos.AnyAsync(p => p.Id == dto.ProductoId);
            if (!existeProducto) return false;

            // 2. Creamos la entidad Imagen
            var nuevaImagen = new Data.Entities.Imagen
            {
                ProductoId = dto.ProductoId,
                Url = dto.Url
            };

            _context.Imagenes.Add(nuevaImagen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarImagen(int idImagen)
        {
            // 1. Buscamos la foto por su ID propio
            var imagen = await _context.Imagenes.FindAsync(idImagen);
            if (imagen == null) return false;

            // 2. La borramos
            _context.Imagenes.Remove(imagen);
            await _context.SaveChangesAsync();
            return true;
        }
    }
  }



