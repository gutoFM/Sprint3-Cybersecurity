using SprintBusiness;
using SprintModel;
using Microsoft.AspNetCore.Mvc;

namespace SprintApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DicasInvestimentoController : ControllerBase
{
    private readonly IDicaInvestimentoService _dicaService;

    public DicasInvestimentoController(IDicaInvestimentoService dicaService)
    {
        _dicaService = dicaService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var dicas = _dicaService.ListarTodos();
        return dicas.Count == 0 ? NoContent() : Ok(dicas);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var dica = _dicaService.ObterPorId(id);
        return dica == null ? NotFound() : Ok(dica);
    }

    [HttpGet("categoria/{categoria}")]
    public IActionResult GetByCategoria(string categoria)
    {
        var dicas = _dicaService.ObterPorCategoria(categoria);
        return dicas.Count == 0 ? NoContent() : Ok(dicas);
    }

    [HttpPost]
    public IActionResult Post([FromBody] DicaInvestimentoModel dica)
    {
        if (string.IsNullOrWhiteSpace(dica.titulo) || string.IsNullOrWhiteSpace(dica.descricao))
            return BadRequest("Título e descrição são obrigatórios.");

        var criado = _dicaService.Criar(dica);
        return CreatedAtAction(nameof(Get), new { id = criado.idDica }, criado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] DicaInvestimentoModel dica)
    {
        if (dica == null)
            return BadRequest("Dados inconsistentes.");

        return _dicaService.Atualizar(dica) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _dicaService.Remover(id) ? NoContent() : NotFound();
    }
}
