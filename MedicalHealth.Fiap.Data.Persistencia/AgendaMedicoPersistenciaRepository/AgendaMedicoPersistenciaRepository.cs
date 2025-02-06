using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using Microsoft.EntityFrameworkCore;

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
                await _unitOfWork.RollbackTransactionAsync();
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
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> PersistirCriacaoConsulta(Consulta consulta)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Consulta.Add(consulta);
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> PersistirAtualizacaoConsulta(ConsultaAtualizarDTO consultaDTO)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Consulta consulta = await _context.Consulta.FirstOrDefaultAsync(x => x.Id == consultaDTO.ConsultaId);

                if (consulta == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                consulta.InformarJustificativa(consultaDTO.Justificativa);

                _context.Update(consulta);
                await _context.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
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
