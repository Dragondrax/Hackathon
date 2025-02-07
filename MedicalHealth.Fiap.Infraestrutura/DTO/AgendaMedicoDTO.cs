namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class AgendaMedicoDTO
    {
        public DateOnly Data { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFim { get; set; }
        public bool Disponivel { get; set; }
        public Guid MedicoId { get; set; }
        public double ValorConsulta { get; set; }
    }
}
