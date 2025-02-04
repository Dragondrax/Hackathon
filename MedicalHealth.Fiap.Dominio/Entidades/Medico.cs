namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Medico : EntidadeBase
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string CRM { get; private set; }
        public string Email { get; private set; }

        public Medico(string nome, string cpf, string crm, string email)
        {
            Nome = nome;
            CPF = cpf;
            CRM = crm;
            Email = email;
        }

        public void AtualizarDados(string nome, string cpf, string crm, string email)
        {
            Nome = nome;
            CPF = cpf;
            CRM = crm;
            Email = email;
            AtualizarDataAtualizacao();
        }

        public void Excluir()
        {
            Desativar();
        }
    }
}
