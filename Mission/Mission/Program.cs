using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Mission.Entities.Context;
using Mission.Repositories;
using Mission.Repositories.Helpers;
using Mission.Repositories.Interface;
using Mission.Repositories.IRepositories;
using Mission.Repositories.Repositories;
using Mission.Service.IServices;
using Mission.Service.Services;
using Mission.Services;
using Mission.Services.IServices;
using Mission.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<MissionDbContext>(db => db.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(cors => cors.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IMissionThemeRepository, MissionThemeRepository>();
builder.Services.AddScoped<IMissionThemeService, MissionThemeService>();

builder.Services.AddScoped<IMissionSkillRepository, MissionSkillRepository>();
builder.Services.AddScoped<IMissionSkillServices, MissionSkillServices>();

builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();

builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<IMissionRepository, MissionRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "UploadMissionImage")),
    RequestPath = "/UploadedImage"
});
app.UseHttpsRedirection();
app.UseCors("MyPolicy");

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
