using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ReviewPlatformAPI.Services;
using ReviewPlatformAPI.Repos;
using ReviewPlatformAPI.Utils;
using Microsoft.IdentityModel.Logging;
using Serilog;
using CWDBreedingAPI.Utils;

namespace ReviewPlatformAPI
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
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddControllers().AddJsonOptions(_ => _.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            services.AddDbContext<CWDBreedingContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddDistributedMemoryCache();

            services.AddOptions();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(Configuration);

            //var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            //var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);

            services.AddScoped<LoginDataService, LoginDataService>();
            services.AddScoped<LoginDataRepo, LoginDataRepo>();

            services.AddScoped<UserService, UserService>();
            services.AddScoped<UserRepo, UserRepo>();

            services.AddScoped<RanchService, RanchService>();
            services.AddScoped<RanchRepo, RanchRepo>();

            services.AddScoped<ChangePasswordRepo, ChangePasswordRepo>();
            services.AddScoped<ChangePasswordService, ChangePasswordService>();

            services.AddScoped<DeerRepo, DeerRepo>();
            services.AddScoped<DeerService, DeerService>();

            services.AddScoped<EmailService, EmailService>();

            services.AddScoped<AuthHelper, AuthHelper>();

            services.AddScoped<AzureStorageHelper, AzureStorageHelper>();

            services.AddHealthChecks();

            services.AddCors();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.Events = new JwtBearerEvents
               {
                   OnTokenValidated = context =>
                   {
                       var userService = context.HttpContext.RequestServices.GetRequiredService<UserService>();
                       var userId = Guid.Parse(context.Principal!.Identity!.Name!);
                       var user = userService.GetByIDNoTracking(userId);

                       var ranchService = context.HttpContext.RequestServices.GetRequiredService<RanchService>();
                       var ranchId = Guid.Parse(context.Principal!.Identity!.Name!);
                       var ranch = ranchService.GetByIDNoTracking(ranchId);

                       if (user == null && ranch == null)
                       {
                           context.Fail("Unauthorized");
                       }

                       user = null;
                       ranch = null;

                       return Task.CompletedTask;
                   }
               };
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials());

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
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
