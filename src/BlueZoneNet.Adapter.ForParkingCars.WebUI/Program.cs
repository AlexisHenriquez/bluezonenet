var builder = WebApplication.CreateBuilder(args);

builder.Services.Scan(scan => scan
    .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies()) // Scans all loaded assemblies
    .AddClasses(classes => classes.InNamespaces("BlueZoneNet.Adapter.ForObtainingRates.Stub"))
    .AddClasses(classes => classes.InNamespaces("BlueZoneNet.Adapter.ForPaying.Spy"))
    .AddClasses(classes => classes.InNamespaces("BlueZoneNet.Adapter.ForStoringTickets.Fake"))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// Add services to the container.
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