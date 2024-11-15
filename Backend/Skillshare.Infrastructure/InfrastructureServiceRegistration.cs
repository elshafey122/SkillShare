using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using System.Text;
using Skillshare.Contract.RepositoryContracts;
using Skillshare.Contracts.RepositoryContracts;
using Skillshare.Contracts.ServicesContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;
using Skillshare.Infrastructure.Helpers;

namespace Skillshare.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<ICourseRequimentRepository, CourseRequimentRepository>();
            services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
            services.AddScoped<IWhatYouLearnFromCourseRepository, WhatYouLearnFromCourseRepository>();
            services.AddScoped<IWhoIsThisCourseForRepository, WhoIsThisCourseForRepository>();
            services.AddScoped<ICourseLangugeRepository, CourseLangugeRepository>();
            services.AddScoped<ICourseSectionRepository, CourseSectionRepository>();
            services.AddScoped<ICourseLectureRepository, CourseLectureRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           
            services.Configure<StripeSettings>(configuration.GetSection("Stripe")); // using to bind data from appsetting to class and make service for it
           
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConStr")));
           
            var _JWT = services.BuildServiceProvider().GetRequiredService<IOptions<JWT>>().Value; // make service for jwt
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false; // Set this to true to require at least one non-alphanumeric character
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _JWT.Issuer,
                    ValidAudience = _JWT.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}