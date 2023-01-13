using CommonProject.Http;
using Microsoft.EntityFrameworkCore;
using Report.Framework;
using Report.Framework.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<APISettings>(builder.Configuration.GetSection("APISettings"));
builder.Services.AddDbContext<ReportDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbContext")));
builder.Services.AddReportServices(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ReportDbContext>();
    dataContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
