using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Data.EntityModels;
using Core;
using Core.Services;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using static Core.Services.EmailSender;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace E_commerce
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
            services.AddDbContext< E_commerceDB > (options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<E_commerceDB>();


            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));


            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });


            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IBranchService, BranchService>();
            services.AddTransient<IBrandCategoryService, BrandCategoryService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IGenderCategoryService, GenderCategoryService>();
            services.AddTransient<ISizeService, SizeService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IItemCostHistoryService, ItemCostHistoryService>();
            services.AddTransient<IGenderSubCategoryService, GenderSubCategoryService>();
            services.AddTransient<IItemImageService, ItemImageService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<IForgetPasswordLoggerService, ForgetPasswordLoggerService>();
            services.AddTransient<ICouponService, CouponService>();
            services.AddTransient<IWarehouseService, WarehouseService>();



            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("Policy",
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("Policy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
