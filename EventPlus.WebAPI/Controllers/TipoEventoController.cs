using EventPlu.WebAPI.Models;
using EventPlu.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventPlus.WebAPI.DTO;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventoRepository;

    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar os tipos de eventos
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de eventos</returns>

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de busca os tipos de eventos
    /// </summary>
    /// <param name="id">Id do tipo de evento buscado</param>
    /// <returns>Status code 200 e tipo de eventos buscado</returns>

    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id) 
    {
        try
        {
            return Ok(_tipoEventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de cadastrado os tipos de eventos
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    /// <returns>Status code 201 e e tipo de eventos cadastrado</returns>

    [HttpPost]
    public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };

            _tipoEventoRepository.Cadastrar(novoTipoEvento);
            return StatusCode(201, novoTipoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// Endpoint da API que faz chamada para o método de atualizar os tipos de eventos
    /// </summary>
    /// <param name="id">Tipo de evento a ser atualizar</param>
    /// <param name="tipoEvento">Status code 204 e e tipo de eventos atualizar</param>
    /// <returns></returns>

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoEvento tipoEvento)
    {
        try
        {
            _tipoEventoRepository.Atualizar(id, tipoEvento);
            return StatusCode(204, tipoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de deletar o tipo de evento
    /// </summary>
    /// <param name="id">Tipo de evento a ser excluido</param>
    /// <returns>Status code 204</returns>

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoEventoRepository.Deletar(id);
            return NoContent();  // Retorna status code 204 para indicar que a operação foi bem-sucedida, mas não há conteúdo para retornar
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
