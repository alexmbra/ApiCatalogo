using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using ApiCatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CategoriasController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet("saudacao/{nome}")]
    public ActionResult<string> GetFromService([FromServices] IMeuServico meuServico, string nome)
    {
        return meuServico.Saudacao(nome);
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasWProdutos()
    {
        try
        {
            var categorias = _uow.CategoriaRepository.GetCategoriascProdutos().ToList();
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
    public ActionResult<IEnumerable<CategoriaDTO>> Categorias()
    {
        try
        {    
            var categorias = _uow.CategoriaRepository.Get().ToList();
            if (categorias is null)
            {
                return NotFound();
            }

            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }        
    }

    

    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Categoria(int id)
    {
        try
        {
            var categoria =  _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
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
    public ActionResult NovaCategoria(CategoriaDTO categoriaDTO)
    {
        try
        {
            if (categoriaDTO is null)
            {
                return BadRequest("Dados inválidos");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uow.CategoriaRepository.Add(categoria);
            _uow.Commit();

            return CreatedAtAction("Categoria", new { id = categoria.CategoriaId }, categoriaDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
       
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Update(int id, CategoriaDTO categoriaDTO)
    {
        try
        {
            if (id != categoriaDTO.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uow.CategoriaRepository.Update(categoria);
            _uow.Commit();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
        
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        try
        {
            var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound($"Categoria {id} não encontrada...");
            }

            _uow.CategoriaRepository.Delete(categoria);
            _uow.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(categoriaDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
   

}
