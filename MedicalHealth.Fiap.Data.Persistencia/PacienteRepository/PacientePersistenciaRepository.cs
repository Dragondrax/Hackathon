using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.PacientePersistenciaRepository
{
    public class PacientePersistenciaRepository : IPacientePersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        public PacientePersistenciaRepository(IUnitOfwork unitOfWork,
                                                  MedicalHealthContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<bool> PersistirCriacaoPaciente(Paciente paciente, Usuario usuarioParciente)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Paciente.Add(paciente);

                _context.Usuario.Add(usuarioParciente);                

                await _unitOfWork.CommitTransactionAsync();

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
