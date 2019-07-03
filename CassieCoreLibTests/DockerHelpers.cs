using System;
using System.Diagnostics;

namespace CassieCoreLibTests
{
    public static class DockerHelpers
    {
        private const string CassieVersion = "cassandra:3.11.4";
        private const string Network = "default";
        private static readonly string CassieName = $"cassie-{Guid.NewGuid()}-for-testing";
        
        private static Boolean ExecDocker(string command)
        {
            try
            {
                Process.Start("docker", command);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public static bool DockerRunCassie(string migrationPath)
        {
            string command = $"run --name {CassieName} --network {Network} -d {CassieVersion}";
            Console.WriteLine(command);
            return ExecDocker(command);
        }

        public static bool DockerDestroyCassie(string migrationPath)
        {
            if (ExecDocker($"stop {CassieName}"))
            {
                return ExecDocker($"rm {CassieName}");    
            }
            Console.WriteLine("Check error, stopping the container ran into a problem.");
            return false;
        }
    }
}