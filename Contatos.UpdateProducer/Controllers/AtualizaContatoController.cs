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
        /// Endpoint para verificar a disponibilidade do serviço.
        /// </summary>
        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok("Serviço online.");
        }

        /// <summary>
        /// Endpoint para atualizar os dados de um contato cadastrado.
        /// </summary>
        /// <param name="input">
        /// Forneça o ID do contato (buscado do banco de dados), e os dados que deseja atualizar. Campos deixados em branco não serão alterados.
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
                return Ok("Atualização de contato recepcionada com êxito.");
            }
            catch (Exception ex)
            {
                Log.Error($"PUT para atualização de contato falhou. Exception: {ex.GetType()}. Message: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }
    }
}
