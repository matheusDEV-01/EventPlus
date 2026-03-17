using System.ComponentModel.DataAnnotations;

namespace EventPlu.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage = "O título do tipo de evento é obrigatório!")]
    public string? NomeFantasia { get; set; }
    public string? Cnpj { get; set; }
    public string? Endereco { get; set; }

}
