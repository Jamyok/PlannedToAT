// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using LoadCsv;
using LoadCsv.Services;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.Sources.Clear();

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

var dbContext = new ImportCsvDbContext(builder.Configuration);
var csvImportService = new CsvImportService(dbContext);
csvImportService.ImportCsv("data/Participants-All_data_fields.csv");
