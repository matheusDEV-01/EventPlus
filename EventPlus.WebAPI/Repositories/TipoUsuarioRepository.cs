using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Interfaces;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    // Injeção de dependência: Recebe o contexto pelo construtor
    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// recebe o id do tipo de evento a ser atualizado e o objeto com as novas informações.
    /// </summary>
    /// <param name="id">id do tipo evento a ser atualizado</param>
    /// <param name="tipoUsuario">Novos dados do tipo evento</param>
    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioExistente = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioExistente != null)
        {
            tipoUsuarioExistente.Titulo = tipoUsuario.Titulo;

            //O SaveChanges() detecta as mudanças na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tipoUsuario"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public void Deletar(Guid id)
    {
        var tipoUsuarioExistente = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioExistente != null) // Verifica se o tipo de evento existe antes de tentar deletar
        {
            _context.TipoUsuarios.Remove(tipoUsuarioExistente);
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }
    }

    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.OrderBy(tipoUsuario => tipoUsuario.Titulo).ToList();
    }
}
