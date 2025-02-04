using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Consulta : EntidadeBase
    {
        public double Valor { get; private set; }
        public bool? Aceite { get; private set; }
        public AgendaMedico? AgendaMedico { get; private set; }

        public Consulta(double valor, bool? aceite)
        {
            Valor = valor;
            Aceite = aceite;
        }

        private Consulta()
        {
            
        }

        public string AtualizarValor(double valor)
        {
            if (Aceite == true)
                return MensagemMedico.ErroNaoPodeAtualizarValorDaConsultaAposAceite;

            Valor = valor;
            AtualizarDataAtualizacao();

            return MensagemGenerica.MENSAGEM_SUCESSO;
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
