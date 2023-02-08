using AutoMapper;
using DataAccess.Db;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Service.Abstracts;
using Service.Concretes;
using Service.Config.Mapper;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddDbContext<ApplicationDbContext>(
                con => con.UseNpgsql(
                    builder.Configuration.GetConnectionString("DbConnection")
                    )
                );

            var mapperConfig = new MapperConfiguration(
                mc => mc.AddProfile(
                    new MapperProfile()
                    )
                );

            builder.Services.AddSingleton(mapperConfig.CreateMapper());

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(auth =>
                {
                    auth.LoginPath = "/Login/SignIn";
                    auth.Cookie.HttpOnly = true;
                    auth.Cookie.Name = "AuthCookie";
                    auth.Cookie.MaxAge = TimeSpan.FromHours(24);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}