using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.PacientePersistenciaRepository
{
    public class PacientePersistenciaRepository : IPacientePersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        private readonly ICacheService _cacheService;
        public PacientePersistenciaRepository(IUnitOfwork unitOfWork,
                                                  MedicalHealthContext context,
                                                  ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<bool> PersistirCriacaoPaciente(Paciente paciente)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Paciente.Add(paciente);
                await _unitOfWork.CommitTransactionAsync();

                await _cacheService.SetAsync($"Paciente:{paciente.Id}", paciente);

                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> PersistirAtualizacaoPaciente(Paciente paciente)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Update(paciente);
                await _unitOfWork.CommitTransactionAsync();

                await _cacheService.SetAsync($"Paciente:{paciente.Id}", paciente);

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
