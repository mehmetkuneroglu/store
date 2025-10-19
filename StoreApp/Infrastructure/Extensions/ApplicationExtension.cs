using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            // context ifadesinin karşılığını newleyerek alabilirdik ancak burada daha başka ihtiyaçlar çıkabilirdi
            // bu yüzden uygulama (app) üzerinden bu değere ulaşmak isteyeceğiz. Aşağıda bunu yaptık.
            RepositoryContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>();
            //Bu aşamada eğer bekleyen migration varsa database update işlemini otomatik yapacak kodu yazalım
            if (context.Database.GetPendingMigrations().Any()) // Eğer bekleyen Migration var ise
            {
                context.Database.Migrate(); //Migration'ı uygula
            }
        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization( options=>
            {
                options.AddSupportedCultures("tr-TR") // desteklenen kültürler virgülle diğerleri eklenebilir
                .AddSupportedUICultures("tr-TR") // arabirimde desteklenen kültürler virgülle diğerleri eklenebilir
                .SetDefaultCulture("tr-TR");    // varsayılan desteklenen kültür        
            });
            
        }
    }
}