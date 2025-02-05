﻿using MedicalHealth.Fiap.Aplicacao.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao.Agenda
{
    public interface IAgendaMedicoService
    {
        Task<ResponseModel> SalvarNovaAgendaParaOMedico(NovaAgendaMedicoRequestModel novaAgendaMedico);
    }
}
