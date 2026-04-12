using AlquilerWebApplication.TokenServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service;
using System.Text;
using UnitOfWork_Interface;
using UnitOfWork_SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAllOrigins", opt => opt
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("X-Pagination"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Stayly"});
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWorkSqlServer>();
builder.Services.AddTransient<IRolService, RolService>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAlojamientoService, AlojamientoService>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IContratoService, ContratoService>();
builder.Services.AddTransient<IPagoService, PagoService>();
builder.Services.AddTransient<ITipo_DocumentoService, Tipo_DocumentoService>();
builder.Services.AddTransient<IBoletaService, BoletaService>();

var jwtSettings = builder.Configuration.GetSection("JWTSettings");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:validIssuer"],
        ValidAudience = builder.Configuration["JwtSettings:validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:securityKey"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"❌ Error de autenticación: {context.Exception.Message}");
            return Task.CompletedTask;
        }
    };
});



builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

app.UseCors("AllowAllOrigins");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
