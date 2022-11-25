using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IoetTest;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(
    services =>
        services.AddHostedService<TimeManager>()
            .AddSingleton<IDataSource, DataSource>());
        

using var host = builder.Build();

host.Run();



