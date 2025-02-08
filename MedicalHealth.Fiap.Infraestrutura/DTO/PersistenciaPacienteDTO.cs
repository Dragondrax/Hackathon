using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class PersistenciaPacienteDTO
    {
        [JsonProperty("Nome")]
        public string Nome { get; private set; }
        [JsonProperty("CPF")]
        public string CPF { get; private set; }
        [JsonProperty("Email")]
        public string Email { get; private set; }
        [JsonProperty("Senha")]
        public string Senha { get; private set; }

        public PersistenciaPacienteDTO(string nome, string cpf, string email, string senha)
        {
            Nome = nome;
            CPF = cpf;
            Email = email;
            Senha = senha;
        }
    }    
}
