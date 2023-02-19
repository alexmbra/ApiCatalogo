using ApiCatalogo.Context;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        _uow = new UnitOfWork(context);
    }

    //=======================================================================
    // testes unitários
    // Inicio dos testes : método GET


}
