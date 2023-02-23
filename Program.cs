using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixMinAPI.Data;
using SixMinAPI.DTOs;
using SixMinAPI.Models;
using SixMinAPI.ModelValidators;
using SixMinAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConbuilder = new SqlConnectionStringBuilder();
sqlConbuilder.ConnectionString = builder.Configuration.GetConnectionString("Sqlconnection");
// sqlConbuilder.UserID=builder.Configuration["UserId"];
// sqlConbuilder.Password= builder.Configuration["Password"];

builder.Services.AddDbContext<AppDataContext>(op =>
{
    op.UseSqlServer(sqlConbuilder.ConnectionString);
});

builder.Services.AddScoped<IValidator<CommandCreateDTO>, CommandsValidator>();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/v1/commands", async (ICommandRepository _commandRepo, IMapper mapper) =>
{
    var commands = await _commandRepo.GetAllCommands();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDTO>>(commands));
});

app.MapGet("/api/v1/command/{id}", async (ICommandRepository _commadnRepo, IMapper mapper, [FromRoute] int id) =>
{
    var command = await _commadnRepo.GetCommandById(id);
    if (command is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(mapper.Map<CommandReadDTO>(command));
});

app.MapGet("/api/v1/commandWithouRoute/{id}", async (ICommandRepository _commadnRepo, IMapper mapper, int id) =>
{
    var command = await _commadnRepo.GetCommandById(id);
    if (command is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(mapper.Map<CommandReadDTO>(command));
});

app.MapGet("/api/v1/commandQuery", async (ICommandRepository _commadnRepo, IMapper mapper, [FromQuery] int id) =>
{
    var command = await _commadnRepo.GetCommandById(id);
    if (command is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(mapper.Map<CommandReadDTO>(command));
});



// https://www.infoworld.com/article/3676077/use-model-validation-in-minimal-apis-in-aspnet-core-6.html
// For ModelState Validatio... please check this link
app.MapPost("/api/v1/commandPost", async (IValidator<CommandCreateDTO> validator, ICommandRepository repo, IMapper mapper, [FromBody] CommandCreateDTO model) =>
{

    var validateResult = await validator.ValidateAsync(model);
    if (!validateResult.IsValid)
    {
        return Results.ValidationProblem(validateResult.ToDictionary());
    }
    var commandModel = mapper.Map<Command>(model);

    try
    {
        await repo.CreateCommand(commandModel);
        await repo.SaveChanges();

        var createdCommand = mapper.Map<CommandReadDTO>(commandModel);

        return Results.Ok(createdCommand);

        // return Results.Created($"api/v1/commands/{createdCommand.Id}",createdCommand);
    }
    catch (System.Exception e)
    {
        return Results.BadRequest(e.Message);

    }
});


app.MapPut("/api/v1/update/{id}", async (ICommandRepository repo, IMapper mapper, int id, CommandUpdateDTO model) =>
{

    var command = await repo.GetCommandById(id);

    if (command is null) return Results.NotFound();

    mapper.Map(model, command);
    await repo.SaveChanges();

    return Results.NoContent();

});

app.MapDelete("/api/command/delete-command/{id}", async (ICommandRepository repo, IMapper mapper, int id) =>
{
    var command = await repo.GetCommandById(id);

    if (command is null) return Results.NotFound();

    repo.DeleteCommand(command);

    await repo.SaveChanges();

    return Results.NoContent();
});


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
