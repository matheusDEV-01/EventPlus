using EventPlu.WebAPI.BdContextEvent;
using EventPlu.WebAPI.Interfaces;
using EventPlu.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlu.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventcontext;

    public PresencaRepository(EventContext context) 
    {
        _eventcontext = context;
    }

    public void Atualizar(Guid IdPresencaEvento)
    {
        var PresencaBuscado = _eventcontext.Presencas.Find(IdPresencaEvento);
        if (PresencaBuscado != null)
        {
            PresencaBuscado.Situacao = !PresencaBuscado.Situacao;
            
           

            //O SaveChanges() detecta as mudanças na propriedade "Titulo" automaticamente
            _eventcontext.SaveChanges();
        }
    }

  


    /// <summary>
    /// Buscar uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada </param>
    /// <returns>presença</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventcontext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var PresencaBuscado = _eventcontext.Presencas.Find(id);

        if (PresencaBuscado != null) 
        {
            _eventcontext.Presencas.Remove(PresencaBuscado);
            _eventcontext.SaveChanges(); 
        }
    }

    public void Inscrever(Presenca Inscricao)
    {
        _eventcontext.Presencas.Add(Inscricao);
        _eventcontext.SaveChanges();
    }

    public List<Presenca> Listar()
    {
        //return _eventcontext.Presencas.Include(p => p.IdEventoNavigation)
        // .Include(e => e!.IdUsuarioNavigation).ToList();
        return _eventcontext.Presencas.OrderBy(Presenca => Presenca.Situacao).ToList();
    }


    /// <summary>
    /// Lista as presenças de um usuário específico
    /// </summary>
    /// <param name="idUsuario">Id do usuário para filtragem</param>
    /// <returns>uma lista de pre</returns>
    public List<Presenca> ListarMinhas(Guid idUsuario)
    {
       return _eventcontext.Presencas //
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == idUsuario)
            .ToList();
    }

    
}
