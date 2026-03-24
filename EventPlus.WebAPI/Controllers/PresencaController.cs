using EventPlu.WebAPI.BdContextEvent;
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
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }


    /// <summary>
    /// Endpoint da API que faz chamada para o método de buscar uma presença pelo seu ID
    /// </summary>
    /// <param name="id">id da presença a ser buscado </param>
    /// <returns>Status code 200 e presença buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }


    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar de presença no qual um filtrada pela id do usuário
    /// </summary>
    /// <param name="idUsuario">id do usuário para filtragem</param>
    /// <returns>uma lista de presença filtrado pelo usuário</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novoPresenca  = new Presenca
            {
                Situacao = presenca.Situacao!,
                IdUsuario = presenca.IdUsuario!,
                IdEvento = presenca.IdEvento!
                
            };

            _presencaRepository.Inscrever(novoPresenca);
            return StatusCode(201, novoPresenca);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presenca)
    {
        try
        {
            var PresencaAtualizado = new Presenca
            {
                Situacao = presenca.Situacao!,
                IdUsuario = presenca.IdUsuario!,
                IdEvento = presenca.IdEvento!
                
            };

            _presencaRepository.Atualizar(id);
            return StatusCode(204, PresencaAtualizado);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _presencaRepository.Deletar(id);
                return NoContent();  // Retorna status code 204 para indicar que a operação foi bem-sucedida, mas não há conteúdo para retornar
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }
    }
