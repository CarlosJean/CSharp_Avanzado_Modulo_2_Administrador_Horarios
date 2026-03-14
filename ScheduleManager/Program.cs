using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScheduleManagerContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ScheduleManagerContext") ?? throw new InvalidOperationException("Connection string 'ScheduleManagerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) { 

	var dbContext = scope.ServiceProvider.GetRequiredService<ScheduleManagerContext>();
	if (dbContext.Database.IsRelational()) {
		dbContext.Database.Migrate();
	}
}

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment()) {
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
	pattern: "{controller=Schedules}/{action=Index}/{id?}");

app.Run();
