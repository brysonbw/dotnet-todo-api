using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NSwag;
using SimpleTodoApi.DatabaseContext;
using SimpleTodoApi.Repository;
using SimpleTodoApi.RepositoryContracts;
using SimpleTodoApi.ServiceContracts;
using SimpleTodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Accept content-type 'application/json' for req/res
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Enable versioning in api controllers
var apiVersioningBuilder = builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader(); // Reads version number from request url at "apiVersion" constraint
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "1.0",
            Title = "Todo API",
        };
    };
});

// Configure db connection string
var conStrBuilder = new SqlConnectionStringBuilder(
       builder.Configuration.GetConnectionString("Default"));
conStrBuilder.Password = builder.Configuration["DbPassword"];
conStrBuilder["Server"] = builder.Configuration["DbHost"];
conStrBuilder["Database"] = builder.Configuration["Database"];
conStrBuilder["User"] = builder.Configuration["DbUser"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(conStrBuilder.ConnectionString);
});

apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // v1
    options.SubstituteApiVersionInUrl = true;
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                builder => builder
                    .WithMethods("GET", "POST", "PUT", "DELETE"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.MapOpenApi();
    // Add OpenAPI 3.0 document serving middleware
    // Available at: https://localhost:<port>/swagger/v1/swagger.json
    app.UseOpenApi();

    // Add web UIs to interact with the document
    // Available at: https://localhost:<port>/swagger
    app.UseSwaggerUi();

    // Add ReDoc UI to interact with the document
    // Available at: https://localhost:<port>/redoc
    app.UseReDoc(options =>
    {
        options.Path = "/redoc";
    });
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
