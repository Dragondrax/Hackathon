using MedicalHealth.Fiap.SharedKernel.Data;
using System.ComponentModel.DataAnnotations;

namespace MedicalHealth.Fiap.Dominio
{
    public class EntidadeBase : Entity
    {
        [Key]
        public Guid Id { get; private set; }
        public bool Excluido { get; private set; }
        public DateTime DataRegistro { get; private set; }
        public DateTime? DataAtualizacaoRegistro { get; private set; }
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