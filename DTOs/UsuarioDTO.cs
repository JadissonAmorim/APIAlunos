using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Alunos.DTOs
{

    [NotMapped]
    public class UsuarioDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
