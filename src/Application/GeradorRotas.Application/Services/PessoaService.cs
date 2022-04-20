﻿using RouteManager.Application.Services.Interfaces;
using RouteManager.Domain.Entities;
using RouteManager.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManager.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasAsync() =>
            await _pessoaRepository.GetAllAsync();

        public async Task<Pessoa> GetPessoaByIdAsync(string id) =>
            await _pessoaRepository.GetByIdAsync(id);

        public async Task AddPessoaAsync(Pessoa pessoa)
        {
            await _pessoaRepository.AddAsync(pessoa);
        }

        public async Task UpdatePessoaAsync(Pessoa pessoa)
        {
            await _pessoaRepository.UpdateAsync(pessoa);
        }

        public async Task RemovePessoaAsync(string id)
        {
            await _pessoaRepository.DeleteAsync(await GetPessoaByIdAsync(id));
        }
    }
}