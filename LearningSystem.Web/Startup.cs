namespace LearningSystem.Web
{
    using AutoMapper;
    using Common.Constants;
    using LearningSystem.Data.Models;
    using LearningSystem.Infrastructure.Automapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services.Contracts;
    using Services.Implementations;
    using Web.Data;
    using Web.Infrastructure.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LearningSystemDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options => 
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;

				options.Password.RequiredLength = DataConstants.Student.PasswordMinLength;
			})
                .AddEntityFrameworkStores<LearningSystemDbContext>()
                .AddDefaultTokenProviders();

			//services.AddAuthentication().AddFacebook(facebookOptions =>
			//{
			//	facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
			//	facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
			//});

			services.AddRouting(options => options.LowercaseUrls = true);

			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IAdminUserService, AdminUserService>();
			services.AddTransient<ICourseService, CourseService>();
			services.AddTransient<IAdminCourseService, AdminCourseService>();
			services.AddTransient<IStudentService, StudentService>();
			services.AddTransient<IBlogAuthorArticleService, BlogAuthorArticleService>();
			services.AddTransient<IArticleService, ArticleService>();
			services.AddTransient<ITrainerService, TrainerService>();

			services.AddAutoMapper(opt => opt.AddProfile(new AutoMapperProfile()));

			services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			app.UseDatabaseMigration();

			app.SeedAdmin();

			app.SeedRoles();

			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
				routes.MapRoute(
					name: "areaRoute",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
