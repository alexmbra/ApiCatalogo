using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using ApiCatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    //private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoriasController(IUnitOfWork uow, IMapper mapper)//, IHttpContextAccessor httpContextAccessor)
    {
        _uow = uow;
        _mapper = mapper;
        //_httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("saudacao/{nome}")]
    public ActionResult<string> GetFromService([FromServices] IMeuServico meuServico, string nome)
    {
        return meuServico.Saudacao(nome);
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasWProdutos()
    {
        try
        {
            var categorias = await _uow.CategoriaRepository.GetCategoriascProdutos();
            if (categorias is null)
            {
                return NotFound();
            }

            var CategoriaDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

            return CategoriaDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Categorias([FromQuery] CategoriasParameters categoriasParameters)
    {
        try
        {
            var categorias = await _uow.CategoriaRepository.GetCategorias(categoriasParameters);
            if (categorias is null)
            {
                return NotFound();
            }

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }        
    }

    

    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> Categoria(int id)
    {
        try
        {
            var categoria =  await _uow.CategoriaRepository.GetByIdAsync(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound($"Categoria {id} não encontrada...");
            }

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }        
    }

    [HttpPost]
    public async Task<ActionResult> NovaCategoria(CategoriaDTO categoriaDTO)
    {
        try
        {
            if (categoriaDTO is null)
            {
                return BadRequest("Dados inválidos");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uow.CategoriaRepository.Add(categoria);
            await _uow.Commit();

            return CreatedAtAction("Categoria", new { id = categoria.CategoriaId }, categoriaDTO);

            //var jsonSerializerSettings = new JsonSerializerSettings
            //{
            //    ContractResolver = new IgnorePropertyForEndpointContractResolver("Categoria", _httpContextAccessor, "Produtos"),
            //    Formatting = Formatting.Indented
            //};

            //return new JsonResult(categoriaDTO, jsonSerializerSettings);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
       
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Update(int id, CategoriaDTO categoriaDTO)
    {
        try
        {
            if (id != categoriaDTO.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uow.CategoriaRepository.Update(categoria);
            await _uow.Commit();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
        
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        try
        {
            var categoria = await _uow.CategoriaRepository.GetByIdAsync(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound($"Categoria {id} não encontrada...");
            }

            _uow.CategoriaRepository.Delete(categoria);
            await _uow.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(categoriaDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
   

}
