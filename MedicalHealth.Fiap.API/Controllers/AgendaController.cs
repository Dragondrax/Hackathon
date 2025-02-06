﻿using MedicalHealth.Fiap.Aplicacao.Agenda;
using MedicalHealth.Fiap.Aplicacao.DTO;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHealth.Fiap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaMedicoService _agendaService;
        public AgendaController(IAgendaMedicoService agendaService)
        {
            _agendaService = agendaService;
        }
        [HttpPost("Criar")]
        public async Task<IActionResult> SalvarNovaAgendaMedico([FromBody] NovaAgendaMedicoRequestModel novaAgenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _agendaService.SalvarNovaAgendaParaOMedico(novaAgenda);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpPut("Atualizar")]
        public async Task<IActionResult> AtualizarAgendaMedico([FromBody] ListaAtualizacoesRequestModel atualizarAgenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _agendaService.AtualizarAgendaMedico(atualizarAgenda);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpDelete("Remover")]
        public async Task<IActionResult> RemoverAgendaMedico([FromBody] RemoverAgendaMedicoRequestModel removerAgenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _agendaService.ApagarAgendaMedico(removerAgenda);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == true && resultado.Mensagem.Any() && resultado.Mensagem.Count() > 1)
                return Ok(resultado.Mensagem);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpGet("BuscarPorData")]
        public async Task<IActionResult> BuscarAgendaData([FromQuery] string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BadRequest("Data é obrigatório.");
            }



            return Ok();
        }

        [HttpGet("BuscarAgendaPorMedico")]
        public async Task<IActionResult> BuscarAgendaPorMedico([FromQuery] string medicoId)
        {
            var resultado = await _agendaService.BuscarAgendaPorMedico(medicoId);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == true && resultado.Mensagem.Any() && resultado.Mensagem.Count() > 1)
                return Ok(resultado.Mensagem);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpPost("CriarConsulta/{pacienteId}")]
        public async Task<IActionResult> CriarConsulta([FromBody] ConsultaSalvarDTO consultaDTO, [FromQuery] Guid pacienteId)
        {
           var resultado = await _agendaService.SalvarConsulta(consultaDTO, pacienteId);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == true && resultado.Mensagem.Any() && resultado.Mensagem.Count() > 1)
                return Ok(resultado.Mensagem);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }

        [HttpPut("AtualizarJustificativaConsulta")]
        public async Task<IActionResult> AtualizarJustificativaConsulta(ConsultaAtualizarDTO consultaAtualizarDTO)
        {
            var resultado = await _agendaService.AtualizarJustificativaConsulta(consultaAtualizarDTO);

            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Sucesso == true && resultado.Mensagem.Any() && resultado.Mensagem.Count() > 1)
                return Ok(resultado.Mensagem);
            else if (resultado.Sucesso == false && resultado.Objeto is null && resultado.Mensagem.Any())
                return BadRequest(resultado.Mensagem);
            else
                return StatusCode(500, MensagemGenerica.MENSAGEM_ERRO_500);
        }
    }
}
