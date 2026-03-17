using EventPlu.WebAPI.BdContextEvent;
using EventPlu.WebAPI.Interfaces;
using EventPlu.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlu.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context;
    }


    /// <summary>
    ///  Método para Atualizar eventos no qual um usuário confirmou a presença.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="evento"></param>
    public void Atualizar(Guid id, Evento evento)
    {
        var EventoBuscado = _context.Eventos.Find(id);

        if (EventoBuscado != null)
        {
            EventoBuscado.Presencas = evento.Presencas;
            EventoBuscado.Descricao = evento.Descricao;
            EventoBuscado.DataEvento = evento.DataEvento;
            EventoBuscado.Nome = evento.Nome;
            EventoBuscado.IdInstituicaoNavigation = evento.IdInstituicaoNavigation;
            EventoBuscado.IdTipoEventoNavigation = evento.IdTipoEventoNavigation;
            //O SaveChanges() detecta as mudanças na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }

    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var EventoBuscado = _context.Eventos.Find(id);

        if (EventoBuscado != null) // Verifica se o tipo de evento existe antes de tentar deletar
        {
            _context.Eventos.Remove(EventoBuscado);
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }
    }

    public List<Evento> Listar()
    {
        return _context.Eventos.Include(e => e.IdInstituicaoNavigation)
          .Include(e => e.IdInstituicaoNavigation).ToList();


    }


    /// <summary>
    /// Método para buscar eventos no qual um usuário confirmou a presença.
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public List<Evento> ListarPorId(Guid idUsuario)
    {
        return _context.Eventos.Include(e => e.IdInstituicaoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == idUsuario && p.Situacao == true)).ToList();
    }


    /// <summary>
    /// Método que traz a lista de próximos eventos
    /// </summary>
    /// <returns>0Um lista de eventos</returns>
    public List<Evento> ProximosEventos()
    {
        return _context.Eventos.Include(e => e.IdInstituicaoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
