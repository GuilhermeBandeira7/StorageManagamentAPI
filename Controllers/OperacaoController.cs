using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SNGeneratorAPI.Model;
using SNGeneratorAPI.Service;

namespace SNGeneratorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacaoController : ControllerBase
    {
        private readonly OperacaoService _service;

        public OperacaoController(OperacaoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<Result<Operacao>> RetornaOperacaoPorId(int id)
        {
            return await _service.GetOperacao(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<Operacao>>> RetornarOperacoes()
        {
            return await _service.GetTodasOperacoes();
        }

        [HttpPost]
        public async Task<Result<Operacao>> AdicionaOperacao([FromBody] Operacao operacao)
        {
            return await _service.PostOperacao(operacao);
        }

        [HttpPut("{id}")]
        public async Task<Result<Operacao>> AlteraOperacao(int id, [FromBody] Operacao operacao)
        {
            return await _service.PutOperacao(id, operacao);
        }


        [HttpDelete("{id}")]
        public async Task<Result<Operacao>> DeletaOperacao(int id)
        {
            return await _service.DeleteOperacao(id);
        }

        [HttpDelete("RemoveComponentOp/{id}/{compId}")]
        public async Task<Result<Operacao>> DeletaComponetesDaOperacao(int id, int compId)
        {
            return await _service.DeletaComponenteDaOperacao(id, compId);
        }

        [HttpPost("AddComponentOp/{id}")]
        public async Task<Result<Operacao>> AdicionaComponentesNaOperacao(int id,[FromBody] List<Componente> compList)
        {
            return await _service.AdicionaComponentesNaOperacao(id, compList);
        }

        [HttpPut("AlterStats/{id}")]
        public async Task<Result<Operacao>> AlteraStatusDaOperacao(int id)
        {
            return await _service.FechaOperacao(id);
        }
    }
}
