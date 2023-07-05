﻿using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations;

internal static class Runner
{
    private const string PersistenceConfigKey = "PersistenceConfig";
    private const string AppSettingPath = "appsettings.json";
    private static void Main(string[] args)
    {
        var options = GetSettings(args, Directory.GetCurrentDirectory());
        var connectionString = options.ConnectionString;
        CreateDatabase(connectionString);
        var runner = CreateRunner(connectionString, options);

        runner.MigrateUp();
    }

    private static void CreateDatabase(string connectionString)
    {
        var databaseName = GetDatabaseName(connectionString);
        var masterConnectionString =
            ChangeDatabaseName(connectionString, "master");
        var createDataBaseCommand =
        $"if db_id(N'{databaseName}') is" +
        $" null create database [{databaseName}]";

        using var connection = new SqlConnection(masterConnectionString);
        connection.Open();
        using (var command = new SqlCommand(createDataBaseCommand, connection))
        {
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    private static string ChangeDatabaseName(
        string connectionString,
        string databaseName)
    {
        var csb = new SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = databaseName
        };
        return csb.ConnectionString;
    }

    private static string GetDatabaseName(string connectionString)
    {
        return new SqlConnectionStringBuilder(connectionString)
            .InitialCatalog;
    }

    private static IMigrationRunner CreateRunner(
        string connectionString,
        PersistenceConfig options)
    {
        var container = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(_ => _
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Runner).Assembly).For.All())
            .AddSingleton(options)
            .AddLogging(_ => _.AddFluentMigratorConsole())
            .BuildServiceProvider();
        return container.GetRequiredService<IMigrationRunner>();
    }

    private static PersistenceConfig GetSettings(
        string[] args,
        string baseDir)
    {
        var configurations = new ConfigurationBuilder()
            .SetBasePath(baseDir)
            .AddJsonFile(AppSettingPath, true, true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var settings = new PersistenceConfig();
        configurations.Bind(PersistenceConfigKey, settings);
        return settings;
    }
}

public class PersistenceConfig
{
    public string ConnectionString { get; set; }
}
