using Microsoft.AspNetCore.Mvc;
using trilha_net_api_desafio.Context;
using trilha_net_api_desafio.Entities;
using trilha_net_api_desafio.Models;

namespace trilha_net_api_desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;
        
        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarPorId(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            
            if (tarefa == null)
                return NotFound();
            
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            
            if(tarefa == null)
                return NotFound();
            
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return Ok("Registro deletado com sucesso.");
        }

        [HttpGet("ObterTodos")]
        public IActionResult ListarTarefas()
        {
            var tarefa = _context.Tarefas.ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ListarPorTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            if (tarefa == null) 
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ListarPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(_x => _x.Data.Date == data.Date);
            if (tarefa == null) 
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ListarPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            if (tarefa == null)
                return NotFound();
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult CriarTarefas(Tarefa tarefa)
        {
            _context.Add(tarefa);
            _context.SaveChanges();
            return Ok("Tarefa Criada");            
        }        
    }
}