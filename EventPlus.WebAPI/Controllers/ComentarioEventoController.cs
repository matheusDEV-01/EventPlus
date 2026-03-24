using Azure;
using Azure.AI.ContentSafety;
using EventPlu.WebAPI.DTO;
using EventPlu.WebAPI.Interfaces;
using EventPlu.WebAPI.Models;
using EventPlu.WebAPI.Repositories;
using EventPlus.WebAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;

    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(IComentarioEventoRepository comentarioEventoRepository, ContentSafetyClient contentSafetyClient) // Injeção de dependência do repositório de comentários de eventos

    {
        _comentarioEventoRepository = comentarioEventoRepository;
        _contentSafetyClient = contentSafetyClient;
    }

    [HttpGet("Evento/{idEvento}")]
    public IActionResult ListarSomenteExibe(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(idEvento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("{idUsuario}/{idEvento}")]
    public IActionResult BuscarPorIdUsuario(Guid idUsuario, Guid idEvento) // Busca um comentário específico de um usuário para um evento específico
    {
        try
        {
            var comentario = _comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento); // Chama o método do repositório para buscar o comentário com base no ID do usuário e do evento

            if (comentario != null) // Verifica se o comentário foi encontrado
                return NotFound(); // Retorna 404 se o comentário não for encontrado

            return Ok(comentario); // Retorna 200 com o comentário encontrado
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet]
    public IActionResult Listar(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(idEvento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }



    /// <summary>
    /// Endpoint para criar um novo comentário de evento.
    /// </summary>
    /// <param name="comentarioevento">comentário ser moderado</param>
    /// <returns>Status Code 201 e o comentário criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioevento) // Endpoint para criar um novo comentário de evento
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioevento.Descricao))
            {
                return BadRequest("O texto a ser moderado nao estar vazio.");
            }

            //criar objeto de análise
            var request = new AnalyzeTextOptions(comentarioevento.Descricao);

            //chamar a API do AZURE Content Safety para analisar o texto
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            //Verificar se o texto tem alguma severidade maior que 0, ou seja, se tem algum tipo de conteúdo inadequado
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);

            var novoComentarioEvento = new ComentarioEvento
            {
                Descricao = comentarioevento.Descricao!,
                IdUsuario = comentarioevento.IdUsuario!,
                IdEvento = comentarioevento.IdEvento!,
                DataComentarioEvento = DateTime.Now,
                //Defini se o comentário deve ser exibido ou não com base na análise de conteúdo
                Exibe = !temConteudoImproprio
            };
            //Cadastra o comentário
            _comentarioEventoRepository.Cadastrar(novoComentarioEvento);
            return StatusCode(201, novoComentarioEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }


}
