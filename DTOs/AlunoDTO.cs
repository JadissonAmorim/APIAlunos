using System.ComponentModel.DataAnnotations;

namespace API_Alunos.DTOs
{
    public class AlunoDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Idade { get; set; }

    }
}
