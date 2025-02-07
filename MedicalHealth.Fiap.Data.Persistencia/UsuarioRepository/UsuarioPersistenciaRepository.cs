using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository
{
    public class UsuarioPersistenciaRepository : IUsuarioPersistenciaRepository
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly MedicalHealthContext _context;
        private readonly ICacheService _cacheService;
        public UsuarioPersistenciaRepository(IUnitOfwork unitOfWork, MedicalHealthContext context, ICacheService cacheService) 
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<bool> PersistirCriacaoUsuario(Usuario usuario)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _context.Usuario.Add(usuario);
                await _unitOfWork.CommitTransactionAsync();

                await _cacheService.SetAsync($"usuario:{usuario.Id}", usuario);

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

                await _cacheService.SetAsync($"usuario:{usuario.Id}", usuario);

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
