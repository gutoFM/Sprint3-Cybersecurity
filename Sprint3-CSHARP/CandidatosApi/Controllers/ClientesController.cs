using SprintBusiness;
using SprintModel;
using Microsoft.AspNetCore.Mvc;

namespace SprintApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var clientes = _clienteService.ListarTodos();
        return clientes.Count == 0 ? NoContent() : Ok(clientes);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var cliente = _clienteService.ObterPorId(id);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpGet("email/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var cliente = _clienteService.ObterPorEmail(email);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ClienteModel cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.nome) || string.IsNullOrWhiteSpace(cliente.email))
            return BadRequest("Nome e email são obrigatórios.");

        var criado = _clienteService.Criar(cliente);
        return CreatedAtAction(nameof(Get), new { id = criado.idCliente }, criado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ClienteModel cliente)
    {
        if (cliente == null)
            return BadRequest("Dados inconsistentes.");

        return _clienteService.Atualizar(cliente) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _clienteService.Remover(id) ? NoContent() : NotFound();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login)
    {
        if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
            return BadRequest("Email e senha são obrigatórios.");

        var isValid = _clienteService.ValidarLogin(login.Email, login.Senha);
        return isValid ? Ok(new { message = "Login bem-sucedido" }) : Unauthorized();
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
}