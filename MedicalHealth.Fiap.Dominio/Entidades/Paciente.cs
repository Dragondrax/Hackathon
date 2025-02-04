namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Paciente : EntidadeBase
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public List<AgendaMedico> AgendaMedico { get; private set; }

        private Paciente()
        {
            
        }
        public Paciente(string nome, string cpf, string email)
        {
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        public void Atualizar(string nome, string cpf, string email) 
        {
            Nome = nome;
            CPF = cpf;
            Email = email;
            AtualizarDataAtualizacao();
        }

        public void Excluir()
        {
            Desativar();
        }
    }
}
