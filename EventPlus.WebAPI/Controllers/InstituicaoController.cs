using EventPlu.WebAPI.DTO;
using EventPlu.WebAPI.Interfaces;
using EventPlu.WebAPI.Models;
using EventPlu.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IInstituicaoRepository _instituicaoRepository;

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar os tipos de Instituicao
    /// </summary>
    /// <returns>Status code 200 e a lista de Instituicao de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de busca os tipos de Instituicao
    /// </summary>
    /// <param name="id">Id do tipo de Instituicao buscado</param>
    /// <returns>Status code 200 e tipo de Instituicao buscado</returns>

    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }

    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de cadastrado os tipos de Instituicao
    /// </summary>
    /// <param name="instituicao">Tipo de Instituicao a ser cadastrado</param>
    /// <returns>Status code 201 e e tipo de Instituicao cadastrado</returns>

    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao) 
    {
        try
        {
            var novoInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Cadastrar(novoInstituicao); 
            return StatusCode(201, novoInstituicao);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de atualizar os tipos de Instituicao
    /// </summary>
    /// <param name="id">Tipo de Instituicao a ser atualizar</param>
    /// <param name="Instituicao">Status code 204 e e tipo de eventos atualizar</param>
    /// <returns></returns>

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizado = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Atualizar(id, instituicaoAtualizado);
            return StatusCode(204, instituicaoAtualizado);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de deletar o tipo de Instituicao
    /// </summary>
    /// <param name="id">Tipo de Instituicao a ser excluido</param>
    /// <returns>Status code 204</returns>

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return NoContent();  // Retorna status code 204 para indicar que a operação foi bem-sucedida, mas não há conteúdo para retornar
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
