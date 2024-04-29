using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SNGeneratorAPI.Data;
using SNGeneratorAPI.Model;

namespace SNGeneratorAPI.Service
{
    public class UsuarioService
    {
        SNContext _context;

        public UsuarioService(SNContext context)
        {
            _context = context;
        }

        public async Task<Result<Usuario>> GetUsuario([FromBody] long id)
        {
            Usuario? usuario = await _context.Usuario.Where(user => user.Id == id).FirstOrDefaultAsync();
            if (usuario != null)
            {
                return Result.Ok(usuario);
            }
            return Result.Fail("Usuário não encontrado.");
        }

        public async Task<Result<Usuario>> GetUsuarioPorLoginSenha(string username, string password)
        {
            Usuario? usuario = await _context.Usuario.Where(user => user.Login == username && user.Senha == password).FirstOrDefaultAsync();
            if (usuario != null)
            {
                return Result.Ok(usuario);
            }
            return Result.Fail("Nenhum usuário encontrado.");
        }

        public async Task<Result<List<Usuario>>> GetTodosUsuarios()
        {
            List<Usuario> usuarios = await _context.Usuario.ToListAsync();
            if (usuarios.Count > 0)
            {
                return Result.Ok(usuarios);
            }
            return Result.Fail("Nenhum usuário encontrado.");
        }

        public async Task<Result<Usuario>> PutUsuario(long id, Usuario usuarioAtualizado)
        {
            if(usuarioAtualizado.Id != id)
            {
                return Result.Fail("ID recebido não corresponde com um ID existente no banco de dados.");
            }

            Usuario? usuario = await _context.Usuario.AsNoTracking().Where(user => user.Id == usuarioAtualizado.Id).FirstOrDefaultAsync();
            if (usuario != null)
            {
                if (usuarioAtualizado != null)
                {
                    try
                    {
                        _context.Entry(usuarioAtualizado).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return Result.Ok();
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync(ex.Message);
                    }
                }
            }
            return Result.Fail("Usuário não pode ser nulo.");
        }

        public async Task<Result<Usuario>> PostUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                try
                {
                    await _context.Usuario.AddAsync(usuario);
                    await _context.SaveChangesAsync();
                    return Result.Ok(usuario);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }

            }
            return Result.Fail("Usuário não pode ser nulo.");
        }


        public async Task<Result<Usuario>> DeleteUsuario(long id)
        {
            Usuario? usuario = await _context.Usuario.Where(user => user.Id == id).FirstOrDefaultAsync();
            if (usuario != null)
            {
                try
                {
                    await _context.Usuario.Where(user => user.Id == id).ExecuteDeleteAsync();
                    await _context.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
            return Result.Fail("O Usuário não existe.");
        }
    }
}
