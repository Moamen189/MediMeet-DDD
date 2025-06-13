using AppointmentBooking.Api;
using AppointmentBooking.Infrastructure;
using AppointmentConfirmation.Api;
using DoctorAppointmentManagement.AdapterInfra;
using DoctorAppointmentManagement.Core;
using DoctorAvailability.Api;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

#region Modules Registration
builder.Services.AddDAModule();
builder.Services.AddABModule().AddABMInfra();
builder.Services.AddACModule();
builder.Services.AddDAMModule().AddDAMInfra();

builder.Services.AddShared();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
