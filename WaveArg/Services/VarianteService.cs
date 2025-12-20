using Data;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using WaveArg.Interfaces;

namespace WaveArg.Services
{
    public class VarianteService : IVarianteService
    {
        private readonly ApplicationDbContext _context;

        public VarianteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductoVariante> AgregarVariante(ProductoVarianteCreateDto dto)
        {
            // 1. Convertimos el DTO a la Entidad (Mapeo manual para no complicarte con AutoMapper ahora)
            var nuevaVariante = new ProductoVariante
            {
                ProductoId = dto.ProductoId,
                Color = dto.Color,
                Memoria = dto.Memoria,
                Precio = dto.Precio,
                Stock = dto.Stock,
                EsUsado = dto.EsUsado,
                DetalleEstado = dto.EsUsado ? dto.DetalleEstado : null, // Validación simple
                FotoEstadoUrl = dto.EsUsado ? dto.FotoEstadoUrl : null
            };

            // 2. Guardamos en Base de Datos usando tu Contexto
            _context.productoVariantes.Add(nuevaVariante);
            await _context.SaveChangesAsync();

            return nuevaVariante;
        }
        public async Task<bool> ModificarVariante(ProductoVarianteUpdateDto dto)
        {
            var varianteDb = await _context.productoVariantes.FindAsync(dto.Id);

            if (varianteDb == null) return false;

            // Actualizamos datos
            varianteDb.Precio = dto.Precio;
            varianteDb.Stock = dto.Stock;
            varianteDb.Color = dto.Color; // Por si corrige el color
            varianteDb.Memoria = dto.Memoria;
            varianteDb.EsUsado = dto.EsUsado;
            varianteDb.DetalleEstado = dto.DetalleEstado;

            // Forzamos el estado modificado (por seguridad)
            _context.Entry(varianteDb).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarVariante(int id)
        {
            var varianteDb = await _context.productoVariantes.FindAsync(id);

            if (varianteDb == null) return false;

            _context.productoVariantes.Remove(varianteDb);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

