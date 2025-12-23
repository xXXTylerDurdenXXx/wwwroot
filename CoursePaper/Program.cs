
using CoursePaper.Models;
using CoursePaper.Repository;
using CoursePaper.Service;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CoursePaper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtConfiguration>(
            builder.Configuration.GetSection("JwtSettings"));
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtConfiguration>();
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               

            })
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                })
               .AddJwtBearer(opt =>
               {
                   opt.RequireHttpsMetadata = false;
                   opt.SaveToken = true;
                   opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                       ValidateIssuer = true,
                       ValidIssuer = jwtSettings.Issuer,
                       ValidateAudience = true,
                       ValidAudience = jwtSettings.Audience,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero
                   };
               }
               );

            builder.Services.AddControllersWithViews();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UserDBContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));
            
            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IMarkerRepository, MarkerRepository>();
            builder.Services.AddScoped<ILeaderBoardRepository, LeaderBoardRepository>();
            builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
            builder.Services.AddScoped<IEmailService, EmailServices>();
            builder.Services.AddScoped<IMarkerService, MarkerService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ILeaderBoardService, LeaderBoardService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
           

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

           
            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");
            

            app.Run();
        }
    }
}
