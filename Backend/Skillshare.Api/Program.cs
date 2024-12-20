using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Stripe;
using System.Globalization;
using Skillshare.Api.MiddleWares;
using Skillshare.Application;
using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure;
using Skillshare.Infrastructure.Helpers;
using Skillshare.Infrastructure.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddInfrastructureRegistration(builder.Configuration).AddApplicationServices();

var app = builder.Build();
app.UseMiddleware<ErrorhandlingMiddleWare>();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseStaticFiles();
using (var Scope = app.Services.CreateScope())
{
    var UserManger = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var RoleManger = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var CategoryRepository = Scope.ServiceProvider.GetRequiredService<ICourseCategoryRepository>();
    var LangugeRepository = Scope.ServiceProvider.GetRequiredService<ICourseLangugeRepository>();
    var userProfileRepository = Scope.ServiceProvider.GetRequiredService<IUserProfileRepository>();

    await new SeedAdminWithRolesinitialData(RoleManger, UserManger, userProfileRepository).SeedData();

    await new SeedCategoriesInitialData(CategoryRepository).SeedCategories();
    await new SeedLanguge(LangugeRepository).seedLanguge();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();