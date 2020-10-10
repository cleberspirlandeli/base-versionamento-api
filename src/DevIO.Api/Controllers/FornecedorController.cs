using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevIO.Api.DTO;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class FornecedorController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(
            IMapper mapper,
            IFornecedorRepository fornecedorRepository,
            IFornecedorService fornecedorService
            )
        {
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDto>>> ObterTodos()
        {
            var result = await _fornecedorRepository.ObterTodos();
            var fornecedor = _mapper.Map<IEnumerable<FornecedorDto>>(result);

            return Ok(fornecedor);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<FornecedorDto>>> ObterPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDto>> Adicionar(FornecedorDto fornecedorDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
            var result = await _fornecedorService.Adicionar(fornecedor);

            if (!result) return BadRequest();

            return Ok(fornecedor);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> Alterar(Guid id, FornecedorDto fornecedorDto)
        {
            if (id != fornecedorDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
            var result = await _fornecedorService.Atualizar(fornecedor);

            if (!result) return BadRequest();

            return Ok(fornecedor);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> Deletar(Guid id)
        {

            if (!ModelState.IsValid) return BadRequest();

            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            var result = await _fornecedorService.Remover(id);

            if (!result) return BadRequest();

            return Ok(fornecedor);
        }

        private async Task<FornecedorDto> ObterFornecedorProdutosEndereco(Guid id)
        {
            var result = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);
            var fornecedor = _mapper.Map<FornecedorDto>(result);
            return fornecedor;
        }

        private async Task<FornecedorDto> ObterFornecedorEndereco(Guid id)
        {
            var result = await _fornecedorRepository.ObterFornecedorEndereco(id);
            var fornecedor = _mapper.Map<FornecedorDto>(result);
            return fornecedor;
        }
    }
}