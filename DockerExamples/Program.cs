using System;
using System.Diagnostics;

namespace DockerExamples
{
    class Program
    {
        private static string _cassieName = "cassie-" + Guid.NewGuid() + "-for-testing";
        private static string _cassieVersion = "cassandra:3.11.4";
        private static string _network = "default";

        static void Main(string[] args)
         {
             DockerRunCassie();
             
//             DockerStopCassie();
//             CassieRemoved();
         }

         private static void DockerRunCassie()
         {
             string command = "run --name " + _cassieName + " --network " + _network + " -d " + _cassieVersion;
             Console.WriteLine(command);
             ExecDocker(command);
         }

         private static void DockerStopCassie()
         {
            ExecDocker("stop " +  _cassieName);   
         }

         private static void CassieRemoved()
         {
             ExecDocker("rm " + _cassieName);
         }

         private static void ExecDocker(string command)
         {
             Process.Start("docker", command);
         }
    }
}