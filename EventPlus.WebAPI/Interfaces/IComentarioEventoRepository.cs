using EventPlu.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IComentarioEventoRepository
{
    void Cadastrar(ComentarioEvento comentarioEventos);

    void Deletar(Guid id);

    List<ComentarioEvento>Listar(Guid IdEvento);
    List<ComentarioEvento> ListarSomenteExibe(Guid IdEventos);
}

