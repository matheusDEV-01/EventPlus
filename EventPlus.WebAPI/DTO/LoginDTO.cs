using System.ComponentModel.DataAnnotations;

namespace EventPlu.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do usuário é obrigatório!")]

    public string? Email { get; set; }
    public string? Senha { get; set; }
}
