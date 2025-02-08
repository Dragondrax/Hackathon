using MedicalHealth.Fiap.Infraestrutura.Enum;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class PersistenciaMedicoDTO
    {
        [JsonProperty("Nome")]
        public string Nome { get; private set; }
        [JsonProperty("CPF")]
        public string CPF { get; private set; }
        [JsonProperty("CRM")]
        public string CRM { get; private set; }
        [JsonProperty("Email")]
        public string Email { get; private set; }
        [JsonProperty("Senha")]
        public string Senha { get; private set; }
        [JsonProperty("SnAtivo")]
        public bool SnAtivo { get; private set; }
        [JsonProperty("ValorConsulta")]
        public double ValorConsulta { get; private set; }
        [JsonProperty("EspecialidadeMedica")]
        public Especialidade EspecialidadeMedica { get; private set; }

        public PersistenciaMedicoDTO(string nome, string cpf, string crm, string email, string senha, bool snAtivo, double valorConsulta, Especialidade especialidadeMedica)
        {
            Nome = nome;
            CPF = cpf;
            CRM = crm;
            Email = email;
            Senha = senha;
            SnAtivo = snAtivo;
            ValorConsulta = valorConsulta;
            EspecialidadeMedica = especialidadeMedica;
        }
    }
}
