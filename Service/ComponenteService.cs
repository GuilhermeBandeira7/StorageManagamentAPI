using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SNGeneratorAPI.Data;
using SNGeneratorAPI.Model;

namespace SNGeneratorAPI.Service
{
    public class ComponenteService
    {
        public SNContext _context;

        public ComponenteService(SNContext context)
        {
            _context = context;
        }

        public async Task<Result<Componente>> GetComponente(int componenteId)
        {
            Componente? componente = await _context.Componente.Where(comp => comp.Id == componenteId).FirstOrDefaultAsync();
            if (componente != null)
            {
                return Result.Ok(componente);
            }
            return Result.Fail("Componente não encontrado.");
        }

        public async Task<List<Componente>> GetTodosComponentes()
        {
            try
            {
                List<Componente>? componentes = await _context.Componente.ToListAsync();
                if (componentes.Count <= 0)
                {
                    throw new Exception("any component found.");
                }
                return componentes;
            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new();
            }
        }

        public async Task<List<Componente>> GetTodosComponentesSemOperacao()
        {
            List<Componente> componentes = await _context.Componente.Where(comp => comp.Operacao == null).ToListAsync();
            if(componentes.Count > 0)
            {
                return componentes;
            }

            return new();
        }

        public async Task<Result<Componente>> PostComponente(Componente componente)
        {
            //checando se o banco de dados contém o componente que queremos adicionar.
            Componente? comp = await _context.Componente.Where
                (c => c.SerialNumber == componente.SerialNumber).FirstOrDefaultAsync();
            if (comp == null && componente != null) 
            {
                try 
                {
                    await _context.AddAsync(componente);
                    await _context.SaveChangesAsync();
                    return Result.Ok(componente);
                }
                catch(Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }                              
            }
            return Result.Fail("Não foi possível adicionar este componente.");
        }

        public async Task<Result> PutComponente(int componenteId, Componente componenteAtualizado)
        {
            if(componenteAtualizado.Id != componenteId)
            {
                return Result.Fail("ID recebido não corresponde com um ID existente no banco de dados.");
            }

            Componente? componente = await _context.Componente.AsNoTracking().Where(comp => comp.Id == componenteId).FirstOrDefaultAsync();
            if(componente != null)
            {
                try
                {
                    _context.Entry(componenteAtualizado).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
            return Result.Fail("Componente não encontrado.");
        }

        public async Task<Result<Componente>> DeleteComponente(int componenteId)
        {
            Componente? componente = await _context.Componente.Where(comp => comp.Id == componenteId).FirstOrDefaultAsync();
            if(componente != null)
            {
                try
                {
                    await _context.Componente.Where(comp => comp.Id == componenteId).ExecuteDeleteAsync();
                    await _context.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
            return Result.Fail("Componente não encontrado.");
        }       
    }
}
