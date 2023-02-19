using ApiCatalogo.Context;
using ApiCatalogo.Controllers.v1;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Repository;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ApiCatalogoXUnitTestes;

public class CategoriasxUnitTestController
{
    private readonly IUnitOfWork? _uow;
    private readonly IMapper? _mapper;

    public static DbContextOptions<AppDbContext> dbContextOptions { get; }

    public static string connectionString =
           "Server=localhost;DataBase=ApiCatalogoDB;Uid=root;Pwd=0192";

    static CategoriasxUnitTestController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public CategoriasxUnitTestController()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        _mapper = config.CreateMapper();

        var context = new AppDbContext(dbContextOptions);

        //DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
        //db.Seed(context);

        _uow = new UnitOfWork(context);
    }

    //=======================================================================
    // testes unitários
    // Inicio dos testes : método GET

    [Fact]
    public async Task GetCategorias_Return_OkResult()
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);

        //Act
        var data = await controller.Get();

        //Assert
        Assert.IsType<List<CategoriaDTO>>(data.Value);
    }

    //Get BadRequest

    [Fact]
    public async Task GetCategorias_Return_BadRequestResult()
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);

        //Act
        var data = await controller.Get();

        //Assert
        Assert.IsType<BadRequestResult>(data.Result);
    }

    //GET retornar lista de categorias
    [Theory]
    [InlineData("Bebidas", "bebidas.jpg", 0)]
    [InlineData("Sobremesas", "Sobremesas.jpg", 2)]
    public async Task GetCategorias_MatchResult(string a, string b, int indexDB)
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);

        //Act
        var data = await controller.Get();

        //Assert
        Assert.IsType<List<CategoriaDTO>>(data.Value);

        var cat = data.Value.Should().BeAssignableTo<List<CategoriaDTO>>().Subject;

        Assert.Equal(a, cat[indexDB].Nome);
        Assert.Equal(b, cat[indexDB].ImagemUrl);

        Assert.Equal(a, cat[indexDB].Nome);
        Assert.Equal(b, cat[indexDB].ImagemUrl);
    }





   


    //Get cat por Id - retorna categoriaDTO
    [Fact]
    public async Task GetCategoriaById_Return_OkResult()
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);
        var catid = 2;

        //Act
        var data = await controller.Categoria(catid);

        //Assert
        Assert.IsType<CategoriaDTO>(data.Value);
    }

    //Get cat por Id - retorna notfound
    [Fact]
    public async Task GetCategoriaById_Return_NotFound()
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);
        var catid = 9999;

        //Act
        var data = await controller.Categoria(catid);

        //Assert
        Assert.IsType<NotFoundResult>(data.Result);
    }

    [Fact]
    public async Task Post_Categoria_AddValidData_Return_CreatedResult()
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);

        var cat = new CategoriaDTO()
        {
            Nome = "Teste Unitario Inclusao",
            ImagemUrl = "testecatInclusao.jpg"
        };

        //Act
        var data = await controller.Post(cat);

        //Assert
        Assert.IsType<CreatedAtActionResult>(data);

    }

    [Fact]
    public async Task Put_Categoria_Update_ValidData_Return_OkResult()
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);
        var catid = 2;

        //Act
        var existingPost = await controller.Categoria(catid);
        var result = existingPost.Value.Should().BeAssignableTo<CategoriaDTO>().Subject;

        var catDto = new CategoriaDTO()
        {
            CategoriaId = catid,
            Nome = "Categoria Atualizada - Testes 1",
            ImagemUrl = result.ImagemUrl
        };

        var updateData = await controller.Update(catid, catDto);

        //Assert
        Assert.IsType<OkObjectResult>(updateData);


    }


    //Remover categoria
    [Fact]
    public async Task Delete_Categoria_Return_OkResult()
    {
        //Arrange
        var controller = new CategoriasController(_uow, _mapper);
        var catid = 9;

        //Act
        var data = await controller.Delete(catid);

        //Assert
        Assert.IsType<CategoriaDTO>(data.Value);


    }


}
