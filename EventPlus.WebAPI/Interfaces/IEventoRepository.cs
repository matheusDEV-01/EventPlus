using EventPlu.WebAPI.Models;

namespace EventPlu.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    void Deletar(Guid id);
    void Atualizar(Guid id, Evento evento);
    List<Evento> ListarPorId(Guid idUsuario);
    List<Evento> ProximosEventos();

}



