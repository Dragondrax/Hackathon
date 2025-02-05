using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository
{
    public class PersistenciaUsuarioRepository : IPersistenciaUsuarioRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        public PersistenciaUsuarioRepository(IUnitOfwork unitOfWork, MedicalHealthContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task PersistirDadosUsuario(Usuario usuario)
        {

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Usuario.Update(usuario);
                await _unitOfWork.BeginTransactionAsync();
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackTransactionAsync();
            }
        }
    }
}
