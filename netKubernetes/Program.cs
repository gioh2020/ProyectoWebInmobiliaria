using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using netKubernetes.Data;
using netKubernetes.Data.Inmuebles;
using netKubernetes.Data.Usuarios;
using netKubernetes.Middleware;
using netKubernetes.Models;
using netKubernetes.Profiles;
using netKubernetes.Token;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.LogTo(Console.WriteLine, new [] {DbLoggerCategory.Database.Command.Name }, LogLevel.Information).EnableSensitiveDataLogging();
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnetion"));
    });

builder.Services.AddScoped<IInmubleRepositorycs, InmuebleRepository>();

// Add services to the container.

builder.Services.AddControllers(opt => { 
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(cfg => {

    cfg.AddProfile(new InmuebleProfile());
    
    });

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var builderSecurity = builder.Services.AddIdentityCore<Usuario>();
var identityBuilder = new IdentityBuilder(builderSecurity.UserType, builder.Services);
identityBuilder.AddEntityFrameworkStores<AppDbContext>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IJwtGenerador, JwtGenerador>();
builder.Services.AddScoped<IUsuarioSesion, UsuarioSesion>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMysecretKey12345123451234512345ThisIsMysecretKey12345123451234512345"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false,
        };
    });

builder.Services.AddCors(o => o.AddPolicy("corsApp", builder => {
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
})       
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ManagerMiddleware>();
app.UseCors("corsApp");
app.UseAuthorization();


app.MapControllers();


using (var ambiente = app.Services.CreateScope())
{ 
    var services = ambiente.ServiceProvider;
    try {
        var userManager = services.GetRequiredService<UserManager<Usuario>>();  
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await LoadDataBase.InsertarData(context, userManager);

    }
    catch (Exception ex) {
        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(ex, "ocurrio un error en la migracion");
    }    

}

    app.Run();
