using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.MedicoPersistenciaRepository
{
    public class MedicoPersistenciaRepository : IMedicoPersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        public MedicoPersistenciaRepository(IUnitOfwork unitOfWork,
                                                  MedicalHealthContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<bool> PersistirCriacaoMedico(Medico medico, Usuario usuario)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Medico.Add(medico);

                _context.Usuario.Add(usuario);

                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> PersistirAtualizacaoMedico(Medico medico)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Update(medico);
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
