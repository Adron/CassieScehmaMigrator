using System;
using System.Diagnostics;
using System.Net;

namespace CassieCoreLibTests
{
    public static class DockerHelpers
    {
        private const string CassieVersion = "cassandra:3.11.4";
        private const string Network = "default";
        private static readonly string CassieName = $"cassie-{Guid.NewGuid()}-for-testing";

        public static string Address;
        
        private static void ExecDocker(string command)
        {
            using (Process myProcess = Process.Start("docker", command))
            {
                try
                {
                     Process.Start("docker", command);
                }
                catch (Exception e)
                {
                     Console.WriteLine(e);
                }    
            }
        }

//        public static IPAddress GetDockerAddress()
//        {
//            string command = $" inspect -f \"{{ .NetworkSettings.IPAddress }}\" {CassieName}";
//            result ExecDocker(command);
//        }

        public static void DockerRunCassie(string migrationPath)
        {
            string command = $"run --name {CassieName} --network {Network} -d {CassieVersion}";
            Console.WriteLine(command);
            ExecDocker(command);
        }

        public static void DockerDestroyCassie(string migrationPath)
        {
            try
            {
                ExecDocker($"stop {CassieName}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                ExecDocker($"rm {CassieName}");    
            }
          
            Console.WriteLine("Check error, stopping the container ran into a problem.");
        }
    }
}