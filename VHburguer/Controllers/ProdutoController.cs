using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VHBurguer.Applications.Services;
using VHBurguer.DTOs.ProdutoDto;
using VHBurguer.Exceptions;

namespace VHBurguer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }


        //Autenticação do usuario 
        private int obterUsuarioIdLogado()
        {
            string? idtexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idtexto))
            {
                throw new DomainException("Usuario não autenticado");
            }

            return int.Parse (idtexto);
        }

        [HttpGet]
        public ActionResult<List<LerProdutoDto>> Listar()
        {
            List<LerProdutoDto> produtos = _service.Listar();

            // return StatusCode(200, produtos);
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerProdutoDto> ObterPorID(int id)
        {
            LerProdutoDto produto = _service.ObterPorID(id);

            if (produto == null)
            {
                // return StatusCode(404);
                return NotFound();
            }

            return Ok(produto);
        }

        // GET -> api/produto/5/imagem
        [HttpGet("{id}/imagem")]

        public ActionResult ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);

                return File(imagem, "image/jpeg");
            }
            catch (VHBurguer.Exceptions.DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        //indica que recebe dados no formato multipart/form-data
        // necessário quando enviamos arquivos (ex. imagem do produto)
        [Consumes ("multipart/form-data")]
        [Authorize] // exige login para adicionar produtos 

        public ActionResult Adicionar([FromForm]CriarProdutoDto produtoDto)
        {
            try
            {
                int usuarioId = obterUsuarioIdLogado();
                // o cadastro fica  associado ao usuário logado
                _service.Adicionar(produtoDto, usuarioId);
                return StatusCode(201); // Created 
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Consumes("multpart/form-data")]
        [Authorize]
        public ActionResult Atualizar(int id, [FromForm] AtualizarProdutoDto produtoDto)
        {
            try
            {
                _service.Atualizar(id, produtoDto);
                return NoContent();

            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();

            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
