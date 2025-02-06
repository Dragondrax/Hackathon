using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository
{
    public class AgendaMedicoPersistenciaRepository : IAgendaMedicoPersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        private readonly ICacheService _cacheService;
        public AgendaMedicoPersistenciaRepository(IUnitOfwork unitOfWork,
                                                  MedicalHealthContext context,
                                                  ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<bool> PersistirAtualizacaoAgendaMedico(List<AgendaMedico> agendasMedico)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.AgendaMedico.UpdateRange(agendasMedico);
                await _unitOfWork.CommitTransactionAsync();

                foreach(var agendaMedico in agendasMedico)
                {
                    if (!agendaMedico.Excluido)
                        await _cacheService.SetAsync($"agenda:{agendaMedico.Id}", agendaMedico);
                    else
                        await _cacheService.RemoveAsync($"agenda:{agendaMedico.Id}");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PersistirCriacaoAgendaMedico(List<AgendaMedico> agendasMedico)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.AgendaMedico.AddRange(agendasMedico);
                await _unitOfWork.CommitTransactionAsync();

                foreach (var agendaMedico in agendasMedico)
                {
                    await _cacheService.SetAsync($"agenda:{agendaMedico.Id}", agendaMedico);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
