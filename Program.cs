using System.Text;
using System.Text.Json.Serialization;
using ApiPerfil;
using ApiPerfil.Data;
using ApiPerfil.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
ConfiguracaoMvc(builder);
ConfigureAuthentication(builder);

var app = builder.Build();
LoadConfiguration(app);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment()) //Se estiver em ambiente de desenvolvimento
{
    app.UseSwagger(); //Habilita o Swagger
    app.UseSwaggerUI(); //Habilita a interface do Swagger
}

app.Run();

void LoadConfiguration(WebApplication app)
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKeyName = app.Configuration.GetValue<string>("ApiKeyName");
    Configuration.ApiKey = app.Configuration.GetValue<string>("ApiKey");

    var smtp = new Configuration.SmtpConfiguration();
    app.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;
}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); //Pegamos a nossa chave
builder.Services.AddAuthentication(x => //Adiciona a autenticação na API
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x=> // Como que será feita a desencriptação
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, //Validar a chave de assinatura(sim)
        IssuerSigningKey = new SymmetricSecurityKey(key), //Como ele valida essa chave, por meio de uma nova chave(key, que criamos)
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
}

void ConfiguracaoMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    
    builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;}) // Desabilita a validação automática do ASP.NET, permitindo que você trate os erros de validação manualmente.
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignora ciclos de referência, ou seja, evita referências circulares durante a serialização
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // Ignora propriedades com valores padrão durante a serialização, ou seja, não inclui essas propriedades no JSON gerado, como por exemplo um valor nulo
    });
}

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApiPerfilDataContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDbContext<ApiPerfilDataContext>();
    builder.Services.AddTransient<TokenService>();
    builder.Services.AddTransient<EmailService>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
{
    // 1. Define o esquema de segurança que a API usa (Bearer Token)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer SEU_TOKEN"
    });

    // 2. Adiciona o requisito de segurança aos endpoints que são protegidos
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
}

