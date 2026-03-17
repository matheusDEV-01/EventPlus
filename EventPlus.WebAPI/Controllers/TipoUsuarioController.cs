using EventPlu.WebAPI.Interfaces;
using EventPlu.WebAPI.Models;
using EventPlu.WebAPI.DTO;
using EventPlu.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private ITipoUsuarioRepository _tipoUsuarioRepository;

    public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar os tipos de Usuario
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de Usuario</returns>

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de busca os tipos de usuarios
    /// </summary>
    /// <param name="id">Id do tipo de usuario buscado</param>
    /// <returns>Status code 200 e tipo de usuario buscado</returns>

    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoUsuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }

    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de cadastrado os tipos de usuarios
    /// </summary>
    /// <param name="tipoUsuario">Tipo de usuarios a ser cadastrado</param>
    /// <returns>Status code 201 e e tipo de usuarios cadastrado</returns>

    [HttpPost]
    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario) 
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario 
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
            return StatusCode(201, novoTipoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de atualizar os tipos de usuarios
    /// </summary>
    /// <param name="id">Tipo de usuarios a ser atualizar</param>
    /// <param name="tipoUsuario">Status code 204 e e tipo de usuarios atualizar</param>
    /// <returns></returns>

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var tipoUsuarioAtualizado = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);
            return StatusCode(204, tipoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de deletar o tipo de usuario
    /// </summary>
    /// <param name="id">Tipo de usuario a ser excluido</param>
    /// <returns>Status code 204</returns>

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);
            return NoContent();  // Retorna status code 204 para indicar que a operação foi bem-sucedida, mas não há conteúdo para retornar
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}
