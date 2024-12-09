using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.ChirpRepositories;
using Chirp.Infrastructure.ChirpServices;
using Chirp.Razor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<IFollowerRepository, FollowerRepository>();
builder.Services.AddScoped<IFollowService, FollowService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddHsts(options => options.MaxAge = TimeSpan.FromDays(800));

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<Author>(options => 
        options.SignIn.RequireConfirmedAccount = true) 
    .AddEntityFrameworkStores<ChirpDBContext>();
builder.Services.AddAuthentication(options =>
    {
        //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       // options.DefaultChallengeScheme = "GitHub";
       options.RequireAuthenticatedSignIn = true;

    })
    //.AddCookie()
    .AddGitHub(o =>
    {
        o.ClientId = builder.Configuration["authentication_github_clientId"] ?? string.Empty;
        o.ClientSecret = builder.Configuration["authentication_github_clientSecret"] ?? string.Empty;
        o.CallbackPath = "/signin-github";
    });

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ChirpDBContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    DbInitializer.SeedDatabase(dbContext); // Seed data only if necessary
}

app.Run();
