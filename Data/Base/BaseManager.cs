using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Data.Base
{
    //Guarda los datos en la API (Usuarios, productos, etc)
    public abstract class BaseManager<T> where T : class
    {
        private static ApplicationDbContext contextInstance = null;

        public static ApplicationDbContext contextSingleton
        {
            get
            {
                if (contextInstance == null)
                {
                    contextInstance = new ApplicationDbContext();
                }
                return contextInstance;
            }
        }

        public abstract Task<List<T>> BuscarListaAsync();   //Se utiliza Async para que el proyecto siga andando mientras los datos se guardan
        public abstract Task<List<T>> BuscarAsync(T entity);
        public abstract Task<bool> Borrar(T entity);


        public async Task<bool> Guardar(T entity, int id)
        {
            if (id == 0)
            {
                contextSingleton.Entry(entity).State = EntityState.Added;
            }
            else
            {
                contextSingleton.Entry(entity).State = EntityState.Modified;
            }

            var resultado = await contextSingleton.SaveChangesAsync() > 0;

            contextSingleton.Entry(entity).State = EntityState.Detached;
            return resultado;
        }

        public async Task<bool> Eliminar(T entity)
        {
            contextSingleton.Entry(entity).State = EntityState.Modified;
            var resultado = await contextSingleton.SaveChangesAsync() > 0;
            return resultado;
        }

    }
}
