using AutoMapper;
using DevIO.Api.Controllers.Common;
using DevIO.Api.DTO;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ProdutoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;


        public ProdutoController(INotificador notificador,
                                 IMapper mapper,
                                 IProdutoRepository produtoRepository,
                                 IProdutoService produtoService,
                                 IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterProdutosFornecedores();
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
            return CustomResponse(produtosDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ObterPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoFornecedor(id);
            var produtoDto = _mapper.Map<ProdutoDto>(produto);

            if (produtoDto == null) return NotFound();

            return CustomResponse(produtoDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDto>> Adicionar(ProdutoDto produtoDto)
        { 
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var novoNomeImagem = Guid.NewGuid() + "_" + Regex.Replace(produtoDto.Imagem, @"\s+", "") ;

            if (!UploadArquivo(produtoDto.ImagemUpload, novoNomeImagem))
            {
                return CustomResponse();
            }

            produtoDto.Imagem = novoNomeImagem;

            var produto = _mapper.Map<Produto>(produtoDto);
            await _produtoRepository.Adicionar(produto);
            return CustomResponse(produtoDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoDto>> Deletar(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoFornecedor(id);

            if (produto == null) return NotFound();

            await _produtoService.Remover(id);

            return CustomResponse();
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {

            if (string.IsNullOrEmpty(arquivo) || string.IsNullOrEmpty(imgNome))
            {
                NotificarErro("Forneça uma imagem para este produto!");
                return false;
            }

            var typesArray = new[] {
                "jpg",
                "png",
                "pdf"
            };

            var extensao = imgNome.Split(".")[1];

            if (!Array.Exists(typesArray, type => type == extensao))
            {
                NotificarErro("Extensão de arquivo não permitido!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
    }
}
