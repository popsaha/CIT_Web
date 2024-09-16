using CIT_Web;
using CIT_Web.Services;
using CIT_Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddHttpClient<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpClient<IBranchService, BranchServicecs>();
builder.Services.AddScoped<IBranchService, BranchServicecs>();
builder.Services.AddHttpClient<IOrderTypeService, OrderTypeService>();
builder.Services.AddScoped<IOrderTypeService, OrderTypeService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();



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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Overview}/{id?}");

app.Run();
