
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteTecnicoWK.Dominio.Entidades
{
    public class Produto : EntidadeBase
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome pode ter somente 100 caracteres no máximo")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(300, ErrorMessage = "A Descrição pode ter somente 300 caracteres no máximo")]
        public string? Descricao { get; set; }

        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double Preco_Venda { get; set; }
        public Guid IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        public Produto()
        {
            Preco_Venda = 0.00;
        }
    }
}
