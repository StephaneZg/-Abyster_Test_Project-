using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Abyster_Test_Project.Config;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Services;
using Abyster_Test_Project.Domain.Users.Mappings;
using Abyster_Test_Project.Service.Contract;
using MediatR;
using Abyster_Test_Project.Domain.Accounts.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// var databaseSettings = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
// builder.Services.AddDbContext<DatabaseContext>(options =>
//                 options.UseNpgsql(databaseSettings.ToString())
//                 .LogTo(Console.WriteLine)
//                 .EnableSensitiveDataLogging()
//                 .EnableDetailedErrors()
//     );

// var sqliteDqtqbseSettings = builder.Configuration.GetConnectionString("AuthSqliteDB");
// builder.Services.AddDbContext<DatabaseContext>(options => 
//                     options.UseSqlite(sqliteDqtqbseSettings)
//                     .LogTo(Console.WriteLine)
//                     .EnableSensitiveDataLogging()
//                     .EnableDetailedErrors());
builder.Services.AddDbContext<DatabaseContext>();

var maperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new UserMapping());
    cfg.AddProfile(new AccountMapping());
});
var mapper = maperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
builder.Services.ConfigureJWT(jwtSettings);

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<DatabaseSeeder>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IClaimsService, ClaimsService>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Abyster test account managment API",
        Description = "API use for Abyster dev test",
        Contact = new OpenApiContact
        {
            Name = "Zang Bengono Philibert Stephane",
            Email = "stephane.zangbengono@gmail.com",
            Url = new Uri("https://linkedin.com/in/philibert-stephane-zang-bengono-0256771bb"),
        },
        
        Version = "v1"
    });

    //Generate the xml documentation that will dive the swagger documentation
    // var xmlFile = string.Format("{0}.xml", Assembly.GetExecutingAssembly().GetName().Name);
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference{
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },new List<string>()
            }
        });
});

var app = builder.Build();

if(args.Length == 1 && args[0] == "seeddata"){
    seedData(app);
}

void seedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using(var scope = scopedFactory.CreateScope()){
        var service = scope.ServiceProvider.GetService<DatabaseSeeder>();
        service.Seed();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
