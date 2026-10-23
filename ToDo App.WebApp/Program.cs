using ToDo_App.Library.Calendar;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;

var builder = WebApplication.CreateBuilder(args);

ToDoHandler toDoHandler = new ToDoHandler();
DataHandler dataHandler = new DataHandler();
ToDoListSort toDoListSort = new ToDoListSort();
TagFilter tagFilter = new TagFilter();
CalendarManager calendarManager = new CalendarManager();
InputManager inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter, calendarManager);

builder.Services.AddSingleton<IInputHandling>(inputManager);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();