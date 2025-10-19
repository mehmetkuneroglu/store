
using StoreApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Controller ve View yapıları ile çalışabilmek için aşağıdaki servisi ekledik
builder.Services.AddControllersWithViews();
// Projede Razor Page yapısı kullanmak için aşağıdaki servisi ekledik.
builder.Services.AddRazorPages();

// Veritabanı kullanabilmek için aşağıdaki servis kaydını ekleriz.
// Bu kısım Dependency Injection için de önemlidir. Bu kısmı eklemeden DI kullanılamaz.
builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureSession();

builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();

builder.Services.ConfigureRouting();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Uygulama static dosyalar kullanabilmesi için aşağıdaki kodu ekledik. wwwroot klasörü ile
app.UseStaticFiles();
// Oturum yönetimini kullanabilmek için Session metodunu ekledik
app.UseSession();
// Uygulamada Redirection metotlarını işletebilmek ve yönledirme yapabilmek için
app.UseHttpsRedirection();
// Routing yapısı oluşturabilmek için
app.UseRouting();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapAreaControllerRoute(
//         name:"Admin",
//         areaName:"Admin",
//         pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"

//     );
//     endpoints.MapControllerRoute(
//         name:"default",
//          pattern:"{Controller=Home}/{Action=Index}/{id?}"
//     );
// });
app.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
        );
app.MapControllerRoute(
        name: "default",
        pattern: "{Controller=Home}/{Action=Index}/{id?}"
);
// Razor Page endpoind yapısını organize etmek için aşağıdaki metodu ekledik
app.MapRazorPages();
app.ConfigureAndCheckMigration();
app.ConfigureLocalization();

app.Run();
