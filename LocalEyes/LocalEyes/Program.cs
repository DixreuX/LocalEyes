using LocalEyes.Components;
using LocalEyes.Components.Account;
using LocalEyes.Data;
using LocalEyes.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LocalEyes
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient<ReportService>();

            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            builder.Services.AddScoped<UserManager<ApplicationUser>>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;

                options.Events.OnRedirectToLogout = context =>
                {
                    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                    context.Response.Headers["Pragma"] = "no-cache";
                    context.Response.Headers["Expires"] = "0";
                    context.Response.Redirect(context.RedirectUri);

                    return Task.CompletedTask;
                };
            });

            builder.Services.AddRazorPages();

            builder.Services.AddServerSideBlazor().AddCircuitOptions(options => options.DetailedErrors = true);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                await SeedData.InitializeAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseMigrationsEndPoint();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/Account/LogoutHandler", async (HttpContext context, SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();

                context.Response.Redirect("/");
            });

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
