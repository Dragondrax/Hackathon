using MedicalHealth.Fiap.SharedKernel.MensagensErro;


namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Consulta : EntidadeBase
    {
        public double Valor { get; private set; }
        public bool? Aceite { get; private set; }
        public bool? Cancelada { get; private set; }
        public string? Justificativa { get; private set; }
        public AgendaMedico? AgendaMedico { get; private set; }

        public Consulta(double valor, bool? aceite = null)
        {
            Valor = valor;
            Aceite = aceite;
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
