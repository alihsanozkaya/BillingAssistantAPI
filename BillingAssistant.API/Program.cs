using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Concrete;
using BillingAssistant.Business.Mappings;
using BillingAssistant.Core.Utilities.Cloudinary;
using BillingAssistant.Core.Utilities.Security.Encryption;
using BillingAssistant.Core.Utilities.Security.JWT;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete;
using BillingAssistant.EmailService;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new CategoryProfile());
    mc.AddProfile(new StoreProfile());
    mc.AddProfile(new ProductProfile());
    mc.AddProfile(new OrderProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

builder.Host.ConfigureServices((hostContext, services) =>
{
    services.AddTransient<IAuthService, AuthManager>();
    services.AddTransient<ITokenHelper, JwtHelper>();
 
    services.AddTransient<IUserService, UserManager>();
    services.AddTransient<IUserRepository, UserRepository>(); 

    services.AddTransient<ICategoryService, CategoryManager>();
    services.AddTransient<ICategoryRepository, CategoryRepository>();

    services.AddTransient<IProductService, ProductManager>();
    services.AddTransient<IProductRepository, ProductRepository>();

    services.AddTransient<IStoreService, StoreManager>();
    services.AddTransient<IStoreRepository, StoreRepository>();

    services.AddTransient<IOrderService, OrderManager>();
    services.AddTransient<IOrderRepository, OrderRepository>();

    services.AddTransient<ICloudinaryService, CloudinaryManager>();
});

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                }));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();