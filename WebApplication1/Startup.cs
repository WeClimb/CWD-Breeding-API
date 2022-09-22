using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ReviewPlatformAPI.Services;
using ReviewPlatformAPI.Repos;
using ReviewPlatformAPI.Utils;
using Microsoft.IdentityModel.Logging;
using Serilog;

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

            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(Configuration);

            //var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            //var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);

            services.AddScoped<ClientService, ClientService>();
            services.AddScoped<ClientRepo, ClientRepo>();

            services.AddScoped<LoginDataService, LoginDataService>();
            services.AddScoped<LoginDataRepo, LoginDataRepo>();

            services.AddScoped<SubDataService, SubDataService>();
            services.AddScoped<SubDataRepo, SubDataRepo>();

            services.AddScoped<ServiceProviderService, ServiceProviderService>();
            services.AddScoped<ServiceProviderRepo, ServiceProviderRepo>();

            services.AddScoped<ServiceProviderReviewService, ServiceProviderReviewService>();
            services.AddScoped<ServiceProviderReviewRepo, ServiceProviderReviewRepo>();

            services.AddScoped<ReviewService, ReviewService>();
            services.AddScoped<ReviewRepo, ReviewRepo>();

            services.AddScoped<ClientReviewService, ClientReviewService>();
            services.AddScoped<ClientReviewRepo, ClientReviewRepo>();

            services.AddScoped<ChangeRequestService, ChangeRequestService>();
            services.AddScoped<ChangeRequestRepo, ChangeRequestRepo>();

            services.AddScoped<ReviewChangeRequestService, ReviewChangeRequestService>();
            services.AddScoped<ReviewChangeRequestRepo, ReviewChangeRequestRepo>();

            services.AddScoped<ChangePasswordRepo, ChangePasswordRepo>();
            services.AddScoped<ChangePasswordService, ChangePasswordService>();

            services.AddScoped<EmailService, EmailService>();

            services.AddScoped<AuthHelper, AuthHelper>();

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
                       var clientService = context.HttpContext.RequestServices.GetRequiredService<ClientService>();
                       var clientId = Guid.Parse(context.Principal!.Identity!.Name!);
                       var client = clientService.GetByIDNoTracking(clientId);

                       var serviceProviderService = context.HttpContext.RequestServices.GetRequiredService<ServiceProviderService>();
                       var serviceProviderId = Guid.Parse(context.Principal!.Identity!.Name!);
                       var serviceProvider = serviceProviderService.GetByIDNoTracking(serviceProviderId);

                       if (client == null && serviceProvider == null)
                       {
                           context.Fail("Unauthorized");
                       }

                       client = null;
                       serviceProvider = null;

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
