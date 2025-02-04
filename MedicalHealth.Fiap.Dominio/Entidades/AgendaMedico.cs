using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class AgendaMedico : EntidadeBase
    {
        public DateTime Data { get; private set; }
        public TimeOnly HorarioInicio { get; private set; }
        public TimeOnly HorarioFim { get; private set; }

        [NotMappedAttribute]
        public int Intervalo { get; private set; }

        public bool Disponivel { get; private set; }
        public Medico Medico { get; private set; }
        public Paciente Paciente { get; private set; }
        public Guid MedicoId { get; private set; }
        public Guid? PacienteId { get; private set; }
        public Consulta Consulta { get; private set; }
        public Guid? ConsultaId { get; private set; }

        private AgendaMedico()
        {
            
        }
        public AgendaMedico(DateTime data, TimeOnly horarioInicio, TimeOnly horarioFim, int intervalo, bool disponivel, Guid medicoId)
        {
            Data = data;
            HorarioInicio = horarioInicio;
            HorarioFim = horarioFim;
            Intervalo = intervalo;
            Disponivel = disponivel;
            MedicoId = medicoId;
        }

        public void AtualizarHorarioIndisponivel(Guid pacienteId)
        {
            PacienteId = pacienteId;
            Disponivel = false;
            AtualizarDataAtualizacao();
        }

        public void AtualizarHorarioDisponivel()
        {
            PacienteId = null;
            Disponivel = true;
            AtualizarDataAtualizacao();
        }

        public string AtualizarAgendaMedico(DateTime data, TimeOnly horarioInicio, TimeOnly horarioFim, int intervalo, bool disponivel)
        {
            if (PacienteId != null) return MensagemAgenda.MENSAGEM_HORARIO_COM_PACIENTE;

            Data = data;
            HorarioInicio = horarioInicio;
            HorarioFim = horarioFim;
            Intervalo = intervalo;
            Disponivel = disponivel;

            AtualizarDataAtualizacao();

            return MensagemGenerica.MENSAGEM_SUCESSO;
        }

        public void Excluir()
        {
            Desativar();
        }
    }
}
