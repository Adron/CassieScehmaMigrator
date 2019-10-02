using System;
using CommandLine;

namespace CassieConsole
{
    public class Options
    {
        public enum MigrationDirection
        {
            Up,
            Down
        }
        
        [Option('m', "migrate", Required = false, HelpText = "Designate to migrate up or down.")]
        public MigrationDirection DirectionUp { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(opts => RunParsedOptions(opts));
        }

        private static void RunParsedOptions(Options opts)
        {
            Console.WriteLine(opts.DirectionUp);
        }
        
        public static void Migrate(Options.MigrationDirection direction = Options.MigrationDirection.Up)
        {
            // TODO: Determine latest version (up) or version zero (down) to fully migrate up to.
            var version = "0.0a";
            
            Migrate(direction, version);
        }

        public static void Migrate(Options.MigrationDirection direction, string version)
        {
            Console.WriteLine("Direction: " + direction);
            Console.WriteLine("Version: " + version);
        }
        
    }

}