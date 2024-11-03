using CIT_Web;
using CIT_Web.Services;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.IsEssential = true;
});

builder.Services.AddMvc();
builder.Services.AddMemoryCache();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddHttpClient<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddHttpClient<ITaskListService, TaskListService>();
builder.Services.AddScoped<ITaskListService, TaskListService>();

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddHttpClient<ITaskGroupService, TaskGroupService>();
builder.Services.AddScoped<ITaskGroupService, TaskGroupService>();

builder.Services.AddHttpClient<ITaskGroupListService, TaskGroupListService>();
builder.Services.AddScoped<ITaskGroupListService, TaskGroupListService>();

builder.Services.AddHttpClient<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ItaskService, TaskService>();

builder.Services.AddHttpClient<ICrewCommanderService, CrewCommanderService>();
builder.Services.AddScoped<ICrewCommanderService, CrewCommanderService>();

builder.Services.AddHttpClient<IVehicleAssignmentService, VehicleAssignmentService>();
builder.Services.AddScoped<IVehicleAssignmentService, VehicleAssignmentService>();

builder.Services.AddHttpClient<ItaskService, TaskService>();
builder.Services.AddScoped<ItaskService, TaskService>();

//builder.Services.AddHttpClient<IOrderService, OrderService>();
//builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddHttpClient<IOrderRouteService, OrderRouteService>();
builder.Services.AddScoped<IOrderRouteService, OrderRouteService>();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers.Add("Expires", "-1");
    }
});
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
