using EventPlu.WebAPI.BdContextEvent;
using EventPlu.WebAPI.Interfaces;
using EventPlu.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlu.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    //Método construtor que aplica a injeção de dependência do contexto
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }



    /// <summary>
    /// Buscado o usuario no banco de dados pelo email e senha fornecidos.
    /// </summary>
    /// <param name="Email">Email do usuário a ser buscado</param>
    /// <param name="Senha">Senha para validar o usuário</param>
    /// <returns>Usuário buscado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        //Primeiro, buscamos o usuário pelo email fornecido
        var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);

        //Verificamos se o usuário foi encontrado 
        if (usuarioBuscado != null)
        {
            //Comparamod a hash da senha digitalizada com o que está armazenado no banco de dados
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if(confere)
            {
                return usuarioBuscado; // Retorna o usuário encontrado
            } 
        }
        return null!;

    }


    /// <summary>
    /// Busca um usuário no banco de dados pelo seu ID, incluindo os dados do seu tipo de usuário.
    /// </summary>
    /// <param name="id">id do usuário a ser buscado</param>
    /// <returns>Usuário Buscado e seu tipo usuário</returns>
    public Usuario BuscarPorId(Guid id)
    {
       return _context.Usuarios.Include(Usuario => Usuario.IdTipoUsuarioNavigation).FirstOrDefault(Usuario => Usuario.IdUsuario == id)!;
    }


    /// <summary>
    /// Cadastra um novo usuário no banco de dados. A senha é criptografada e i id.
    /// </summary>
    /// <param name="usuario">Usuário a ser Cadastrar</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha); // Criptografa a senha antes de salvar no banco

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();// Salva as alterações no banco de dados
    }

    public List<Usuario> Listar()
    {
        return _context.Usuarios.OrderBy(Usuario => Usuario.Nome).ToList();
    }
}