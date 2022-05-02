
using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoWK.Dominio.Entidades
{
    public class Categoria : EntidadeBase
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome pode ter somente 100 caracteres no máximo")]
        public string Nome { get; set; } 
        
        //public ICollection<Produto>? Produtos { get; set; }
    }
}
