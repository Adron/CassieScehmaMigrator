using System;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace DockerForTests
{
    public class DockerCruft
    {
        private string _cassieName;
        private string _network;
        private string _cassieVersion;

        public DockerCruft()
        {
            _cassieName = $"cassie-{Guid.NewGuid()}-for-testing";
            _network = "default";
            _cassieVersion = "cassandra:3.11.4";
        }

        public DockerCruft(string containerName, string network, string cassieVersion)
        {
            _cassieName = containerName;
            _network = network;
            _cassieVersion = cassieVersion;
        }
        
        private  void ExecDocker(string command, string finishedStatement)
        {
            Console.WriteLine($"docker {command}");
            Process.Start("docker", command);
            Thread.Sleep(2000);
            Console.WriteLine("... processing ...");
            Thread.Sleep(2000);
            Console.WriteLine(finishedStatement);
        }
        
        public void DockerRunCassie()
        {
            ExecDocker($"run --name {_cassieName} --network {_network} -d {_cassieVersion}", "Running container.");
        }

        public void DockerStopCassie()
        {
            ExecDocker($"stop {_cassieName}", "Stopped container.");
        }

        public void DockerRemoveCassie()
        {
            ExecDocker($"rm {_cassieName}", "Removed container.");
        }

        public string GetDockerAddress()
        {
            string command = $"docker inspect --format '{{ .NetworkSettings.IPAddress }}' {_cassieName}";


            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = command
                }
            };

            proc.Start();
            var line = string.Empty;
            while (!proc.StandardOutput.EndOfStream)
            {
                line = proc.StandardOutput.ReadLine();
            }

            return line;
        }
    }
}