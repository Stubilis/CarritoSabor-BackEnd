using FirebaseAdmin;
using FlavorCart;
/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => {
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
string[] origins = { "http://localhost:4200", "http://localhost:5208" };
//Add CORS Policy to allow cross-origin requests
//Change the URL to the URL of your frontend
app.UseCors(options => options.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();
*/
IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });

CreateHostBuilder(args).Build().Run();
//app.Run();
