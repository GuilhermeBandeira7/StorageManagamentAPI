using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SNGeneratorAPI.Model;
using SNGeneratorAPI.Service;

namespace SNGeneratorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<Result<Usuario>> RetornaUsuarioPorId(int id)
        {
            return await _service.GetUsuario(id);
        }

        [HttpGet]
        public async Task<Result<List<Usuario>>> RetornaUsuarios()
        {
            return await _service.GetTodosUsuarios();
        }

        [HttpGet("UserPassword/{login}/{senha}")]
        public async Task<Result<Usuario>> RetornaUsuarioPorLoginSenha(string login, string senha)
        {
            return await _service.GetUsuarioPorLoginSenha(login, senha);
        }

        [HttpPost]
        public async Task<Result<Usuario>> AdicionaUsuario([FromBody] Usuario usuario)
        {
            return await _service.PostUsuario(usuario);
        }

        [HttpPut("{id}")]
        public async Task<Result<Usuario>> AlteraUsuario([FromBody] Usuario usuario, int usuarioId)
        {
            return await _service.PutUsuario(usuarioId, usuario);
        }

        [HttpDelete("{id}")]
        public async Task<Result<Usuario>> DeleteUsuario(int id)
        {
            return await _service.DeleteUsuario(id);
        }
    }
}
