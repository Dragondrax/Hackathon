namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Medico : EntidadeBase
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string CRM { get; private set; }
        public string Email { get; private set; }
        public bool SnAtivo { get; private set; }
        public double ValorConsulta {  get; private set; }
        public Enum.EspecialidadeMedica EspecialidadeMedica { get; private set; }
        public List<AgendaMedico> AgendaMedico { get; set; }

        public Medico()
        {
            
        }
        public Medico(string nome, string cpf, string crm, string email, Enum.EspecialidadeMedica especialidadeMedica, double valorConsulta)
        {
            Nome = nome;
            CPF = cpf;
            CRM = crm;
            Email = email;
            EspecialidadeMedica = especialidadeMedica;
            ValorConsulta = valorConsulta;
        }

        public void AtualizarDados(string nome, string cpf, string crm, string email, double valorConsulta)
        {
            Nome = nome;
            CPF = cpf;
            CRM = crm;
            Email = email;
            ValorConsulta = valorConsulta;
            AtualizarDataAtualizacao();
        }

        public void Excluir()
        {
            Desativar();
        }
    }
}
