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
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Repository.Implementation;
using AdminWebApi.Model;
using AdminDAL;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AdminWebApi
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

            services.AddDbContext<AdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AdminConnectionString"), b=>b.MigrationsAssembly("AdminWebApi")));

            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("Policy11",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            //configure the repository using dependency injection.
            services.AddScoped<ICheckAvailableInterface<SPCheckAvailableResult>, CheckAvailable>();
            services.AddScoped<IDropDownFillInterface<SPDropDownFillResult>, DropDownFill>();
            services.AddScoped<IGenCodeMasterInterface<GenCodeMasterResult>, GenCodeMaster>();
            services.AddScoped<IGenCodeTypeInterface<GenCodeTypeResult>, GenCodeType>();
            services.AddScoped<IMasterAddressTypeInterface<MasterAddressTypeResult>, MasterAddressType>();
            services.AddScoped<IMasterBankAccountTypeInterface<MasterBankAccountTypeResult>, MasterBankAccountType>();
            services.AddScoped<IMasterBranchInterface<MasterBranchResult>, MasterBranch>();
            services.AddScoped<IMasterBusinessVerticleInterface<MasterBusinessVerticleResult>, MasterBusinessVerticle>();
            services.AddScoped<IMasterCityInterface<MasterCityResult>, MasterCity>();
            services.AddScoped<IMasterColorInterface<MasterColorResult>, MasterColor>();
            services.AddScoped<IMasterCompanyInterface<MasterCompanyResult>, MasterCompany>();
            services.AddScoped<IMasterCompanyTypeInterface<MasterCompanyTypeResult>, MasterCompanyType>();
            services.AddScoped<IMasterConfigurationInterface<MasterConfigurationResult>, MasterConfiguration>();
            services.AddScoped<IMasterCountryInterface<MasterCountryResult>, MasterCountry>();
            services.AddScoped<IMasterCurrencyInterface<MasterCurrencyResult>, MasterCurrency>();
            services.AddScoped<IMasterDepartmentInterface<MasterDepartmentResult>, MasterDepartment>();
            services.AddScoped<IMasterDesignationInterface<MasterDesignationResult>, MasterDesignation>();
            services.AddScoped<IMasterEmployeeInterface<MasterEmployeeResult>, MasterEmployee>();
            services.AddScoped<IMasterEmployeeStatusInterface<MasterEmployeeStatusResult>, MasterEmployeeStatus>();
            services.AddScoped<IMasterEmployeeTypeInterface<MasterEmployeeTypeResult>, MasterEmployeeType>();
            services.AddScoped<IMasterErrorLogInterface<MasterErrorLogResult>, MasterErrorLog>();
            services.AddScoped<IMasterFinancialYearInterface<MasterFinancialYearResult>, MasterFinancialYear>();
            services.AddScoped<IMasterFunctionInterface<MasterFunctionResult>, MasterFunction>();
            services.AddScoped<IMasterGenderInterface<MasterGenderResult>, MasterGender>();
            services.AddScoped<IMasterIndustryGroupInterface<MasterIndustryGroupResult>, MasterIndustryGroup>();
            services.AddScoped<IMasterIndustrySubTypeInterface<MasterIndustrySubTypeResult>, MasterIndustrySubType>();
            services.AddScoped<IMasterIndustryTypeInterface<MasterIndustryTypeResult>, MasterIndustryType>();
            services.AddScoped<IMasterLoginInterface<MasterLoginResult>, MasterLogin>();
            services.AddScoped<IMasterLoginTypeInterface<MasterLoginTypeResult>, MasterLoginType>();
            services.AddScoped<IMasterMailTemplateInterface<MasterMailTemplateResult>, MasterMailTemplate>();
            services.AddScoped<IMasterMessageTypeInterface<MasterMessageTypeResult>, MasterMessageType>();
            services.AddScoped<IMasterPaymentTypeInterface<MasterPaymentTypeResult>, MasterPaymentType>();
            services.AddScoped<IMasterProfileInterface<MasterProfileResult>, MasterProfile>();
            services.AddScoped<IMasterRegionInterface<MasterRegionResult>, MasterRegion>();
            services.AddScoped<IMasterRegisteredDeviceInterface<MasterRegisteredDeviceResult>, MasterRegisteredDevice>();
            services.AddScoped<IMasterRegistrationInterface<MasterRegistrationResult>, MasterRegistration>();
            services.AddScoped<IMasterRegistrationTypeInterface<MasterRegistrationTypeResult>, MasterRegistrationType>();
            services.AddScoped<IMasterReportingHeadInterface<MasterReportingHeadResult>, MasterReportingHead>();
            services.AddScoped<IMasterSalutationInterface<MasterSalutationResult>, MasterSalutation>();
            services.AddScoped<IMasterStateInterface<MasterStateResult>, MasterState>();
            services.AddScoped<IMasterStatusInterface<MasterStatusResult>, MasterStatus>();
            services.AddScoped<IMasterTaxInterface<MasterTaxResult>, MasterTax>();
            services.AddScoped<IMasterTimeZoneInterface<MasterTimeZoneResult>, MasterTimeZone>();
            services.AddScoped<IMasterTypeOfDeviceInterface<MasterTypeOfDeviceResult>, MasterTypeOfDevice>();
            services.AddScoped<IMessageNotificationInterface<MessageNotificationResult>, MessageNotification>();
            services.AddScoped<IProfileTaskMappingInterface<SPProfileTaskMappingResult>, ProfileTaskMapping>();
            services.AddScoped<IMasterVendorInterface<MasterVendorResult>, MasterVendor>();
            services.AddScoped<IMaxTableMasterIdInterface<SPMaxTableMasterIdResult>, MaxTableMasterId>();
            services.AddScoped<INextMasterIdInterface<SPNextMasterIdResult>, NextMasterId>();
            services.AddScoped<IValidateAccountInterface<SPValidateAccountResult>, ValidateAccount>();

            // Register Swagger  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AdminWebAPI", Version = "version 1" });
            });

            //services.Configure<FormOptions>(o =>
            //{
            //    o.ValueLengthLimit = int.MaxValue;
            //    o.MultipartBodyLengthLimit = int.MaxValue;
            //    o.MemoryBufferThreshold = int.MaxValue;
            //});
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

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
            //    RequestPath = new PathString("/Resources")
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCors(builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

        }
    }
}
