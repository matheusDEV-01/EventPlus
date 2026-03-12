using EventPlu.WebAPI.BdContextEvent;
using EventPlu.WebAPI.Models;
using EventPlu.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository

{
    private readonly EventContext _context;

    // Injeção de dependência: Recebe o contexto pelo construtor
    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// recebe um id do tipo "Guid" e um objeto do tipo "Instituição".
    /// </summary>
    /// <param name="id">id do tipo evento a ser atualizado</param>
    /// <param name="instituicao">Novos dados do tipo evento</param>

    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);
        if (instituicaoBuscado != null)
        {
            instituicaoBuscado.NomeFantasia = instituicao.NomeFantasia;

            //O SaveChanges() detecta as mudanças na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    /// <summary>
    /// recebe um objeto do tipo "Instituição" e o adiciona ao banco de dados, salvando as alterações em seguida.
    /// </summary>
    /// <param name="instituicao"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    /// <summary>
    /// recebe um id do tipo "Guid" e remove a instituição correspondente do banco de dados, salvando as alterações em seguida.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Deletar(Guid id)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);

        if (instituicaoBuscado != null) // Verifica se o tipo de evento existe antes de tentar deletar
        {
            _context.Instituicaos.Remove(instituicaoBuscado);
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(instituicao => instituicao.NomeFantasia).ToList();
    }
}
