using MedicalHealth.Fiap.SharedKernel.MensagensErro;


namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Consulta : EntidadeBase
    {
        public double Valor { get; private set; }
        public bool? Aceite { get; private set; }
        public bool? Cancelada { get; private set; }
        public string? Justificativa { get; private set; }
        public Guid MedicoId { get; private set; }
        public Guid PacienteId { get; private set; }
        public Medico Medico { get; private set; }
        public Paciente Paciente { get; private set; }

        public Consulta(double valor, Guid medicoId, Guid pacienteId, bool? aceite = null)
        {
            Valor = valor;
            Aceite = aceite;
            MedicoId = medicoId;
            PacienteId = pacienteId;
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
