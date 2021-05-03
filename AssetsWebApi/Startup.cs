using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Repository.Implementation;
using AssetsWebApi.Model;
using Microsoft.EntityFrameworkCore;
using AdminDAL;
using AssetsDAL;

namespace AssetsWebApi
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

            services.AddDbContext<AssetsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AssetsConnectionString"), x => x.MigrationsAssembly("AssetsWebApi")));
            services.AddDbContext<AdminContext>(options =>  options.UseSqlServer(Configuration.GetConnectionString("AdminConnectionString"),x => x.MigrationsAssembly("AdminWebApi.Migrations")));

            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("Policy11",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            //configure the repository using dependency injection.
            services.AddScoped<ICheckAvailableInterface<SPCheckAvailableResult>, CheckAvailable>();
            services.AddScoped<IDropDownFillInterface<SPDropDownFillResult>, DropDownFill>();
            services.AddScoped<IDashboardInterface<ProductDetailResult>, Dashboard>();
            services.AddScoped<IMasterAssetsAssignmentInterface<MasterAssetsAssignmentResult>, MasterAssetsAssignment>();
            services.AddScoped<IMasterBrandInterface<MasterBrandResult>, MasterBrand>();
            services.AddScoped<IMasterCategoryInterface<MasterCategoryResult>, MasterCategory>();
            services.AddScoped<IMasterProductTypeInterface<MasterProductTypeResult>, MasterProductType>();
            services.AddScoped<IMasterProductSizeInterface<MasterProductSizeResult>, MasterProductSize>();
            services.AddScoped<IMasterProductInterface<MasterProductResult>, MasterProduct>();
            services.AddScoped<IMasterProductChildInterface<MasterProductChildResult>, MasterProductChild>();
            services.AddScoped<IMasterSubCategoryInterface<MasterSubCategoryResult>, MasterSubCategory>();
            services.AddScoped<IMaxTableMasterIdInterface<SPMaxTableMasterIdResult>, MaxTableMasterId>();
            services.AddScoped<INextMasterIdInterface<SPNextMasterIdResult>, NextMasterId>();
            services.AddScoped<ITransactionProductHistoryInterface<TransactionProductHistoryResult>, TransactionProductHistory>();


            // Register Swagger  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AssetsWebAPI", Version = "version 1" });
            });

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.  
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),  
                // specifying the Swagger JSON endpoint.  
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors("AllowMyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
