using BahiaDex.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Ensure Npgsql uses strict UTC for timestamptz
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BahiadexConnexion"),
    npgsql => npgsql.MigrationsAssembly("BahiaDex.Api")
));


//builder.Services.AddScoped<IAndonService, AndonService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// Initialisation database
using (IServiceScope scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();
