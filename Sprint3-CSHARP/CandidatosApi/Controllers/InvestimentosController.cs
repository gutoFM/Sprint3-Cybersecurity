using SprintBusiness;
using SprintModel;
using Microsoft.AspNetCore.Mvc;

namespace SprintApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestimentosController : ControllerBase
{
    private readonly IInvestimentoService _investimentoService;

    public InvestimentosController(IInvestimentoService investimentoService)
    {
        _investimentoService = investimentoService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var investimentos = _investimentoService.ListarTodos();
        return investimentos.Count == 0 ? NoContent() : Ok(investimentos);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var investimento = _investimentoService.ObterPorId(id);
        return investimento == null ? NotFound() : Ok(investimento);
    }

    [HttpGet("tipo/{tipo}")]
    public IActionResult GetByTipo(string tipo)
    {
        var investimentos = _investimentoService.ObterPorTipo(tipo);
        return investimentos.Count == 0 ? NoContent() : Ok(investimentos);
    }

    [HttpPost]
    public IActionResult Post([FromBody] InvestimentoModel investimento)
    {
        if (string.IsNullOrWhiteSpace(investimento.nome) || string.IsNullOrWhiteSpace(investimento.tipo))
            return BadRequest("Nome e tipo são obrigatórios.");

        var criado = _investimentoService.Criar(investimento);
        return CreatedAtAction(nameof(Get), new { id = criado.idInvestimento }, criado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] InvestimentoModel investimento)
    {
        if (investimento == null)
            return BadRequest("Dados inconsistentes.");

        return _investimentoService.Atualizar(investimento) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _investimentoService.Remover(id) ? NoContent() : NotFound();
    }
}
