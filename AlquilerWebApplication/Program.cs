using Service;
using UnitOfWork_Interface;
using UnitOfWork_SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUnitOfWork, UnitOfWorkSqlServer>();
builder.Services.AddTransient<IRolService, RolService>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClient();
//builder.Services.AddApiVersioning();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
