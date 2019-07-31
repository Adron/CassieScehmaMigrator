﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DockerForTests
{
    public class DockerCruft
    {
        private string _cassieName;
        private string _network;
        private string _cassieVersion;

        public DockerCruft()
        {
            SetContainerDefaults();
        }
        
        public DockerCruft(bool startCassandra)
        {
            SetContainerDefaults();
            SetCassandraConfiguration(_cassieName, _network, _cassieVersion, startCassandra);
        }

        public DockerCruft(string containerName)
        {
            SetContainerDefaults();
            _cassieName = containerName;
            SetCassandraConfiguration(_cassieName, _network, _cassieVersion, false);
        }
        
        public DockerCruft(string containerName, bool startCassandra)
        {
            SetContainerDefaults();
            _cassieName = containerName;
            SetCassandraConfiguration(_cassieName, _network, _cassieVersion, startCassandra);
        }

        public DockerCruft(string containerName, string network, string cassieVersion)
        {
            SetCassandraConfiguration(containerName, network, cassieVersion, false);
        }
        
        public DockerCruft(string containerName, string network, string cassieVersion, bool startCassandra)
        {
            SetCassandraConfiguration(containerName, network, cassieVersion, startCassandra);
        }

        private void SetContainerDefaults()
        {
            _cassieName = $"cassie-{Guid.NewGuid()}-for-testing";
            _network = "default";
            _cassieVersion = "cassandra:3.11.4";
        }

        private void SetCassandraConfiguration(string containerName, string network, string cassieVersion, bool startCassandra)
        {
            _cassieName = containerName;
            _network = network;
            _cassieVersion = cassieVersion;
            if (startCassandra)
            {
                DockerRunCassie();
            }
        }

        private  void ExecDocker(string command, string finishedStatement)
        {
            Console.WriteLine($"docker {command}");
            Process.Start("docker", command);
            Thread.Sleep(3000);
            Console.WriteLine("... processing ...");
            Thread.Sleep(3000);
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

        public void DockerStopRemoveCassie()
        {
            DockerStopCassie();
            DockerRemoveCassie();
        }

        public string GetDockerAddress()
        {
            string ipAddress;
            
            using (var process = new Process())
            {
                var command = " inspect  -f \"{{ .NetworkSettings.IPAddress }}\" " + _cassieName;
                
                Console.WriteLine(command);
                
                process.StartInfo.FileName = "docker";
                process.StartInfo.Arguments = command;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                var reader = process.StandardOutput;
                var output = reader.ReadLine();

                Console.WriteLine(output);
                ipAddress = output;
                
                process.WaitForExit();
            }

            return ipAddress;
        }
    }
}