using System.Reflection;
using BlueZoneNet.Hexagon;
using BlueZoneNet.Hexagon.Factory;
using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

var builder = WebApplication.CreateBuilder(args);

var assemblies = new List<Assembly>();

foreach (string assemblyPath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories))
{
    if (assemblyPath.Contains("BlueZoneNet"))
    {
        var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
        assemblies.Add(assembly);
    }
}

builder.Services.Scan(scan => scan
     .FromAssemblies(assemblies)
     .AddClasses(classes => classes.InNamespaces(
         "BlueZoneNet.Adapter.ForObtainingRates.Stub",
         "BlueZoneNet.Adapter.ForPaying.Spy",
         "BlueZoneNet.Adapter.ForStoringTickets.Fake",
         "BlueZoneNet.Hexagon"))
     .AsImplementedInterfaces()
     .WithSingletonLifetime());

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();