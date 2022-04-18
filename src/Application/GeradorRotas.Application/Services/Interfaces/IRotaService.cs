﻿using GeradorRotas.Application.ViewModels;
using GeradorRotas.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GeradorRotas.Application.Services.Interfaces
{
    public interface IRotaService
    {
        Task AddRotaAsync(Rota rota);
        Task<Rota> GetRotaByIdAsync(string id);
        Task<IEnumerable<Rota>> GetRotasAsync();
        Task RemoveRotaAsync(string id);
        Task UpdateRotaAsync(Rota rota);
        Task<ReportRotaViewModel> RotaUpload(IFormFile file);
        Task<byte[]> ExportToDocx(ReportRotaViewModel reportRota);
    }
}