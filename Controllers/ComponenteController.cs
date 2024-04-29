using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SNGeneratorAPI.Model;
using SNGeneratorAPI.Service;

namespace SNGeneratorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComponenteController
    {
        private readonly ComponenteService _service;

        public ComponenteController(ComponenteService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<Result<Componente>> RetornaComponentePorId(int id)
        {
            return await _service.GetComponente(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<Componente>>> RetornarComponentes()
        {
            return await _service.GetTodosComponentes();
        }

        [HttpGet("ReturnComponents/")]
        public async Task<ActionResult<List<Componente>>> RetornaComponentesSemOperacao()
        {
            return await _service.GetTodosComponentesSemOperacao();
        }

        [HttpPost]
        public async Task<Result<Componente>> AdicionaComponente([FromBody] Componente componente)
        {
            return await _service.PostComponente(componente);

        }

        [HttpPut("{id}")]
        public async Task<Result<Componente>> AlterarComponente(int id, [FromBody] Componente componente)
        {
           return await _service.PutComponente(id, componente);
        }

        [HttpDelete("{id}")]
        public async Task<Result<Componente>> DeletaComponente(int id)
        {
            return await _service.DeleteComponente(id);
        }
    }
}
