using MedicalHealth.Fiap.Dominio.Enum;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Medico : EntidadeBase
    {
        [JsonProperty("Nome")]
        public string Nome { get; private set; }
        [JsonProperty("CPF")]
        public string CPF { get; private set; }
        [JsonProperty("CRM")]
        public string CRM { get; private set; }
        [JsonProperty("Email")]
        public string Email { get; private set; }
        [JsonProperty("SnAtivo")]
        public bool SnAtivo { get; private set; }
        [JsonProperty("ValorConsulta")]
        public double ValorConsulta {  get; private set; }
        [JsonProperty("EspecialidadeMedica")]
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
            SnAtivo = true;
            EspecialidadeMedica = especialidadeMedica;
            ValorConsulta = valorConsulta;
        }

        public void AtualizarDados(string nome, string cpf, string crm, string email, double valorConsulta, int especialidadeMedica)
        {
            Nome = nome;
            CPF = cpf;
            CRM = crm;
            Email = email;
            ValorConsulta = valorConsulta;
            EspecialidadeMedica = (EspecialidadeMedica) especialidadeMedica;
            AtualizarDataAtualizacao();
        }

        public void Excluir()
        {
            Desativar();
        }
    }
}
