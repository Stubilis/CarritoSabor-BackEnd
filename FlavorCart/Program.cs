using FirebaseAdmin;
using FlavorCart;
using Azure.Identity;

IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });

CreateHostBuilder(args).Build().Run();
//app.Run();
