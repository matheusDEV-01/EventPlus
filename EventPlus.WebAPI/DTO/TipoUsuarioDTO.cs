using System.ComponentModel.DataAnnotations;

namespace EventPlu.WebAPI.DTO;

public class TipoUsuarioDTO
{
    [Required(ErrorMessage = "O título do tipo de evento é obrigatório!")]
    public string? Titulo { get; set; }
}
