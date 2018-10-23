using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using EntityCode;
using EntityCode.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service.Implementation;
using Service.Interface;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace WebAPI
{

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddMvc();
            //services.AddAutoMapper();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<DataBaseContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(Configuration.GetConnectionString("App"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });




            //99
            //اضافه کردن سرویس
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IFreightService, FreightService>();
            services.AddScoped<ILoaderService, LoaderService>();
            services.AddScoped<ITruckTypeService, TruckTypeService>();
            services.AddScoped<IPackageTypeService, PackageTypeService>();
            //IPropertyGoodService

            // Register the Identity services.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Register the OpenIddict services.
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<DataBaseContext>();
                }).AddServer(options =>
                {
                    options.UseMvc();
                    // Enable the token endpoint (required to use the password flow).
                    options.EnableTokenEndpoint("/connect/token");

                    // Allow client applications to use the grant_type=password flow.
                    options.AllowPasswordFlow();

                    /*[97-03-07] برای جلوگیری از لغو توکن*/
                    options.DisableSlidingExpiration();

                    options.DisableHttpsRequirement();

                    //97-03-30[این متد اضافه شد.با توجه به نسخه جدید]
                    options.AcceptAnonymousClients();

                });

            services.AddAuthentication()
                .AddOAuthValidation();


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "version 1",
                    Title = "Test API",
                    Description = "دسترسی به وب سرویس ",

                    Contact = new Contact
                    {
                        Name = "تماس با ما",
                        Email = "Rezayousefi88@yahoo.com",
                        Url = "www.sharjbook.com"

                    }
                });
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            //app.UseWelcomePage();
            //app.UseMvcWithDefaultRoute();
            //app.UseStaticFiles();
            //app.UseAuthentication();
            //app.UseCors("CorsPolicy");
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }

    }

}
