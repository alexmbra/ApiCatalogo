using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace ApiCatalogo.Controllers.v1;

[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
//[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
//[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
//[EnableCors("PermitirApiRequest")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorPreco()
    {
        var produtos = await _uow.ProdutoRepository.GetProdutosPorPreco();
        if (produtos is null)
        {
            return NotFound();
        }

        var produtoDTOs = _mapper.Map<List<ProdutoDTO>>(produtos);
        return Ok(produtoDTOs);
    }

    /// <summary>
    /// Exibe uma relação de produtos
    /// </summary>
    /// <param name="produtosParameters">Paramentros para PageNumber e pageSize</param>
    /// <returns>Retorna uma lista de objetos Produtos paginada</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Produtos([FromQuery] ProdutosParameters produtosParameters)
    {
        try
        {
            var produtos = await _uow.ProdutoRepository.GetProdutos(produtosParameters);
            if (produtos is null)
            {
                return NotFound();
            }

            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var produtoDTOs = _mapper.Map<List<ProdutoDTO>>(produtos);

            return Ok(produtoDTOs);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet("primeiro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    //[HttpGet("/primeiro")]
    //[HttpGet("{valor:alpha:length(5)}")]
    public async Task<ActionResult<ProdutoDTO>> GetPrimeiro()
    {
        try
        {
            var produto = await _uow.ProdutoRepository.Get().FirstOrDefaultAsync();
            if (produto is null)
            {
                return NotFound();
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    /// <summary>
    /// Obtem um produto pelo seu Id
    /// </summary>
    /// <param name="id">Código do produto</param>
    /// <returns>Retornar um objeto produto</returns>
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ProdutoDTO>> Produto(int id)
    {
        try
        {
            var produto = await _uow.ProdutoRepository.GetByIdAsync(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produto {id} não encontrado!");
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDTO)
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
            await _uow.Commit();

            return CreatedAtAction("Produto", new { id = produto.ProdutoId }, produtoDTO);
            //return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, ProdutoDTO produtoDTO)
    {
        try
        {
            if (id != produtoDTO.ProdutoId)
            {
                return BadRequest("Dados inválidos");
            }

            var produto = _mapper.Map<Produto>(produtoDTO);

            _uow.ProdutoRepository.Update(produto);
            await _uow.Commit();

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        try
        {
            var produto = await _uow.ProdutoRepository.GetByIdAsync(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produto {id} não encontrado!");
            }

            _uow.ProdutoRepository.Delete(produto);
            await _uow.Commit();

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
}
