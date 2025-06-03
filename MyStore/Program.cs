using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;


var bld = WebApplication.CreateBuilder(args);

bld.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(bld.Configuration.GetConnectionString("DefaultConnection")));

bld.Services.AddFastEndpoints().SwaggerDocument();

var app = bld.Build();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();
