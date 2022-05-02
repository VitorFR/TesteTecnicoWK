using Microsoft.EntityFrameworkCore;
using TesteTecnicoWK.Data.Entity.MySQL.Contexto;
using TesteTecnicoWK.Dominio.Entidades;

namespace TesteTecnicoWK.Data.Entity.MySQL.Repositorio
{
    public class CategoriaRepositorio
    {
        private readonly MySqlDbContext _contexto;

        public CategoriaRepositorio(MySqlDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Categoria>> Listar()
        { 
            return await _contexto.Categorias.OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<Categoria> Get(Guid id)
        {
            return await _contexto.Categorias.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Categoria> Insert(Categoria categoria)
        {
            try
            {                
                _contexto.Add(categoria);
                await _contexto.SaveChangesAsync();
                return categoria;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Categoria> Edit(Categoria categoria)
        {
            try
            {              
                _contexto.Entry(categoria).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return categoria;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Boolean> Delete(Categoria categoria)
        {
            try
            {              
                _contexto.Entry(categoria).State = EntityState.Deleted;
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
