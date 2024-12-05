using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using caotinhofeliz.Data;
using caotinhofeliz.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configuração do pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Redirecionamento para a página de login, caso o usuário não esteja autenticado.
app.MapGet("/", context =>
{
    // Redireciona para a página de login se o usuário não estiver autenticado.
    var isAuthenticated = context.User.Identity.IsAuthenticated;
    if (!isAuthenticated)
    {
        context.Response.Redirect("/Identity/Account/Login");
    }
    else
    {
        context.Response.Redirect("/Index"); // Ou outra página inicial desejada.
    }

    return System.Threading.Tasks.Task.CompletedTask;
});

app.MapRazorPages(); // Adiciona o mapeamento das Razor Pages.

app.Run();