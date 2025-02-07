using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.ConsultaPersistenciaRepository
{
    public class ConsultaPersistenciaRepository : IConsultaPersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        private readonly ICacheService _cacheService;
        public ConsultaPersistenciaRepository(IUnitOfwork unitOfWork,
                                                  MedicalHealthContext context,
                                                  ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<bool> PersistirCriacaoConsulta(Consulta consulta)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Consulta.Add(consulta);
                await _unitOfWork.CommitTransactionAsync();

                await _cacheService.SetAsync($"consulta:{consulta.Id}", consulta);

                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> PersistirAtualizacaoConsulta(Consulta consulta)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Update(consulta);
                await _unitOfWork.CommitTransactionAsync();

                await _cacheService.SetAsync($"consulta:{consulta.Id}", consulta);

                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }
    }
}
