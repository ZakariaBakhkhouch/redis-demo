using RedisDemoApp.Repository;
using StackExchange.Redis;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IList<Endpoint> endpoints = new List<Endpoint>()
//{
//    new Endpoint()
//    {
//        DisplayName = "Name",

//    }
//};

//ConfigurationOptions configurationOptions = new ConfigurationOptions
//{
//    AbortOnConnectFail = false,
//    ClientName = "localhost",
//    EndPoints = new EndPointCollection()
//    {
//        Endpoint e = new()
//        {

//        }
//    }
//};
builder.Services.AddScoped<IPlatfomRepository, PlatfomRepository>();

builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisCS")!)
);

builder.Services.AddHttpClient();

builder.Services.AddScoped<IPlatfomRepository, PlatfomRepository>();

var app = builder.Build();


//use abortConnect = false in your connection string or AbortOnConnectFail=false; in your code.'

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
