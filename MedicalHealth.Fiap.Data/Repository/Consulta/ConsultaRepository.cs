using MedicalHealth.Fiap.Data.Context;

namespace MedicalHealth.Fiap.Data.Repository.Consulta
{
    public class ConsultaRepository : Repository<Dominio.Entidades.Consulta>, IConsultaRepository
    {
        public ConsultaRepository(MedicalHealthContext db) : base(db)
        {
        }
    }
}
