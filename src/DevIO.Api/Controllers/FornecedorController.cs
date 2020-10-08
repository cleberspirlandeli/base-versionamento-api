using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevIO.Api.DTO;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorController(
            IMapper mapper,
            IFornecedorRepository fornecedorRepository)
        {
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
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
            var fornecedor = ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDto>> Adicionar(Guid id)
        { 
            return Ok();
        }

        private async Task<FornecedorDto> ObterFornecedorProdutosEndereco(Guid id)
        {
            var result = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);
            var fornecedor = _mapper.Map<FornecedorDto>(result);
            return fornecedor;
        }
    }
}