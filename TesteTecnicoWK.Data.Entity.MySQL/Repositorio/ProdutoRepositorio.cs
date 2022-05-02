using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoWK.Data.Entity.MySQL.Contexto;
using TesteTecnicoWK.Dominio.Entidades;

namespace TesteTecnicoWK.Data.Entity.MySQL.Repositorio
{
    public class ProdutoRepositorio
    {
        private readonly MySqlDbContext _contexto;

        public ProdutoRepositorio(MySqlDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Produto>> Listar()
        {
            IEnumerable<Produto> produtos = await _contexto.Produtos.Include(p => p.Categoria).OrderBy(p => p.Nome).ToListAsync();
            return produtos;
        }

        public async Task<Produto> Get(Guid id)
        {
            return await _contexto.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> Insert(Produto produto)
        {
            try
            {
                _contexto.Add(produto);
                await _contexto.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Produto> Edit(Produto produto)
        {
            try
            {
                _contexto.Entry(produto).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Boolean> Delete(Produto produto)
        {
            try
            {
                _contexto.Entry(produto).State = EntityState.Deleted;
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
