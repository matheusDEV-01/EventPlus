using EventPlu.WebAPI.Models;

namespace EventPlu.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    Usuario BuscarPorId(Guid id);

    Usuario BuscarPorEmailESenha(string Email, string Senha);
}
