using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository
{
    public class PersistenciaUsuarioRepository : IPersistenciaUsuarioRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        public PersistenciaUsuarioRepository(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task PersistirDadosUsuario(Usuario usuario)
        {

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Db.Update(usuario);
                await _unitOfWork.BeginTransactionAsync();
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackTransactionAsync();
            }
        }
    }
}
