using CustomAuthorization.BLL.Enums;
using CustomAuthorization.Presentation.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//auth
string key = builder.Configuration["AppSettings:EncryptionKey"];

builder.Services.AddAuthorization(builder =>
{
    builder.AddPolicy("SuperAdmin",
       policy => policy.RequireRole(UserRole.SuperAdmin.ToString()
    ));
    builder.AddPolicy("Admin",
      policy => policy.RequireRole(UserRole.SuperAdmin.ToString(), UserRole.Admin.ToString()
   ));
    builder.AddPolicy("Creator",
      policy => policy.RequireRole(UserRole.SuperAdmin.ToString(), UserRole.Admin.ToString(), UserRole.Creator.ToString()
   ));
});

builder.Services.AddAuthentication(builder =>
{

    builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(builder =>
{

    builder.RequireHttpsMetadata = false;
    builder.SaveToken = true;
    builder.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

    
builder.Services.AddScoped<JwtAuthenticationManager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
