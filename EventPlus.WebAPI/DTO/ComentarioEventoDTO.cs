using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlu.WebAPI.DTO;

public class ComentarioEventoDTO
{
    public string Descricao { get; set; } = null!;
    public Guid? IdUsuario { get; set; }

    public Guid? IdEvento { get; set; }
}
