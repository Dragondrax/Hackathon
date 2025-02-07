using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using Newtonsoft.Json;


namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Consulta : EntidadeBase
    {
        [JsonProperty("Valor")]
        public double Valor { get; private set; }
        [JsonProperty("Aceite")]
        public bool? Aceite { get; private set; }
        [JsonProperty("Cancelada")]
        public bool? Cancelada { get; private set; }
        [JsonProperty("Justificativa")]
        public string? Justificativa { get; private set; }
        [JsonProperty("MedicoId")]
        public Guid MedicoId { get; private set; }
        [JsonProperty("PacienteId")]
        public Guid PacienteId { get; private set; }
        [JsonProperty("AgendaMedicoId")]
        public Guid AgendaMedicoId { get; private set; }
        public Medico Medico { get; private set; }
        public Paciente Paciente { get; private set; }
        public AgendaMedico AgendaMedico { get; private set; }

        public Consulta(double valor, Guid medicoId, Guid pacienteId, Guid agendaMedicoId, bool? aceite = null)
        {
            Valor = valor;
            Aceite = aceite;
            MedicoId = medicoId;
            PacienteId = pacienteId;
            AgendaMedicoId = agendaMedicoId;
        }

        public Consulta()
        {
            
        }
        public void InformarJustificativa(string justificativa)
        {
            Justificativa = justificativa;
            Cancelada = true;
        }
        public void Excluir()
        {
            Desativar();
        }

        public void Aceitar()
        {
            Aceite = true;
            AtualizarDataAtualizacao();
        }

        public void Recusar()
        {
            Aceite = false;
            AtualizarDataAtualizacao();
        }
    }
}
