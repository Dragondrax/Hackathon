using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Infraestrutura;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao.Agenda
{
    public interface IAgendaMedicoService
    {
        Task<ResponseModel> SalvarNovaAgendaParaOMedico(NovaAgendaMedicoRequestModel novaAgendaMedico);
        Task<ResponseModel> ApagarAgendaMedico(RemoverAgendaMedicoRequestModel removerAgenda);
        Task<ResponseModel> AtualizarAgendaMedico(ListaAtualizacoesRequestModel atualizarAgendaMedico);
        Task<bool> AtualizarAgendaIndisponivel(Guid agendaMedicoId);
        Task<bool> AtualizarAgendaDisponivel(Guid agendaMedicoId);
        Task<ResponseModel> BuscarAgendaPorMedico(Guid medicoId);
        Task<AgendaMedico> ObterAgendaMedicoPorId(Guid id);
    }
}
