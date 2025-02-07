using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class Paciente : EntidadeBase
    {
        [JsonProperty("Nome")]
        public string Nome { get; private set; }
        [JsonProperty("CPF")]
        public string CPF { get; private set; }
        [JsonProperty("Email")]
        public string Email { get; private set; }

        public Paciente()
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
