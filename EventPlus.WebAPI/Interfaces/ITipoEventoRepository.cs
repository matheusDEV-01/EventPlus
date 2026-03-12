using EventPlu.WebAPI.Models;

namespace EventPlu.WebAPI.Interfaces;

public interface ITipoEventoRepository
{
    List<TipoEvento> Listar();
    void Cadastrar(TipoEvento tipoEvento);
    
    void Atualizar(Guid id, TipoEvento tipoEvento);

    void Deletar(Guid id);
    TipoEvento BuscarPorId(Guid id);



}
