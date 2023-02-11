using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet("menorpreco")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorPreco()
    {
        var produtos = _uow.ProdutoRepository.GetProutosPorPreco().ToList();
        if (produtos is null)
        {
            return NotFound();
        }

        var produtoDTOs = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtoDTOs;
    }


    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Produtos()
    {
        try
        {
            var produtos = _uow.ProdutoRepository.Get().ToList();
            if (produtos is null)
            {
                return NotFound();
            }

            var produtoDTOs = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtoDTOs;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet("primeiro")]
    //[HttpGet("/primeiro")]
    //[HttpGet("{valor:alpha:length(5)}")]
    public ActionResult<ProdutoDTO> GetPrimeiro()
    {
        try
        {
            var produto =  _uow.ProdutoRepository.Get().FirstOrDefault();
            if (produto is null)
            {
                return NotFound();
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return produtoDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    //[HttpGet("{id:int}/{nome}", Name="ObterProduto")]
    [HttpGet("{id:int:min(1)}")]
    public ActionResult<ProdutoDTO> Produto(int id)
    {
        try
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produto {id} não encontrado!");
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return produtoDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] ProdutoDTO produtoDTO)
    {
        try
        {
            if (produtoDTO is null)
            {
                return BadRequest("Dados inválidos");
            }

            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.DataCadastro = DateTime.Now;

            _uow.ProdutoRepository.Add(produto);
            _uow.Commit();

            return CreatedAtAction("Produto", new { id = produto.ProdutoId }, produtoDTO);
            //return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, ProdutoDTO produtoDTO)
    {
        try
        {
            if (id != produtoDTO.ProdutoId)
            {
                return BadRequest("Dados inválidos");
            }

            var produto = _mapper.Map<Produto>(produtoDTO);

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
    public ActionResult<ProdutoDTO> Delete(int id)
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

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
}
