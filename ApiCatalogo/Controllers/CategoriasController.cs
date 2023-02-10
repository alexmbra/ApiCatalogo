using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public CategoriasController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet("saudacao/{nome}")]
    public ActionResult<string> GetFromService([FromServices] IMeuServico meuServico, string nome)
    {
        return meuServico.Saudacao(nome);
    }




    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Categorias()
    {
        try
        {            
            return  _uow.CategoriaRepository.Get().ToList();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }        
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> CategoriasWProdutos()
    {
        try
        {
            return _uow.CategoriaRepository.GetCategoriasProdutos()
                .ToList();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Categoria(int id)
    {
        try
        {
            var categoria =  _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound($"Categoria {id} não encontrada...");
            }

            return categoria;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }        
    }

    [HttpPost]
    public ActionResult NovaCategoria(Categoria categoria)
    {
        try
        {
            if (categoria is null)
            {
                return BadRequest("Dados inválidos");
            }

            _uow.CategoriaRepository.Add(categoria);
            _uow.Commit();

            return CreatedAtAction("Categoria", new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
       
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Update(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }

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
    public ActionResult Delete(int id)
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

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
   

}
