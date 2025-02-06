using MedicalHealth.Fiap.Aplicacao.DTO;
using MedicalHealth.Fiap.Infraestrutura;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao.Agenda
{
    public interface IAgendaMedicoService
    {
        Task<ResponseModel> SalvarNovaAgendaParaOMedico(NovaAgendaMedicoRequestModel novaAgendaMedico);
        Task<ResponseModel> ApagarAgendaMedico(RemoverAgendaMedicoRequestModel removerAgenda);
        Task<ResponseModel> AtualizarAgendaMedico(ListaAtualizacoesRequestModel atualizarAgendaMedico);
        Task<bool> AtualizarAgendaIndisponivel(Guid agendaMedicoId, Guid pacienteId, Guid consultaId);
        Task<ResponseModel> BuscarAgendaPorMedico(string medicoId);
        Task<ResponseModel> SalvarConsulta(ConsultaSalvarDTO consultaDTO, Guid pacienteId);
        Task<ResponseModel> AtualizarJustificativaConsulta(ConsultaAtualizarDTO consultaAtualizarDTO);
    }
}
