global using Microsoft.EntityFrameworkCore;
using System.Text;
using ChineseSaleServer.BL;
using ChineseSaleServer.Dal;
using ChineseSaleServer.DAL;
using ChineseSaleServer.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IGiftDal, GiftDal>();

builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IDonorDal, DonorDal>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDal, UserDal>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDal, OrderDal>();

builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
builder.Services.AddScoped<IOrderDetailsDal, OrderDetailsDal>();

builder.Services.AddScoped<IPurchasesService, PurchasesService>();
builder.Services.AddScoped<IPurchasesDal, PurchasesDal>();

builder.Services.AddScoped<IDraftService, DraftService>();
builder.Services.AddScoped<IDraftDal, DraftDal>();

builder.Services.AddScoped<IRandomService, RandomService>();
builder.Services.AddScoped<IRandomDal, RandomDal>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartDal, CartDal>();

builder.Services.AddScoped<ChineseSaleServer.BL.IAuthenticationService, ChineseSaleServer.BL.AuthenticationService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChineseSaleContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ChineseSaleContext")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", //give it the name you want
                  builder =>
                  {
                      builder.WithOrigins("http://localhost:4200",
                                           "development web site")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()
                                           .AllowCredentials();
           
           

});

});

// ConfigureServices
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidateLifetime = true,
//                ValidateIssuerSigningKey = true,
//                ValidIssuer = Configuration["Jwt:Issuer"],
//                ValidAudience = Configuration["Jwt:Audience"],
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
//            };
//        });




// Add configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle





builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
// Configure
app.UseAuthentication();



//IConfiguration configuration = app.Configuration;
//IWebHostEnvironment environment = app.Environment;


app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/Auth"),
    builder =>
    {
        builder.UseBearerTokenMiddleware();
    });

//app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Gifts"),builder =>
//{
//    builder.UseBearerTokenMiddleware();
//});
app.MapControllers();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.Run();

