using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class AgendaMedico : EntidadeBase
    {
        [JsonProperty("Data")]
        public DateTime Data { get; private set; }
        [JsonProperty("HorarioInicio")]
        public TimeOnly HorarioInicio { get; private set; }
        [JsonProperty("HorarioFim")]
        public TimeOnly HorarioFim { get; private set; }

        [NotMappedAttribute]
        public int Intervalo { get; private set; }
        [JsonProperty("Disponivel")]
        public bool Disponivel { get; private set; }
        public Medico Medico { get; private set; }
        public Paciente Paciente { get; private set; }
        [JsonProperty("MedicoId")]
        public Guid MedicoId { get; private set; }
        [JsonProperty("PacienteId")]
        public Guid? PacienteId { get; private set; }
        public Consulta Consulta { get; private set; }
        [JsonProperty("ConsultaId")]
        public Guid? ConsultaId { get; private set; }

        public AgendaMedico()
        {
            
        }
        public AgendaMedico(DateTime data, TimeOnly horarioInicio, TimeOnly horarioFim, Guid medicoId)
        {
            Data = data;
            HorarioInicio = horarioInicio;
            HorarioFim = horarioFim;
            Disponivel = true;
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

        public string AtualizarAgendaMedico(DateTime data, TimeOnly horarioInicio, TimeOnly horarioFim, bool disponivel)
        {
            if (PacienteId != null && Disponivel) return MensagemAgenda.MENSAGEM_HORARIO_COM_PACIENTE;

            Data = data;
            HorarioInicio = horarioInicio;
            HorarioFim = horarioFim;
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
