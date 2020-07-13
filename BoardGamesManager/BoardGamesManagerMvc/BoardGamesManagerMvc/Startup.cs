using AutoMapper;
using BoardGamesManagerMvc.Models.Mapping;
using BoardGamesManagerMvc.Models.Validation;
using BoardGamesServices.Clients.BoardGamesDisplayLogsClients;
using BoardGamesServices.Extensions;
using BoardGamesServices.Services.BoardGameLastDisplays;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoardGamesManagerMvc
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
            services.AddControllersWithViews()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BoardGameViewModelValidator>());

            services.AddAutoMapper(typeof(BoardGamesViewModelMappingProfile));
            services.AddBoardGameService(Configuration);

            services.AddScoped<IBoardGameLastDisplayService, BoardGameLastDisplayService>();
            services.AddScoped<IBoardGamesDisplayLogsClient, FakeBoardGamesDisplayLogsClient>();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=BoardGames}/{action=Index}/{id?}");
            });
        }
    }
}