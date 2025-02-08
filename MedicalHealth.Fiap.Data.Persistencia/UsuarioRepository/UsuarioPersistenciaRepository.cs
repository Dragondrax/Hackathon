using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository
{
    public class UsuarioPersistenciaRepository : IUsuarioPersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        public UsuarioPersistenciaRepository(IUnitOfwork unitOfWork, MedicalHealthContext context) 
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<bool> PersistirCriacaoUsuario(Usuario usuario)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
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

        public async Task<bool> PersistirAtualizacaoUsuario(Usuario usuario)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Update(usuario);
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
