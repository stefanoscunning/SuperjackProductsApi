using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperjackProducts.Api.DataAccess;
using SuperjackProducts.Api.Services;

namespace SuperjackProducts.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("MyNewDatabase")).EnableSensitiveDataLogging(true));
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperjackProducts.Api", Version = "v1" });
      });

      services.AddCors();

      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<ILanguageService, LanguageService>();
      services.AddScoped<IManufacturerService, ManufacturerService>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<ITagService, TagService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext db)
    {
      app.UseCors(options =>
     options.SetIsOriginAllowed(origin=>true)
     .AllowAnyMethod()
     .AllowAnyHeader());

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuperjackProducts.Api v1"));
      }

      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
      }


      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
