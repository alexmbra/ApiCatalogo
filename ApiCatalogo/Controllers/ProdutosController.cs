using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public ProdutosController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet("menorpreco")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorPreco()
    {
        return _uow.ProdutoRepository.GetProutosPorPreco().ToList();
    }


    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Produtos()
    {
        try
        {
            var produtos = _uow.ProdutoRepository.Get().ToList();
            if (produtos is null)
            {
                return NotFound();
            }

            return produtos;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet("primeiro")]
    //[HttpGet("/primeiro")]
    //[HttpGet("{valor:alpha:length(5)}")]
    public ActionResult<Produto> GetPrimeiro()
    {
        try
        {
            var produto =  _uow.ProdutoRepository.Get().FirstOrDefault();
            if (produto is null)
            {
                return NotFound();
            }

            return produto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    //[HttpGet("{id:int}/{nome}", Name="ObterProduto")]
    [HttpGet("{id:int:min(1)}")]
    public ActionResult<Produto> Produto(int id)
    {
        try
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produto {id} não encontrado!");
            }

            return produto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody]Produto produto)
    {
        try
        {
            if (produto is null)
            {
                return BadRequest("Dados inválidos");
            }

            _uow.ProdutoRepository.Add(produto);
            _uow.Commit();

            return CreatedAtAction("Produto", new { id = produto.ProdutoId }, produto);
            //return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Produto produto)
    {
        try
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Dados inválidos");
            }

            _uow.ProdutoRepository.Update(produto);
            _uow.Commit();

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produto {id} não encontrado!");
            }

            _uow.ProdutoRepository.Delete(produto);
            _uow.Commit();

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
}
