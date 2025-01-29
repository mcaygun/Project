using CategoryServices.Data;
using Microsoft.EntityFrameworkCore;
using BookStore.Service.Services;
using BookStore.Service.Services.Concrete;
using BookStore.Service.Services.Abstract;
using NToastNotify;
using Windows.UI.Notifications;


var builder = WebApplication.CreateBuilder(args);



// Diðer servislerin eklenmesi



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
