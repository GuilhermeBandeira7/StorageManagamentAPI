using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SNGeneratorAPI.Data;
using SNGeneratorAPI.Model;
using System.Linq;

namespace SNGeneratorAPI.Service
{
    public class OperacaoService
    {
        public SNContext _context;

        public OperacaoService(SNContext context)
        {
            _context = context;
        }

        public async Task<Result<Operacao>> GetOperacao(int id)
        {
            Operacao? operacao = await _context.Operacao.Where(op => op.Id == id).Include(x => x.Componentes).FirstOrDefaultAsync();
            if (operacao != null)
            {
                return Result.Ok(operacao);
            }
            return Result.Fail("Operação não encontrada.");
        }

        public async Task<List<Operacao>> GetTodasOperacoes()
        {
            try
            {
                List<Operacao> operacoes = await _context.Operacao.ToListAsync();
                if (operacoes != null)
                {
                    return operacoes;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            return new();
        }

        public async Task<Result<Operacao>> PutOperacao(int id, Operacao operacaoAtualizada)
        {
            if (operacaoAtualizada.Id != id)
            {
                return Result.Fail("ID recebido não corresponde com um ID existente no banco de dados.");
            }

            Operacao? operacao = await _context.Operacao.AsNoTracking().Where(op => op.Id == id).FirstOrDefaultAsync();
            if (operacao != null)
            {
                try
                {
                    _context.Entry(operacaoAtualizada).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
            return Result.Fail("Operação não encontrada");
        }

        public async Task<Result<Operacao>> PostOperacao(Operacao operacao)
        {
            if (operacao != null)
            {
                try
                {
                    List<int> opCompIds = operacao.Componentes.Select(c => c.Id).ToList();
                    operacao.Componentes = await _context.Componente
                        .Where(comp => opCompIds.Contains(comp.Id))
                        .ToListAsync();
                    await _context.Operacao.AddAsync(operacao);
                    await _context.SaveChangesAsync();
                    return Result.Ok(operacao);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
            return Result.Fail("Operação não poder ser nula.");
        }


        public async Task<Result<Operacao>> DeleteOperacao(int id)
        {
            Operacao? operacao = await _context.Operacao.Where(op => op.Id == id).Include(op => op.Componentes).FirstOrDefaultAsync();

            if (operacao != null)
            {
                try
                {
                    if (operacao.Componentes.Count > 0)
                    {
                        while (operacao.Componentes.Count != 0)
                        {
                            int cont = 0;
                            if (operacao.Componentes.Remove(operacao.Componentes[cont]))
                            {
                                await _context.SaveChangesAsync();
                                await Console.Out.WriteLineAsync("Sucesso ao remover componente.");
                            }
                            else
                            {
                                throw new Exception("Falha ao tenter remover componente associado à operação.");
                            }
                            cont++;
                        }
                        await _context.Operacao.Where(op => op.Id == id).ExecuteDeleteAsync();
                        await _context.SaveChangesAsync();
                        return Result.Ok();
                    }
                    else
                    {
                        await _context.Operacao.Where(op => op.Id == id).ExecuteDeleteAsync();
                        await _context.SaveChangesAsync();
                        return Result.Ok();
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
            return Result.Fail("Operação não encontrada.");
        }

        public async Task<Result<Operacao>> DeletaComponenteDaOperacao(int id, int compId)
        {
            Operacao? operacao = await _context.Operacao.Where(op => op.Id == id)
                .Include(o => o.Componentes).FirstOrDefaultAsync();
            if (operacao != null)
            {
                Componente? componente = await _context.Componente.Where(comp => comp.Id == compId).FirstOrDefaultAsync();
                if (componente != null)
                {
                    if (operacao.Componentes.Contains(componente))
                    {
                        operacao.Componentes.Remove(componente);
                        await _context.SaveChangesAsync();
                        return Result.Ok();
                    }
                }
            }
            return Result.Fail("Não foi possível remover componente da operação selecionada.");
        }

        public async Task<Result<Operacao>> AdicionaComponentesNaOperacao(int id, List<Componente> componentes)
        {
            Operacao? operacao = await _context.Operacao.Where(op => op.Id == id)
               .Include(o => o.Componentes).FirstOrDefaultAsync();

            if (operacao != null)
            {
                try
                {
                    foreach (Componente comp in componentes)
                    {
                        operacao.Componentes.Add(comp);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                    return Result.Fail(ex.ToString());
                }
                return Result.Ok(operacao);
            }
            return Result.Fail("Operação não encotrada.");
        }

        public async Task<Result<Operacao>> FechaOperacao(int id)
        {
            Operacao? operacao = await _context.Operacao.Where(op => op.Id == id).FirstOrDefaultAsync();
            if (operacao != null)
            {
                try
                {
                    operacao.Status = 0;
                    await _context.SaveChangesAsync();
                    return Result.Ok(operacao);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                    return Result.Fail(ex.ToString());
                }
            }
            return Result.Fail("Operação não encontrada.");
        }
    }
}
