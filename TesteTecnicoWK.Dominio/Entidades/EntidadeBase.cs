using System;
using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoWK.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        [Display(Name = "Código")]
        public Guid Id { get; set; }

        [Display(Name = "Data de Cadastro")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Dt_Inclusao { get; set; }

        [Display(Name = "Data de Alteração")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Dt_Alteracao { get; set; }
        public EntidadeBase()
        {
            Id = Guid.NewGuid();
            Dt_Inclusao = DateTime.Now; 
        }
    }
}
