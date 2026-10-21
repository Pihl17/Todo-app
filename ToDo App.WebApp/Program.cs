using ToDo_App.Library.Calendar;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.Library.Data;

var builder = WebApplication.CreateBuilder(args);

ToDoHandler toDoHandler = new ToDoHandler();
DataHandler dataHandler = new DataHandler();
ToDoListSort toDoListSort = new ToDoListSort();
TagFilter tagFilter = new TagFilter();
CalendarManager calendarManager = new CalendarManager();
InputManager inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter, calendarManager);

builder.Services.AddSingleton<IInputHandling>(inputManager);

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