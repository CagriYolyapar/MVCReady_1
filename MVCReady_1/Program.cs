using Microsoft.EntityFrameworkCore;
using MVCReady_1.Models.Context;
using Microsoft.Extensions.Configuration;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Bu noktada hangi servisin eklenecegini belirliyorsunuz...Bazı servisler eklendiklerinde otomatik olarak kullanımı alınırken bazı servisleri de ekledikten sonra   asagıdaki ikinci kısımda özellikle kullanacagınızı belirtmeniz gerekiyor...


//Burada standartlara uygun bir Sql server baglantısı belirtmek istiyorsanız (Context sınıf icerisinde optionsBuilder'in belirtilmesindense bu tercih edilir) su ifadeyi yazmalısınız...

//Pool kullanmak bir Singleton Pattern görevi görür... (Microsoft.Extensions.Configuration kütüphanesi sayesinde Configuration gelir)
builder.Services.AddDbContextPool<MyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies()); //böylece baglantı ayarlarını burada belirtmiş olduk...

//Üstteki ayarlamalar yapıldıktan sonra  terminale < add-migration "migration ismi" > komutunu vermeliyiz


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization(); //Identity

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=ListCategories}/{id?}");

app.Run();
