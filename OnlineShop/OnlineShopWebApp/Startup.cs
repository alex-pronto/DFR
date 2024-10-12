using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repository;
using Serilog;
using System;

namespace OnlineShopWebApp
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
			var connection = Configuration.GetConnectionString("online_shop");
			services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseMySql(connection, ServerVersion.AutoDetect(connection)
					//,
	 //               options => options.EnableRetryOnFailure(
	 //               maxRetryCount: 5,
	 //               maxRetryDelay: System.TimeSpan.FromSeconds(30),
	 //               errorNumbersToAdd: null)
					);
			});

            services.AddDbContext<IdentityContext>(options =>

			{
				options.UseMySql(connection, ServerVersion.AutoDetect(connection)
					
					//,options => options.EnableRetryOnFailure(
     //               maxRetryCount: 5,
     //               maxRetryDelay: System.TimeSpan.FromSeconds(30),
     //               errorNumbersToAdd: null)
	                );
			});  
			
			services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
			services.ConfigureApplicationCookie(options =>
			{
				options.ExpireTimeSpan = TimeSpan.FromHours(8);
				options.LoginPath = "/Account/Login";
				options.LogoutPath = "/Account/Logout";
				options.Cookie = new CookieBuilder
				{
					IsEssential = true,
				};
			});
			services.AddTransient<ITimeRepository, TimeDBRepository>();
			services.AddTransient<IDanceFloorsRepository, DanceFloorsDBRepository>();
            services.AddTransient<IItemsRepository, ItemsDBRepository>();
            services.AddTransient<ICartsRepository, CartsDBRepository>(); //перейти на scoped  при создании новых пользователей (для каждого будет создана своя корзина)
            services.AddTransient<IOrdersRepository, OrdersDBRepository>();
            services.AddTransient<IFavouriteDanceFloorsRepository, FavouriteDanceFloorsDBRepository>();
            services.AddTransient<UserManager<User>, UsersDBRepository>(); // нужно ли это здесь? 
            services.AddSingleton<IDanceFloorsHistoryChangesRepository, InMemoryDanceFloorsHistoryChangesRepository>();
            services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();
            
            services.AddSingleton<IUserHistoryChangesRepository, InMemoryUserHistoryChangesRepository>();
            services.AddControllersWithViews();
          

        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseSerilogRequestLogging();
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
			
			app.UseEndpoints(endpoints =>
			{
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
                
				
            });

		}
	}
}
