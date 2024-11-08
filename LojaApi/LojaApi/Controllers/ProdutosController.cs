using LojaApi.Models;
using ProdutosApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosRepository _produtosRepository;

        public ProdutosController(ProdutosRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        [HttpGet("listar-produto")]
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _produtosRepository.ListarProdutosDB();

            return Ok(produtos);
        }



        [HttpPost("registrar-produto")]
        public async Task<IActionResult> RegistrarProduto([FromBody] Produtos produtos)
        {
            var produtoId = await _produtosRepository.RegistrarProdutoDB(produtos);

            return Ok(new { mensagem = "Produto registrado com sucesso", produtoId });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto([FromBody] Produtos produtos)
        {
            produtos = produtos;
            await _produtosRepository.AtualizarProdutoDB(produtos);

            return Ok(new { mensagem = "Livro atualizado" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            try
            {
                await _produtosRepository.ExcluirProdutoDB(id);

                return Ok(new { mensagem = "Produto excluído com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
