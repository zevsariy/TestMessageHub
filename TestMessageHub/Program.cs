using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using TestMessageHub.Models;
using TestMessageHub.Models.Const;

namespace TestMessageHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // create test DATA
                DBMessageEntity message001 = new DBMessageEntity
                {
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString(),
                    SendDate = DateTime.UtcNow,
                    Read = true,
                    From = Companies.Adidas,
                    To = Companies.Puma
                };

                DBMessageEntity message002 = new DBMessageEntity
                {
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString(),
                    SendDate = DateTime.UtcNow,
                    Read = true,
                    From = Companies.Puma,
                    To = Companies.Adidas
                };

                db.Messages.AddRange(message001, message002);
                db.SaveChanges();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
