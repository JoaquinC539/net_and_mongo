
using dotenv.net;

using MongoDB.Driver;
using BookStoreApi.Services;

var builder=WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

if(builder.Environment.IsDevelopment())
{
    DotEnv.Load();
    
}
var mongouri=Environment.GetEnvironmentVariable("MONGO_URI");
var mongoDatabase=Environment.GetEnvironmentVariable("MONGO_DB");
if(mongouri!=null && mongoDatabase!=null)
{
    var mongoClient=new MongoClient(mongouri);
    var mongoDb=mongoClient.GetDatabase(mongoDatabase);
    
    builder.Services.AddSingleton<IMongoClient>(mongoClient);
    builder.Services.AddSingleton<IMongoDatabase>(mongoDb);
}




builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<CategoryService>();


var app=builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();

app.Run();

