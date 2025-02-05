using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedicalHealth.Fiap.Dominio
{
    public class EntidadeBase
    {
        [Key]
        [JsonProperty("Id")]
        public Guid Id { get; private set; }
        [JsonProperty("Excluido")]
        public bool Excluido { get; private set; }
        [JsonProperty("DataRegistro")]
        public DateTime DataRegistro { get; private set; }
        [JsonProperty("DataAtualizacaoRegistro")]
        public DateTime? DataAtualizacaoRegistro { get; private set; }
        [JsonProperty("DataExclusao")]
        public DateTime? DataExclusao { get; private set; }

        public EntidadeBase()
        {
            Id = Guid.NewGuid();
            DataRegistro = DateTime.Now;
        }

        public void Desativar()
        {
            Excluido = true;
            DataExclusao = DateTime.Now;
        }

        public void AtualizarDataAtualizacao()
        {
            DataAtualizacaoRegistro = DateTime.Now;
        }
    }
}