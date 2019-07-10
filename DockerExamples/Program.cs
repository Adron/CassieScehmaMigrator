using System;
using DockerForTests;

namespace DockerExamples
{
    class Program
    {
        private static string _cassieName = "cassie-" + Guid.NewGuid() + "-for-testing";
        private static string _cassieVersion = "cassandra:3.11.4";
        private static string _network = "default";

        static void Main(string[] args)
        {
            var docker4Tests = new DockerCruft(_cassieName, _network, _cassieVersion);
          
            docker4Tests.DockerRunCassie();
            Console.WriteLine(docker4Tests.GetDockerAddress());
            docker4Tests.DockerStopCassie();
            docker4Tests.DockerRemoveCassie();
        }
    }
}