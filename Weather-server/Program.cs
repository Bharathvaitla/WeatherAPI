using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Weather_server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<WeatherService>(); 

var key = Encoding.UTF8.GetBytes("averylongsecretkeyoflength256bits");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    // Optionals
                    options.Events = new JwtBearerEvents
                    {
                        //
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsync("{\"error\":\"Missing or invalid token\"}");
                        }

                    
                    };
                });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
