using Azure.AI.ContentSafety;
using EventPlu.WebAPI.BdContextEvent;
using EventPlu.WebAPI.Interfaces;
using EventPlu.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var endpoint = "https://moderatorservice-marcos.cognitiveservices.azure.com/";
var apiKey = "";

var client = new ContentSafetyClient(new Uri
    (endpoint), new Azure.AzureKeyCredential
    (apiKey));

builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer //inserir a string de conexão do banco de dados aqui, ou usar o appsettings.json para armazenar a string de conexão e ler usando builder.Configuration.GetConnectionString("DefaultConnection")
(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Registrar os reposirories para injeção de dependência
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IPresencaRepository, PresencaRepository>();
builder.Services.AddScoped<IComentarioEventoRepository, ComentarioEventoRepository>();


//Adicione o serviço de autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";

})

.AddJwtBearer("JwtBearer", Options =>
 {
     Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
     {
         ValidateIssuer = true, //Valida quem está solicitando o token
         ValidateAudience = true, //valida quem está recebendo o token
         ValidateLifetime = true,//define se o token tem um tempo de expiração

         //Chave de acesso ao token
         IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

         //valida o tempo de expiração do token
         ClockSkew = TimeSpan.FromMinutes(7),

         //nome do issuer (de onde o token está vindo)
         ValidIssuer = "api_EventPlus",

         //nome da audience (para onde ele está indo)
         ValidAudience = "api_EventPlus"
     };
 });


//Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Eventos",
        Description = "Aplicação para gerenciamento de eventos",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Matheus Felix",
            Url = new Uri("https://www.linkedin.com/in/matheus")
        },
        License = new OpenApiLicense
        {
            Name = "Exemplo de licensa",
            Url = new Uri("https://opensource.org/licenses")
        }

        });

    //Usando a autenticação no swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT: "
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
          
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
