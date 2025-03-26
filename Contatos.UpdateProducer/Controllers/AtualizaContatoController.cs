using Contatos.DataContracts.Commands;
using Contatos.UpdateProducer.DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Contatos.UpdateProducer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtualizaContatoController : ControllerBase
    {
        private readonly IBus _bus;

        public AtualizaContatoController(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Endpoint para verificar a disponibilidade do servi�o.
        /// </summary>
        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok("Servi�o online.");
        }

        /// <summary>
        /// Endpoint para atualizar os dados de um contato cadastrado.
        /// </summary>
        /// <param name="input">
        /// Forne�a o ID do contato (buscado do banco de dados), e os dados que deseja atualizar. Campos deixados em branco n�o ser�o alterados.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> AtualizarContato([FromBody] AtualizacaoContato input)
        {
            try
            {
                await _bus.Send(new AtualizarContato
                {
                    CommandId = Guid.NewGuid(),
                    TimeStamp = DateTime.Now,
                    Id = input.IdContato,
                    NomeCompleto = input.NovoNomeCompleto,
                    DDD = input.NovoDDD ?? 0,
                    Telefone = input.NovoTelefone ?? 0,
                    Email = input.NovoEmail
                });
                return Ok("Atualiza��o de contato recepcionada com �xito.");
            }
            catch (Exception ex)
            {
                Log.Error($"PUT para atualiza��o de contato falhou. Exception: {ex.GetType()}. Message: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }
    }
}
