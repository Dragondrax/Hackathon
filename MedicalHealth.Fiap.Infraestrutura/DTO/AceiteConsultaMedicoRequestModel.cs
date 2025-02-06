namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class AceiteConsultaMedicoRequestModel
    {
        public Guid ConsultaId { get; set; }
        public Guid MedicoId { get; set; }
        public bool Aceite {  get; set; }
    }
}
