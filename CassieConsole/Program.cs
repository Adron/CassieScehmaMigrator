using System;
using System.Reflection;
using CassieCoreLib;
using Microsoft.Extensions.CommandLineUtils;

namespace CassieConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "Migration Console",
                Description = "Schema migrations for Apache Cassandra and DataStax Enterprise.",
                ExtendedHelpText = "This CLI app can be used to migrate schema up, down, or to a specific version.",
            };

            app.HelpOption("-?|-h|--help");
            app.VersionOption("-v|--version", () => {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });
            
            app.Command("up", (command) =>
            {
                // This is a command that has it's own options.
                command.ExtendedHelpText = "This is the extended help text for complex-command.";
                command.Description = "This is the description for complex-command.";
                command.HelpOption("-?|-h|--help");

                var schemaVersion = command.Option("-v|--version <value>",
                    "Set the schema version in which to migrate the schema to.",
                    CommandOptionType.SingleValue);

                var version = schemaVersion.Value();
                
                command.OnExecute(() =>
                {
                    Console.WriteLine("Executing up migration.");

                    if(schemaVersion.HasValue())
                    {                        
                        Console.WriteLine("singleValueOption option: {0}", version ?? "null");
                    }
                    else
                    {
                        // TODO: Get latest version from the schema migration table.
                        version = 10.ToString();

                        // TODO: If latest version doesn't exist, do a full up migration and create history table.
                    }

                    Migrate(MigrationDirection.Up, version);
                    
                    Console.WriteLine("Schema version up migration complete.");
                    
                    return 0; 
                });
            });
            
            try
            {
                // This begins the actual execution of the application
                Console.WriteLine("Console migration app executing...");
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                // You'll always want to catch this exception, otherwise it will generate a messy and confusing error for the end user.
                // the message will usually be something like:
                // "Unrecognized command or argument '<invalid-command>'"
                Console.WriteLine("Unrecognized command or argument: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to execute application: {0}", ex.Message);
            }
        }

        public enum MigrationDirection
        {
            Up,
            Down
        }

        public static void Migrate(MigrationDirection direction = MigrationDirection.Up)
        {
            // TODO: Determine latest version (up) or version zero (down) to fully migrate up to.
            var version = "0.0a";
            
            Migrate(direction, version);
        }

        public static void Migrate(MigrationDirection direction, string version)
        {
            Console.WriteLine("Direction: " + direction);
            Console.WriteLine("Version: " + version);
        }
        
    }

}