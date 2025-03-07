﻿using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao
{
    public interface IMedicoService
    {
        Task<ResponseModel> BuscarMedicoPorCRM(BuscarCRMDTO crmDTO);
        Task<ResponseModel> BuscarMedicosPorEspecialidade(EspecialidadeMedica especialidadeMedica);
        Task<ResponseModel> AceiteConsultaMedica(AceiteConsultaMedicoRequestModel aceiteConsultaMedica);
        Task<ResponseModel> SalvarNovoMedico(CriarAlteraMedicoDTO medicoDTO);
        Task<ResponseModel> AtualizarMedico(CriarAlteraMedicoDTO medicoDTO);
        Task<ResponseModel> ExcluirMedico(CriarAlteraMedicoDTO medicoDTO);
        Task<string> GerarHashSenhaUsuario(string senha);
    }
}
