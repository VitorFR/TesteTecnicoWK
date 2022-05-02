using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniValidation;
using System.Text.Json.Serialization;
using TesteTecnicoWK.Data.Entity.MySQL.Contexto;
using TesteTecnicoWK.Data.Entity.MySQL.Repositorio;
using TesteTecnicoWK.Dominio.Entidades;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s => {
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Teste Técnico WK",
        Description = "Desenvolvido por Vítor Ferreira Rodrigues",
        Contact = new OpenApiContact { Name = "Vítor F. Rodrigues", Email = "v.fr@outlook.com"},
        License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});

var connectionString = builder.Configuration.GetConnectionString("Conexao");
builder.Services.AddDbContext<MySqlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();
#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Categoria EndPoint

app.MapGet("/categorias", async (MySqlDbContext contexto) =>
    {
        CategoriaRepositorio rCategoria = new CategoriaRepositorio(contexto);
        return await rCategoria.Listar();
    })
    .WithName("GetCategorias")
    .WithTags("Categoria");

app.MapGet("/categoria/{id}", async (
    Guid id,
    MySqlDbContext contexto) =>
    { 
        CategoriaRepositorio rCategoria = new CategoriaRepositorio(contexto);
        return await rCategoria.Get(id)
            is Categoria categoria
                ? Results.Ok(categoria)
                : Results.NotFound("Categoria não encontrada");
    })
    .Produces<Categoria>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetCategoriaID")
    .WithTags("Categoria");

app.MapPost("/categoria/{categoria}", async (
    Categoria categoria,
    MySqlDbContext contexto) =>
    {
        if (!MiniValidator.TryValidate(categoria, out var errors))
            return Results.ValidationProblem(errors);

        CategoriaRepositorio rCategoria = new CategoriaRepositorio(contexto);
       return await rCategoria.Insert(categoria)
            is Categoria novaCategoria
                ? Results.CreatedAtRoute("GetCategoriaID", new { id = novaCategoria.Id }, novaCategoria)
                : Results.BadRequest("Erro ao inserir a nova categoria.");
    })
    .ProducesValidationProblem()
    .Produces<Categoria>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("InsertCategoria")
    .WithTags("Categoria");

app.MapPut("/categoria/{categoria}", async (
    Categoria categoria,
    MySqlDbContext contexto) =>
    {
        categoria.Dt_Alteracao = DateTime.Now;

        if (!MiniValidator.TryValidate(categoria, out var errors))
            return Results.ValidationProblem(errors);

        CategoriaRepositorio rCategoria = new CategoriaRepositorio(contexto);

        var categoriaExiste = await rCategoria.Get(categoria.Id);
        if (categoriaExiste == null) return Results.NotFound("Categoria inexistente");

        return await rCategoria.Edit(categoria)
            is Categoria editedCategoria
                ? Results.NoContent()
                : Results.BadRequest("Erro ao alterar a categoria.");
    })
    .ProducesValidationProblem()
    .Produces<Categoria>(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("EditCategoria")
    .WithTags("Categoria");

app.MapDelete("/categoria/{id}", async (
    Guid id,
    MySqlDbContext contexto) =>
    {
        CategoriaRepositorio rCategoria = new CategoriaRepositorio(contexto);

        var categoria = await rCategoria.Get(id);
        if (categoria == null) return Results.NotFound("Categoria inexistente");

        return await rCategoria.Delete(categoria)
            is Boolean retorno
                ? Results.NoContent()
                : Results.BadRequest("Erro ao excluir a categoria.");
    })
    .Produces<Categoria>(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("DeleteCategoria")
    .WithTags("Categoria");
#endregion

#region Produto EndPoint

app.MapGet("/produtos", async (MySqlDbContext contexto) =>
{
    ProdutoRepositorio rProduto = new ProdutoRepositorio(contexto);
    return await rProduto.Listar();
})
    .WithName("GetProdutos")
    .WithTags("Produto");

app.MapGet("/produto/{id}", async (
    Guid id,
    MySqlDbContext contexto) =>
{
    ProdutoRepositorio rProduto = new ProdutoRepositorio(contexto);
    return await rProduto.Get(id)
        is Produto produto
            ? Results.Ok(produto)
            : Results.NotFound("Produto não encontrado");
})
    .Produces<Produto>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetProdutoID")
    .WithTags("Produto");

app.MapPost("/produto/{produto}", async (
    Produto produto,
    MySqlDbContext contexto) =>
{
    if (!MiniValidator.TryValidate(produto, out var errors))
        return Results.ValidationProblem(errors);

    ProdutoRepositorio rProduto = new ProdutoRepositorio(contexto);
    return await rProduto.Insert(produto)
         is Produto novoProduto
             ? Results.CreatedAtRoute("GetProdutoID", new { id = novoProduto.Id }, novoProduto)
             : Results.BadRequest("Erro ao inserir o novo produto.");
})
    .ProducesValidationProblem()
    .Produces<Produto>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("InsertProduto")
    .WithTags("Produto");

app.MapPut("/produto/{produto}", async (
    Produto produto,
    MySqlDbContext contexto) =>
{
    produto.Dt_Alteracao = DateTime.Now;

    if (!MiniValidator.TryValidate(produto, out var errors))
        return Results.ValidationProblem(errors);

    ProdutoRepositorio rProduto = new ProdutoRepositorio(contexto);

    var produtoExiste = await rProduto.Get(produto.Id);
    if (produtoExiste == null) return Results.NotFound("Produto inexistente");

    return await rProduto.Edit(produto)
        is Produto editedProduto
            ? Results.NoContent()
            : Results.BadRequest("Erro ao alterar o produto.");
})
    .ProducesValidationProblem()
    .Produces<Produto>(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("EditProduto")
    .WithTags("Produto");

app.MapDelete("/produto/{id}", async (
    Guid id,
    MySqlDbContext contexto) =>
{
    ProdutoRepositorio rProduto = new ProdutoRepositorio(contexto);

    var produto = await rProduto.Get(id);
    if (produto == null) return Results.NotFound("Produto inexistente");

    return await rProduto.Delete(produto)
        is Boolean retorno
            ? Results.NoContent()
            : Results.BadRequest("Erro ao excluir o produto.");
})
    .Produces<Produto>(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("DeleteProduto")
    .WithTags("Produto");
#endregion

app.Run();