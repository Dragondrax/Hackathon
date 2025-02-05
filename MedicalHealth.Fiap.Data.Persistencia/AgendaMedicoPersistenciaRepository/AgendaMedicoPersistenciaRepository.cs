using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository
{
    public class AgendaMedicoPersistenciaRepository : IAgendaMedicoPersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        public AgendaMedicoPersistenciaRepository(IUnitOfwork unitOfWork,
                                                  MedicalHealthContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<bool> PersistirAtualizacaoAgendaMedico(List<AgendaMedico> agendaMedico)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.AgendaMedico.UpdateRange(agendaMedico);
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PersistirCriacaoAgendaMedico(List<AgendaMedico> agendaMedico)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.AgendaMedico.AddRange(agendaMedico);
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
