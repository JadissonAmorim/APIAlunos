using API_Alunos.Validation;
using System.ComponentModel.DataAnnotations;

namespace API_Alunos.Models
{
    public class Aluno
    {
        [Key]

        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [StringLength(80, ErrorMessage = "Coloque um nome menor")]

        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Formato E-mail errado.")]
        [StringLength(100, ErrorMessage = "Adicione um E-mail menor")]

        public string Email { get; set; }

        [Required]

        public int Idade { get; set; }

        [Required]
        [ValidationCPF]
        public string CPF { get; set; }
    }
}
