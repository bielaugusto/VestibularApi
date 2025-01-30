using Microsoft.AspNetCore.Mvc;
using VestibularApi.Domain.Entities;
using VestibularApi.Infrastructure;

namespace VestibularApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscricaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InscricaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var inscricoes = _context.Inscricoes.ToList();
            return Ok(inscricoes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var inscricao = _context.Inscricoes.Find(id);

            if (inscricao == null)
                return NotFound();

            return Ok(inscricao);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Inscricao inscricao)
        {
            _context.Inscricoes.Add(inscricao);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id =  inscricao.Id }, inscricao);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Inscricao updateInscricao)
        {
            var inscricao = _context.Inscricoes.Find(id);
            if(inscricao == null)
                return NotFound();

            inscricao.Status = updateInscricao.Status;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var inscricao = _context.Inscricoes.Find(id);
            if (inscricao == null)
                return NotFound();

            _context.Inscricoes.Remove(inscricao);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
